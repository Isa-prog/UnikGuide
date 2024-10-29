using System.Text.Json.Serialization;
using Destinationosh.Middlewares;
using Destinationosh.Extensions;
using Destinationosh.Models;
using Destinationosh.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Destinationosh.SpaAdmin.ViewModels;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("MySql");

builder.Services.Configure<SupportedCultureOptions>( builder.Configuration.GetSection("SupportedCultureOptions"));
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddScoped(
        s =>
        {
            var navigationManager = s.GetRequiredService<NavigationManager>();
            return new HttpClient
            {
                BaseAddress = new Uri(navigationManager.BaseUri)
            };
        });

//Validators
builder.Services.AddScoped<IValidator<UserViewModel>, UserViewModelValidator>();
builder.Services.AddScoped<IValidator<PostViewModel>, PostViewModelValidator>();

builder.Services.AddRazorPages(options =>
            {
                options.Conventions.Add(new CultureTemplatePageRouteModelConvention());
                options.Conventions.AuthorizePage("/DesDer");
            });
builder.Services.AddHttpContextAccessor();
builder.Services.AddXssSecurity();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 27))));

builder.Services.AddIdentity<User, IdentityRole>(options => 
{
    options.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin";
});

builder.Services.AddTransient<VisitsSourceService>();

builder.Services.AddTransient<IUserService, UserServiceIdentity>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IFileUploaderService, FileUploaderService>();
builder.Services.AddTransient<IPostAnalyticsService, PostAnalyticsService>();

builder.Services.AddScoped<IPostContainerService, PostContainerService>();

builder.Services.AddPostBlockConverters();
builder.Services.AddPostBuilder();

builder.Services.AddControllers()
    .AddDataAnnotationsLocalization();

builder.Services.AddSpaAdmin();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSpaAdmin();

app.MapControllers();

app.UseCultureRedirect();

app.MapFallbackToPage("/{culture}/{*pattern:nonfile}", "/PostsView");

//DataSeeder
var services = app.Services.CreateAsyncScope();
await DataSeeder.InitializeAsync(
    services.ServiceProvider.GetRequiredService<UserManager<User>>(),
    services.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>()
);

app.Run();