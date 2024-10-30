namespace Destinationosh.Services;

public class VisitSourceOption
{
    public MatchSourceType MatchSourceType 
    { 
        get => _matchSourceType;
        set 
        {
            _matchSourceType = value;
            switch (value)
            {
                case MatchSourceType.Exact:
                    _matchSource = url => url == Argument;
                    break;
                case MatchSourceType.Contains:
                    _matchSource = url => url.Contains(Argument);
                    break;
                case MatchSourceType.StartsWith:
                    _matchSource = url => url.StartsWith(Argument);
                    break;
                case MatchSourceType.EndsWith:
                    _matchSource = url => url.EndsWith(Argument);
                    break;
            }
        }
    }
    private MatchSourceType _matchSourceType = MatchSourceType.Contains;
    public string Argument { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Icon { get; set; }
    public Func<string, bool> MatchSource { get => _matchSource; }
    private Func<string, bool> _matchSource = null!;
}