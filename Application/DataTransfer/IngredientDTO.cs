namespace Application.DataTransfer
{
    public class IngredientDTO : BaseDTO
    {
        public string Name { get; set; }
        public List<DishIngredientDTO> DishIngredients { get; set; }
    }
}
