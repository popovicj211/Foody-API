using Application.Commands;
using Application.Exceptions;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Commands
{
    public class EFDeleteDishCommand : BaseService, IDeleteDishCommand
    {
        public EFDeleteDishCommand(DBContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            var dish = _context.Dishes.FirstOrDefault(u => u.Id == id);

            if (dish == null)
            {
                throw new EntityNotFoundException("Dish");
            }

            if (dish.IsDeleted == true)
            {
                throw new AlreadyExistException();
            }

            dish.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
