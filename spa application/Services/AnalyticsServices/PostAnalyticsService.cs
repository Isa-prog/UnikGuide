using Destinationosh.Models;
using Microsoft.EntityFrameworkCore;
using JM.LinqFaster;

namespace Destinationosh.Services;

public class PostAnalyticsService : IPostAnalyticsService
{
    private readonly ApplicationContext _db;
    private readonly HttpContext _httpContext;
    private readonly VisitsSourceService _visitsSourceService;
    public PostAnalyticsService(ApplicationContext db, IHttpContextAccessor accessor, VisitsSourceService visitsSourceService)
    {
        _db = db;
        _httpContext = accessor.HttpContext ?? throw new ArgumentNullException(nameof(accessor));
        _visitsSourceService = visitsSourceService;
    }

    public async Task<PostVisit[]> GetPostVisits(Post post)
    {
        return await _db.PostVisits.Where(p => p.PostId == post.Id).ToArrayAsync();
    }

    public async Task AddPostVisit(Post post)
    {
        var url = _httpContext.Request.Headers["Referer"].ToString();
        var ip = _httpContext.Connection.RemoteIpAddress;
        var postVisit = new PostVisit
        {
            PostId = post.Id,
            UrlReferrer = url,
            Ip = ip != null ? ip.ToString() : null,
            VisitDate = DateTime.Now
        };
        await _db.PostVisits.AddAsync(postVisit);
        await _db.SaveChangesAsync();
    }

    public async Task<PostAnalytics> GetPostAnalytics(Post post)
    {
        var postVisits =  await _db.PostVisits.Where(p => p.Id == post.Id).ToArrayAsync();
        var week =  CountVisistsBeetwinDays(DateTime.Today.AddDays(-7), DateTime.Today, postVisits);
        var month = CollapsByStep(
            CountVisistsBeetwinDays(DateTime.Today.AddDays(-30), DateTime.Today, postVisits),
            7
        );
        var year = CollapsByStep(
            CountVisistsBeetwinDays(DateTime.Today.AddDays(-365), DateTime.Today, postVisits),
            30
        );
        return new PostAnalytics(
            LastWeek: week,
            LastMonth: month,
            LastYear: year,
            VisitsSources:  _visitsSourceService.GetVisitsSources(postVisits),
            TotalVisits: postVisits.Length,
            TotalUniqueVisits: postVisits.SelectF(p => p.Ip).Distinct().Count()
        );
    }

    private VisitsDayCount[] CountVisistsBeetwinDays(DateTime startDate, DateTime endDate, PostVisit[] posts)
    {
        var visits =  posts.WhereF(p => p.VisitDate.Date >= startDate && p.VisitDate.Date <= endDate);
        var visitsCount = new Dictionary<DateTime, int>();
        for(int i=0; i <= (endDate - startDate).Days; i++)
        {
            var date = startDate.AddDays(i);
            visitsCount.Add(date, 0);
        }
        foreach (var visit in visits)
        {
            visitsCount[visit.VisitDate.Date]++;
        }
        return visitsCount.Select(v => new VisitsDayCount(v.Value, v.Key)).ToArray();
    }

    private IEnumerable<VisitsDayCount> CollapsByStep(VisitsDayCount[] visits, int step)
    {
        var result = new List<VisitsDayCount>();
        for (int i = 0; i < visits.Length; i += step)
        {
            var curentStep = step;
            if(i + step >= visits.Length)
            {
                curentStep = visits.Length - i;
            }
            var sum = 0;
            for (int j = 0; j < curentStep; j++)
            {
                sum += visits[i + j].Count;
            }
            result.Add(new VisitsDayCount(sum, visits[i].Date));
        }
        return result;
    }
}