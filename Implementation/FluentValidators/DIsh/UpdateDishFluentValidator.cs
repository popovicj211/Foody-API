using EFDataAccess;

namespace Implementation.FluentValidators.DIsh
{
    public class UpdateDishFluentValidator : DishFluentValidator
    {
        private readonly int _id;

        [Obsolete]
        public UpdateDishFluentValidator(DBContext context, int id) : base(context)
        {
            _id = id;
        }

        protected override bool BeUniqueName(string Name)
        {
            return !_context.Dishes.Any(g => g.Name == Name && g.Id != _id);
        }
    }
}
