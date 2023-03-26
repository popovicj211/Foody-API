namespace Application.DataTransfer
{
    public class DishIngredientDTO : BaseDTO
    {
        public DishIngredientDTO()
        {
            IngredientIds = new List<int>();
        }

        public List<int> IngredientIds { get; set; }
        public int DishId { get; set; }
        public int IngreId { get; set; }
        public DishDTO Dish { get; set; }
        public DishTypeDTO  Ingredient { get; set; }
    }
}
