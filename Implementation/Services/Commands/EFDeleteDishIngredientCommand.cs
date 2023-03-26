using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;
using Microsoft.EntityFrameworkCore;

namespace Implementation.Services.Commands
{
    public class EFDeleteDishIngredientCommand : BaseService, IDeleteDishIngredientCommand
    {
        public EFDeleteDishIngredientCommand(DBContext context) : base(context)
        {
        }

        public void Execute(DishIngredientDTO request)
        {
            var dish = _context.Dishes.Include(d => d.DishIngredients).FirstOrDefault(g => g.Id == request.DishId);

            if (dish == null)
            {
                throw new EntityNotFoundException("Dish");
            }

            List<DishIngredientEntity> dishIngredients = new List<DishIngredientEntity>();

            foreach (int item in request.IngredientIds)
            {
                var ingredient = dish.DishIngredients.FirstOrDefault(g => g.IngreId == item);

                if (ingredient == null)
                {
                    throw new EntityNotFoundException("Ingredient");
                }

                dishIngredients.Add(ingredient);
            }

            foreach (var item in dishIngredients)
            {
                this._context.Remove(item);
                this._context.SaveChanges();
            }
        }
    }
}
