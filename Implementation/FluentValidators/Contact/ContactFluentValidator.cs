using Application.DataTransfer;
using EFDataAccess;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Implementation.FluentValidators.Contact
{
    public class ContactFluentValidator : AbstractValidator<ContactDTO>
    {
        protected readonly DBContext _context;

        [Obsolete]
        public ContactFluentValidator(DBContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(u => u.FullName)
    .NotEmpty()
                .WithMessage("Fullname name is required.")
                .MinimumLength(3)
                .WithMessage("Fullname must be at least 3 characters long.")
                .MaximumLength(30)
                .WithMessage("Fullname can't be longer than 30 characters.")
                .Matches(new Regex("^[A-Z][a-z]{2,15}(\\s[A-Z][a-z]{2,15})+$"))
                .WithMessage("Fullname is not valid, must start with upper letter and contains lower letters.");

            RuleFor(g => g.Email)
                .NotEmpty()
                .WithMessage("Email address is required.")
                .MaximumLength(40)
                .WithMessage("Email address can't be longer than 40 characters.")
                .EmailAddress()
                .WithMessage("Invalid email address.");

            RuleFor(u => u.Message)
                .NotEmpty()
                .WithMessage("Message is required.")
                .MinimumLength(3)
                .WithMessage("Message must be at least 3 characters long.");
        }
    }
}
