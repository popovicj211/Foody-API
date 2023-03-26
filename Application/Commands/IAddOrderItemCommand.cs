using Application.DataTransfer;
using Application.Interfaces;

namespace Application.Commands
{
    public interface IAddOrderItemCommand : ICommand<List<OrderItemDTO>>
    {
    }
}
