using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using AutoMapper;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Queriess
{
    public class EFGetDishQuery : BaseService, IGetDishQuery
    {
        private readonly IMapper _mapper;

        public EFGetDishQuery(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public DishDTO Execute(int id)
        {
            var dish = this._context.Dishes.Where(u => !u.IsDeleted).Select(dish => new DishDTO
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                ImagePath = dish.ImagePath,
                Price = dish.Price,
                Ingredients = dish.DishIngredients.Select(g => g.Ingredient.Name).ToList(),
                TypeDishes = dish.DishTypeDishes.Select(g => g.DishType.Name).ToList()
            }).FirstOrDefault(u => u.Id == id);

            if (dish == null)
            {
                throw new EntityNotFoundException("Dish");
            }

            return dish;
        }
    }
}
