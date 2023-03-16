using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
