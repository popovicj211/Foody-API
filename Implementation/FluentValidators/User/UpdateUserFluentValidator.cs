using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.FluentValidators.User
{
    public class UpdateUserFluentValidator : RegisterFluentValidator
    {
        private readonly int _id;

        public UpdateUserFluentValidator(DBContext context) : base(context)
        {
        }

        [Obsolete]
        public UpdateUserFluentValidator(DBContext context, int id) : base(context)
        {
            _id = id;
        }

        protected override bool BeUniqueEmailInDatabase(string email)
        {
            return !_context.Users.Any(u => u.Email == email && u.Id != _id);
        }

        protected override bool BeUniqueUsernameInDatabase(string username)
        {
            return !_context.Users.Any(u => u.Username == username && u.Id != _id);
        }
    }
}
