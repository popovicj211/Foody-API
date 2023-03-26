namespace Domain.Entities
{
    public class DishTypeDishEntity : BaseEntity
    {
        public int DishTypeId { get; set; }
        public int DishId { get; set; }
        public DishTypeEntity DishType { get; set; }
        public DishEntity Dish { get; set; }
    }
}
