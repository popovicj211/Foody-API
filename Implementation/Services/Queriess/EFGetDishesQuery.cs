using Application.DataTransfer;
using Application.Pagination;
using Application.Queries;
using Application.Searches;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Implementation.EFServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDataAccess;
using Application.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Implementation.Services.Queriess
{
    public class EFGetDishesQuery : BaseService, IGetDishesQuery
    {
        private readonly IMapper _mapper;
        public EFGetDishesQuery(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public PagedResponse<DishDTO> Execute(BaseSearchRequest request)
        {
            var dieshes = _context.Dishes.AsQueryable();

            return dieshes.Where(d => !d.IsDeleted).ProjectTo<DishDTO>(_mapper.ConfigurationProvider).Select(dish => new DishDTO
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                ImagePath = dish.ImagePath
            }).Paginate(request.PerPage, request.Page);
        }
    }
}
