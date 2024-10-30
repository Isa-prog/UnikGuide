using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Destinationosh.SpaAdmin.ViewModels;

public class PostViewModelValidator : AbstractValidator<PostViewModel>
{
    private readonly IStringLocalizer<PostViewModel> _localizer;
    public PostViewModelValidator(IStringLocalizer<PostViewModel> localizer)
    {
        _localizer = localizer;
        RuleFor(x => x.Name).NotEmpty().WithMessage(_localizer["NameRequired"]);
        RuleFor(x => x.Route)
            .NotEmpty().WithMessage(_localizer["RouteRequired"])
            .Matches(@"^[a-zA-Z\/]+$").WithMessage(_localizer["RouteMustn'tContainSpecialCharacters"])
            .Length(5, 100).WithMessage(_localizer["RouteMustn'tBeLongerThan100Characters"])
            .Matches(@"^((?!(\d)).)*$").WithMessage(_localizer["RouteMustn'tContainNumbers"])
            .Must(ValidateRoute).WithMessage(_localizer["RouteInvalid"]);
    }

    bool ValidateRoute(string route)
    {
        if(string.IsNullOrWhiteSpace(route))
        {
            return false;
        }
        if(route.StartsWith("/") || route.EndsWith("/"))
        {
            return false;
        }
        if(!route.Split("/").All(x => x.Length > 0))
        {
            return false;
        }
        return true;
    }
}