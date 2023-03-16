using EFDataAccess;
using Implementation.FluentValidators.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.FluentValidators.DishType
{
    public class UpdateDishTypeFluentValidator : DishTypeFluentValidator
    {
        private readonly int _id;

        [Obsolete]
        public UpdateDishTypeFluentValidator(DBContext context, int id) : base(context)
        {
            _id = id;
        }

        protected override bool BeUniqueName(string Name)
        {
            return !_context.DishTypes.Any(g => g.Name == Name && g.Id != _id);
        }
    }
}
