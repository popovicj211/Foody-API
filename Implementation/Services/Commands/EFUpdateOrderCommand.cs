using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using EFDataAccess;
using Implementation.EFServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Commands
{
    public class EFUpdateOrderCommand : BaseService, IUpdateOrderCommand
    {
        private readonly IMapper _mapper;

        public EFUpdateOrderCommand(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public void Execute(OrderDTO request)
        {
            var order = _context.Orders.FirstOrDefault(u => u.Id == request.Id);

            if (order == null)
            {
                throw new EntityNotFoundException("Order");
            }

            if (order.TotalPrice != request.TotalPrice)
            {
               order.TotalPrice = request.TotalPrice;
            }

            if (order.Date != request.Date)
            {
                order.Date = request.Date;
            }

            _context.SaveChanges();
        }
    }
}
