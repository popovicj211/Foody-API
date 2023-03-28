using EFDataAccess;

namespace Implementation.FluentValidators.Contact
{
    public class UpdateContactFluentValidator : ContactFluentValidator
    {
        private readonly int _id;

        [Obsolete]
        public UpdateContactFluentValidator(DBContext context, int id) : base(context)
        {
            _id = id;
        }
    }
}
