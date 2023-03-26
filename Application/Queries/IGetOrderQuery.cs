using Application.DataTransfer;
using Application.Interfaces;

namespace Application.Queries
{
    public interface IGetOrderQuery : IQuery<int, OrderDTO>
    {
    }
}
