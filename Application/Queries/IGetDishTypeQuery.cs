using Application.DataTransfer;
using Application.Interfaces;

namespace Application.Queries
{
    public interface IGetDishTypeQuery : IQuery<int, DishTypeDTO>
    {
    }
}
