using Destinationosh.Models;
using JM.LinqFaster;
using Microsoft.Extensions.Options;

namespace Destinationosh.Services;

public class VisitsSourceService
{
    private readonly AnalyticsOptions _options;
    public VisitsSourceService(IOptions<AnalyticsOptions> options)
    {
        _options = options.Value;
    }
    public IEnumerable<VisitsSource> GetVisitsSources(PostVisit[] visits)
    {
        var sources = new Dictionary<string, int>();
        sources.Add("other", 0);
        foreach(var visit in visits)
        {
            if (_options.VisitSources != null && visit.UrlReferrer != null)
            {
                bool found = false;
                foreach (var source in _options.VisitSources)
                {
                    if (source.MatchSource(visit.UrlReferrer))
                    {
                        found = true;
                        if (sources.ContainsKey(source.Name))
                        {
                            sources[source.Name]++;
                        }
                        else
                        {
                            sources.Add(source.Name, 1);
                        }
                        break;
                    }
                }
                if(!found) 
                {
                    sources["other"]++;
                }
            }
            else
            {
                sources["other"]++;
            }
        }
        return sources.Select( pair => new VisitsSource(pair.Value, pair.Key));
    } 
}