using Application.DataTransfer;
using Application.Helpers;
using Application.Pagination;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFDataAccess;
using Implementation.EFServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Queriess
{
    public class EFGetDishTypesQuery : BaseService, IGetDishTypesQuery
    {
        private readonly IMapper _mapper;
        public EFGetDishTypesQuery(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public PagedResponse<DishTypeDTO> Execute(BaseSearchRequest request)
        {
            var ingredients = _context.DishTypes.AsQueryable();

            return ingredients.Where(d => !d.IsDeleted).ProjectTo<DishTypeDTO>(_mapper.ConfigurationProvider).Select(user => new DishTypeDTO
            {
                Id = user.Id,
                Name = user.Name
            }).Paginate(request.PerPage, request.Page);
        }
    }
}
