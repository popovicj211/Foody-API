using Application.DataTransfer;
using EFDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.FluentValidators.Ingredient
{
    public class IngredientFluentValidator : AbstractValidator<IngredientDTO>
    {
        protected readonly DBContext _context;

        [Obsolete]
        public IngredientFluentValidator(DBContext context)
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
            return !_context.Ingredients.Any(u => u.Name == name);
        }
    }
}
