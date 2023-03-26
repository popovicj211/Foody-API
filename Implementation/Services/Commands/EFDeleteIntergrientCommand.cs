using Application.Commands;
using Application.Exceptions;
using AutoMapper;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Commands
{
    public class EFDeleteIntergrientCommand : BaseService, IDeleteIngedientCommand
    {
        public EFDeleteIntergrientCommand(DBContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            var ingredient = _context.Ingredients.FirstOrDefault(u => u.Id == id);

            if (ingredient == null)
            {
                throw new EntityNotFoundException("Ingredient");
            }

            if (ingredient.IsDeleted == true)
            {
                throw new AlreadyExistException();
            }

            ingredient.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
