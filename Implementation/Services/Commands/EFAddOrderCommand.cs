using Application.Commands;
using Application.DataTransfer;
using AutoMapper;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;
using Implementation.Services.Exstensions;

namespace Implementation.Services.Commands
{
    public class EFAddOrderCommand : BaseService, IAddOrderCommand
    {
        private readonly IMapper _mapper;

        public EFAddOrderCommand(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public void Execute(OrderDTO request)
        {
            var orderItems = request.OrderItems;
            var dishIds = orderItems.Where(item => item.DishId > 0).Select(item => item.DishId);
            var dishes = this._context.Dishes.Where(dish => dishIds.Contains(dish.Id)).ToList();

            var mappingToOrderEntity = this._mapper.Map<OrderEntity>(new OrderDTO
            {
                TotalPrice = request.OrderItems.Where(d => d.DishId > 0).Sum(orderItem => ((decimal)(dishes.FirstOrDefault(dish => dish.Id == orderItem.DishId).Price)).Total(orderItem.Qty)),
                Date = DateTime.Now,
                UserId = request.UserId
            });

            this._context.Orders.Add(mappingToOrderEntity);
            this._context.SaveChanges();

            var orderItemsEntities = request.OrderItems.Select(orderItem => new OrderItemEntity
            {
                DishId = orderItem.DishId,
                Qty = orderItem.Qty,
                OrderId = orderItem.OrderId
            }).ToList();

            this._context.OrderItems.AddRange(orderItemsEntities);
            this._context.SaveChanges();
        }
    }
}
