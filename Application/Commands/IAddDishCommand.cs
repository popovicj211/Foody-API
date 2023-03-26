using Application.DataTransfer;
using Application.Interfaces;

namespace Application.Commands
{
    public interface IAddDishCommand : ICommandAsync<DishDTO>
    {
    }
}
