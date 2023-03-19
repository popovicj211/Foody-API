using Application.DataTransfer;
using Application.Interfaces;

namespace Application.Commands
{
    public interface IDeleteDishIngredientCommand : ICommand<DishIngredientDTO>
    {
    }
}
