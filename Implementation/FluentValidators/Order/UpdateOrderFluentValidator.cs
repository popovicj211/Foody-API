using EFDataAccess;

namespace Implementation.FluentValidators.Order
{
    internal class UpdateOrderFluentValidator : OrderFluentValidator
    {
        private readonly int _id;

        [Obsolete]
        public UpdateOrderFluentValidator(DBContext context, int id) : base(context)
        {
            _id = id;
        }

    }
}
