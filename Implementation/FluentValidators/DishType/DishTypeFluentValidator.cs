using Application.DataTransfer;
using EFDataAccess;
using FluentValidation;

namespace Implementation.FluentValidators.DishType
{
    public class DishTypeFluentValidator : AbstractValidator<DishTypeDTO>
    {
        protected readonly DBContext _context;

        [Obsolete]
        public DishTypeFluentValidator(DBContext context)
        {
            _context = context;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(u => u.Name)
                .NotEmpty()
                .WithMessage("Name name is required.")
                .MinimumLength(3)
                .WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(50)
                .WithMessage("Name can't be longer than 50 characters.")
                .Must(BeUniqueName)
                .WithMessage("Name address already exists.");
        }

        protected virtual bool BeUniqueName(string name)
        {
            return !_context.DishTypes.Any(u => u.Name == name);
        }
    }
}
