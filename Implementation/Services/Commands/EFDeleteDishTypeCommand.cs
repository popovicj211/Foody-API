using Application.Commands;
using Application.Exceptions;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Commands
{
    public class EFDeleteDishTypeCommand : BaseService, IDeleteDishTypeCommand
    {
        public EFDeleteDishTypeCommand(DBContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            var dishType = _context.DishTypes.FirstOrDefault(u => u.Id == id);

            if (dishType == null)
            {
                throw new EntityNotFoundException("Dish type");
            }

            if (dishType.IsDeleted == true)
            {
                throw new AlreadyExistException();
            }

            dishType.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
