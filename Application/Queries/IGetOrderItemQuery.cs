using Application.DataTransfer;
using Application.Interfaces;

namespace Application.Queries
{
    public interface IGetOrderItemQuery : IQuery<int, OrderItemDTO>
    {
    }
}
