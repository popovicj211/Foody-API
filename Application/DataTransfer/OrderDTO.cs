namespace Application.DataTransfer
{
    public class OrderDTO : BaseDTO
    {
        public OrderDTO()
        {
            Dishes = new List<DishDTO>();
            Quantities = new List<int>();
        }

        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public List<DishDTO> Dishes { get; set; }
        public List<int> Quantities { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
