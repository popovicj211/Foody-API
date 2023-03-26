using Application.DataTransfer;
using Application.Pagination;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using Implementation.EFServices;
using EFDataAccess;
using Application.Helpers;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;

namespace Implementation.Services.Queriess
{
    public class EFGetDishesQuery : BaseService, IGetDishesQuery
    {
        private readonly IMapper _mapper;

        public EFGetDishesQuery(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public PagedResponse<DishDTO>? Execute(BaseSearchRequest request)
        {
            var dishes = this._context.Dishes.AsQueryable();

            if (!dishes.Any())
            {
                throw new EntityNotFoundException("Dishes");
            }

            return dishes.Include(d => d.DishIngredients).ThenInclude(d => d.Ingredient).Where(d => !d.IsDeleted).Select(dish => new DishDTO
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                ImagePath = dish.ImagePath,
                Price = dish.Price,
                Ingredients = dish.DishIngredients.Select(g => g.Ingredient.Name).ToList(),
                TypeDishes = dish.DishTypeDishes.Select(g => g.DishType.Name).ToList(),
            }).Paginate(request.PerPage, request.Page);
        }
    }
}
