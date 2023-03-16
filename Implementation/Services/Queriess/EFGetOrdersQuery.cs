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

namespace Implementation.Services.Queriess
{
    public class EFGetOrdersQuery : BaseService, IGetOrdersQuery
    {
        private readonly IMapper _mapper;
        public EFGetOrdersQuery(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public PagedResponse<OrderDTO> Execute(BaseSearchRequest request)
        {
            var orders = _context.Orders.AsQueryable();

            return orders.Where(d => !d.IsDeleted).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).Select(u => new OrderDTO
            {
                Id = u.Id,
                TotalPrice = u.TotalPrice,
                Date = u.Date
            }).Paginate(request.PerPage, request.Page);
        }
    }
}
