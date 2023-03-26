using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;
using Microsoft.EntityFrameworkCore;

namespace Implementation.Services.Commands
{
    public class EFDeleteDishTypeDishCommand : BaseService, IDeleteDishTypeDishCommand
    {
        public EFDeleteDishTypeDishCommand(DBContext context) : base(context)
        {
        }

        public void Execute(DishTypeDishDTO request)
        {
            var dish = _context.Dishes.Include(d => d.DishTypeDishes).FirstOrDefault(g => g.Id == request.DishId);

            if (dish == null)
            {
                throw new EntityNotFoundException("Dish");
            }

            List<DishTypeDishEntity> dishTypeDishes = new List<DishTypeDishEntity>();

            foreach (int item in request.DishTypeIds)
            {
                var dishType = dish.DishTypeDishes.FirstOrDefault(g => g.DishTypeId == item);

                if (dishType == null)
                {
                    throw new EntityNotFoundException("Dish type");
                }

                dishTypeDishes.Add(dishType);
            }

            foreach (var item in dishTypeDishes)
            {
                this._context.Remove(item);
                this._context.SaveChanges();
            }
        }
    }
}
