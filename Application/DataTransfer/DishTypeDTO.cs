namespace Application.DataTransfer
{
    public class DishTypeDTO : BaseDTO
    {
        public string Name { get; set; }
        public List<DishTypeDishDTO> DishTypeDishes { get; set; }
    }
}
