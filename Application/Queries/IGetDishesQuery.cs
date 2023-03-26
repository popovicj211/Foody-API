using Application.DataTransfer;
using Application.Interfaces;
using Application.Pagination;
using Application.Searches;

namespace Application.Queries
{
    public interface IGetDishesQuery : IQuery<BaseSearchRequest, PagedResponse<DishDTO>>
    {
    }
}
