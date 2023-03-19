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
    public class EFAddDishTypeDishCommand : BaseService, IAddDishTypeDishCommand
    {
        private readonly IMapper _mapper;

        public EFAddDishTypeDishCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public void Execute(DishTypeDishDTO request)
        {
            var dish = this._context.Dishes.Include(g => g.DishTypeDishes).FirstOrDefault(g => g.Id == request.DishId);

            if (dish == null)
            {
                throw new EntityNotFoundException("Dish");
            }

            List<DishTypeEntity> dishTypes = new List<DishTypeEntity>();
            List<DishTypeDishEntity> dishDishTypes = new List<DishTypeDishEntity>();

            foreach (var item in request.DishTypeIds)
            {
                var dishTypeCheck = this._context.DishTypes.Find(item);

                if (dishTypeCheck == null)
                {
                    throw new EntityNotFoundException("Dish type");
                }
                else
                {
                    bool isContainDishTypeId = dish.DishTypeDishes.Any(g => g.DishTypeId == item);

                    if (isContainDishTypeId)
                    {
                        throw new AlreadyExistException();
                    }

                    dishTypes.Add(dishTypeCheck);
                }

                dishDishTypes.Add(new DishTypeDishEntity
                {
                    DishId = request.Id,
                    DishTypeId = item
                });
            }

            this._context.DishTypeDishes.AddRange(dishDishTypes);
            this._context.SaveChanges();
        }
    }
}
