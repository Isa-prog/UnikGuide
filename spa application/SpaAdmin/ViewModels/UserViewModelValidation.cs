using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Destinationosh.SpaAdmin.ViewModels;

public class UserViewModelValidator : AbstractValidator<UserViewModel>
{
    public UserViewModelValidator(IStringLocalizer<UserViewModel> localizer)
    {
       RuleFor(x => x.Login).NotEmpty().WithMessage(localizer["LoginRequired"]);
       RuleFor(x => x.Password).NotEmpty().WithMessage(localizer["PasswordRequired"]);
       RuleFor(x => x.Password).Length(8, 100).WithMessage(localizer["PasswordLength"]);
       RuleFor(x => x.Password).Matches(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$").WithMessage(localizer["PasswordRegex"]);
       RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage(localizer["PasswordConfirm"]);
       RuleFor(x => x.FullName).NotEmpty().WithMessage(localizer["FullNameRequired"]);
       RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage(localizer["EmailRequired"]);
    }
}