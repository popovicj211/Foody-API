using Application.DataTransfer;
using EFDataAccess;
using FluentValidation;

namespace Implementation.FluentValidators.DIsh
{
    public class DishFluentValidator : AbstractValidator<DishDTO>
    {
        protected readonly DBContext _context;

        [Obsolete]
        public DishFluentValidator(DBContext context)
        {
            _context = context;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(u => u.Name)
                .NotEmpty()
                .WithMessage("Name name is required.")
                .MinimumLength(3)
                .WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(40)
                .WithMessage("Name can't be longer than 40 characters.")
                .Must(BeUniqueName)
                .WithMessage("Name address already exists.");


            RuleFor(u => u.Image)
                .NotEmpty()
                .WithMessage("Image is required.");
                 

            RuleFor(u => u.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MinimumLength(7)
                .WithMessage("Name must be at least 7 characters long.");

            RuleFor(u => u.Price)
                .NotEmpty()
                .WithMessage("Price is required.")
                .PrecisionScale(11, 2, false)
                .WithMessage("Price must not be more than 11 digits in total, with allowance for 2 decimals");
        }

        protected virtual bool BeUniqueName(string name)
        {
            return !_context.Dishes.Any(u => u.Name == name);
        }
    }
}
