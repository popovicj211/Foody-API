namespace Domain.Entities
{
    public class IngredientEntity : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<DishIngredientEntity> DishIngredients { get; set; }
    }
}
