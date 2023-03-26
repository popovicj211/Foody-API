using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Queriess
{
    public class EFGetOrderQuery : BaseService, IGetOrderQuery
    {
        private readonly IMapper _mapper;

        public EFGetOrderQuery(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public OrderDTO Execute(int id)
        {
            var order = _context.Orders.Where(u => !u.IsDeleted).ProjectTo<OrderDTO>(this._mapper.ConfigurationProvider).Select(u => new OrderDTO
            {
                Id = u.Id,
                TotalPrice = u.TotalPrice,
                Date = u.Date
            }).FirstOrDefault(u => u.Id == id);

            if (order == null)
            {
                throw new EntityNotFoundException("Order");
            }

            return order;
        }
    }
}
