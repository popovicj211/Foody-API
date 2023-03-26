using Application.DataTransfer;
using Application.Helpers;
using Application.Pagination;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Queriess
{
    public class EFGetDishTypesQuery : BaseService, IGetDishTypesQuery
    {
        private readonly IMapper _mapper;

        public EFGetDishTypesQuery(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public PagedResponse<DishTypeDTO> Execute(BaseSearchRequest request)
        {
            var dishTypes = this._context.DishTypes.AsQueryable();

            return dishTypes.Where(d => !d.IsDeleted).ProjectTo<DishTypeDTO>(this._mapper.ConfigurationProvider).Select(user => new DishTypeDTO
            {
                Id = user.Id,
                Name = user.Name
            }).Paginate(request.PerPage, request.Page);
        }
    }
}
