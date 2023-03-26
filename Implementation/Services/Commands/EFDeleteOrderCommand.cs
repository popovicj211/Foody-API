using Application.Commands;
using Application.Exceptions;
using AutoMapper;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Commands
{
    public class EFDeleteOrderCommand : BaseService, IDeleteOrderCommand
    {
        public EFDeleteOrderCommand(DBContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            var order = _context.Orders.FirstOrDefault(u => u.Id == id);

            if (order == null)
            {
                throw new EntityNotFoundException("Order");
            }

            if (order.IsDeleted == true)
            {
                throw new AlreadyExistException();
            }

            order.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
