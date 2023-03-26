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
    public class EFUpdateOrderItemCommand : BaseService, IUpdateOrderItemCommand
    {
        private readonly IMapper _mapper;

        public EFUpdateOrderItemCommand(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public void Execute(OrderItemDTO request)
        {
            var orderItem = _context.OrderItems.FirstOrDefault(u => u.Id == request.Id);

            if (orderItem == null)
            {
                throw new EntityNotFoundException("Order item");
            }

            //if (order.Total != request.To)
            //{
            //    order.TotalPrice = request.TotalPrice;
            //}

         

            _context.SaveChanges();
        }
    }
}
