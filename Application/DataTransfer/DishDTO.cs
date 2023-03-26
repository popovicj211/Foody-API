using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
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
        //public List<DishIngredientDTO> DishIngredients { get; set; }
        //public List<DishCommentDTO> DishComments { get; set; }
        //public List<OrderItemDTO> OrderItems { get; set; }
        //public List<DishTypeDishDTO> DishTypeDishes { get; set; }
    }
}
