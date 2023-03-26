using Application.Commands;
using Application.Exceptions;
using AutoMapper;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Commands
{
    public class EFDeleteOrderItemCommand : BaseService, IDeleteOrderItemCommand
    {
        public EFDeleteOrderItemCommand(DBContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            var orderItem = this._context.OrderItems.FirstOrDefault(orderItem => orderItem.Id == id);

            if(orderItem == null)
            {
                throw new EntityNotFoundException("Order item");
            }

            if (orderItem.IsDeleted == true)
            {
                throw new AlreadyExistException();
            }

            orderItem.IsDeleted = true;
            this._context.SaveChanges();
        }
    }
}
