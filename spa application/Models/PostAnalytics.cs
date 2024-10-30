namespace Destinationosh.Models;

public record PostAnalytics(
    IEnumerable<VisitsDayCount> LastWeek, 
    IEnumerable<VisitsDayCount> LastMonth,
    IEnumerable<VisitsDayCount> LastYear,
    int TotalVisits,
    int TotalUniqueVisits,
    IEnumerable<VisitsSource> VisitsSources
);