using Application.DataTransfer;
using Application.Interfaces;

namespace Application.Queries
{
    public interface IGetIngredientQuery : IQuery<int, DishTypeDTO>
    {
    }
}
