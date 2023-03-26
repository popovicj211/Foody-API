using Application.DataTransfer;
using Application.Interfaces;

namespace Application.Queries
{
    public interface IGetDishQuery : IQuery<int, DishDTO>
    {
    }
}
