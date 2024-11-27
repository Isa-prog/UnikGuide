namespace Destinationosh.Services;

public static class PostBuilderExtensions
{
    public static void AddPostBlockConverters(this IServiceCollection services)
    {
        services.AddTransient<IBlockConverter, Paragraph>();
        services.AddTransient<IBlockConverter, Header>();
        services.AddTransient<IBlockConverter, Image>();
        services.AddTransient<IBlockConverter, Cards>();
        services.AddTransient<IBlockConverter, Carousel>();
    }

    public static void AddPostBuilder(this IServiceCollection services)
    {
        services.AddTransient<PostBuilder>();
    }
}