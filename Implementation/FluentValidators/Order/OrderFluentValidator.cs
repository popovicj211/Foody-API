using Application.DataTransfer;
using EFDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.FluentValidators.Order
{
    public class OrderFluentValidator : AbstractValidator<OrderDTO>
    {
        protected readonly DBContext _context;

        [Obsolete]
        public OrderFluentValidator(DBContext context)
        {
            _context = context;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(u => u.TotalPrice)
                   .NotEmpty()
                   .WithMessage("Total price is required.")
                   .PrecisionScale(11, 2, false)
                   .WithMessage("Price must not be more than 11 digits in total, with allowance for 2 decimals");
        }
    }
}
