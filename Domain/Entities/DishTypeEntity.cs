namespace Domain.Entities
{
    public class DishTypeEntity : BaseEntity
    {
        public string Name { get; set; }
       public ICollection<DishTypeDishEntity> DishTypeDishes { get; set; }
    }
}
