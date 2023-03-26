using Microsoft.AspNetCore.Http;
namespace Application.DataTransfer
{
    public class DishDTO : BaseDTO
    {
        public DishDTO()
        {
            Ingredients = new List<string>();
            TypeDishes = new List<string>();
        }

        public string Name { get; set; }
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> TypeDishes { get; set; }
    }
}
