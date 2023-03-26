using Application.DataTransfer;
using Application.Pagination;
using Application.Queries;
using Application.Searches;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Implementation.EFServices;
using EFDataAccess;
using Application.Helpers;

namespace Implementation.Services.Queriess
{
    public class EFGetIngredientsQuery : BaseService, IGetIngredientsQuery
    {
        private readonly IMapper _mapper;

        public EFGetIngredientsQuery(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public PagedResponse<DishTypeDTO> Execute(BaseSearchRequest request)
        {
            var ingredients = _context.Ingredients.AsQueryable();

            return ingredients.Where(d => !d.IsDeleted).ProjectTo<DishTypeDTO>(this._mapper.ConfigurationProvider).Select(user => new DishTypeDTO
            {
                Id = user.Id,
                Name = user.Name
            }).Paginate(request.PerPage, request.Page);
        }
    }
}
