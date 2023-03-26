using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Queriess
{
    public class EFGetIngredientQuery : BaseService, IGetIngredientQuery
    {
        private readonly IMapper _mapper;

        public EFGetIngredientQuery(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public DishTypeDTO Execute(int id)
        {
            var ingredient = _context.Ingredients.Where(u => !u.IsDeleted).ProjectTo<DishTypeDTO>(this._mapper.ConfigurationProvider).Select(u => new DishTypeDTO
            {
                Id = u.Id,
                Name = u.Name
            }).FirstOrDefault(u => u.Id == id);

            if (ingredient == null)
            {
                throw new EntityNotFoundException("Ingredient");
            }

            return ingredient;
        }
    }
}
