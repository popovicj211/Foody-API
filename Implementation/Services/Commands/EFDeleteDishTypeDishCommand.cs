using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Commands
{
    public class EFDeleteDishTypeDishCommand : BaseService, IDeleteDishTypeDishCommand
    {
        private readonly IMapper _mapper;
        public EFDeleteDishTypeDishCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
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
