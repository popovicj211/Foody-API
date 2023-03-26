namespace Domain.Entities
{
    public class DishIngredientEntity : BaseEntity
    {
        public int DishId { get; set; }
        public int IngreId { get; set; }
        public DishEntity Dish { get; set; }
        public IngredientEntity Ingredient { get; set; }
    }
}
