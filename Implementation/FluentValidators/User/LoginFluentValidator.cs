using Application.DataTransfer;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Implementation.FluentValidators.User
{
    public class LoginFluentValidator : AbstractValidator<LoginDTO>
    {
        [Obsolete]
        public LoginFluentValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("Email address is required.")
                .EmailAddress()
                .WithMessage("Email address is not valid.")
                .MaximumLength(40)
                .WithMessage("Email address can't be longer than 40 characters.");

            RuleFor(u => u.Password)
              .NotEmpty()
              .WithMessage("Password is required.")
              .MinimumLength(7)
              .WithMessage("Password must be at least 7 characters long.")
              .Matches(new Regex("/^(?=.*[a-z])(?=.*[A-Z])(?=.*[\\d]).{7,}$/"))
              .WithMessage("Password is not valid, please enter at least one digits, lower and upper characters");
        }
    }
}
