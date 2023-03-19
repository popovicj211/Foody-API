using Application.Commands;
using Application.DataTransfer;
using AutoMapper;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Commands
{
    public class EFAddOrderItemCommand : BaseService, IAddOrderItemCommand
    {
        private readonly IMapper _mapper;
        public EFAddOrderItemCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public void Execute(List<OrderItemDTO> request)
        {
            var orderItemEntities = request.Select(orderItem => new OrderItemEntity
            {
                DishId = orderItem.DishId,
                Qty = orderItem.Qty,
                OrderId = orderItem.OrderId
            }).ToList();

            this._context.OrderItems.AddRange(orderItemEntities);
            this._context.SaveChanges();
        }
    }
}
