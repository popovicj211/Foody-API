using Application.DataTransfer;
using Application.Interfaces;
using Application.Pagination;
using Application.Searches;

namespace Application.Queries
{
    public interface IGetOrdersQuery : IQuery<BaseSearchRequest, PagedResponse<OrderDTO>>
    {
    }
}
