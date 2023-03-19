using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Commands
{
    public class EFAddDishIngredientCommand : BaseService, IAddDishIngredientCommand
    {
        private readonly IMapper _mapper;
        public EFAddDishIngredientCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
 
        public void Execute(DishIngredientDTO request)
        {
            var dish = this._context.Dishes.Include(g => g.DishIngredients).FirstOrDefault(g => g.Id == request.DishId);

            if (dish == null)
            {
                throw new EntityNotFoundException("Dish");
            }

            List<IngredientEntity> ingredients = new List<IngredientEntity>();
            List<DishIngredientEntity> dishIngredients = new List<DishIngredientEntity>();

            foreach (var ingredient in request.IngredientIds)
            {
                var ingredientCheck = _context.Ingredients.Find(ingredient);

                if (ingredientCheck == null)
                {
                    throw new EntityNotFoundException("Ingredient");
                }
                else
                {
                    bool isContainIngredientId = dish.DishIngredients.Any(g => g.IngreId == ingredient);

                    if (isContainIngredientId)
                    {
                        throw new AlreadyExistException();
                    }

                    ingredients.Add(ingredientCheck);
                }

                dishIngredients.Add(new DishIngredientEntity
                {
                    DishId = request.Id,
                    IngreId = ingredient
                });
            }

            this. _context.DishIngredients.AddRange(dishIngredients);
            this._context.SaveChanges();
        }
    }
}
