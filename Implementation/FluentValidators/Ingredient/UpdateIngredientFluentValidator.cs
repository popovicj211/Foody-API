using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.FluentValidators.Ingredient
{
    public class UpdateIngredientFluentValidator : IngredientFluentValidator
    {
        private readonly int _id;

        [Obsolete]
        public UpdateIngredientFluentValidator(DBContext context, int id) : base(context)
        {
            _id = id;
        }

        protected override bool BeUniqueName(string Name)
        {
            return !_context.Ingredients.Any(g => g.Name == Name && g.Id != _id);
        }
    }
}
