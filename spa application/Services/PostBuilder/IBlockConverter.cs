namespace Destinationosh.Services;

public interface IBlockConverter
{
    string Name { get; }
    string Convert(string json);
}
