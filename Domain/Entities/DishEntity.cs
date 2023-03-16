using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DishEntity : BaseEntity
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string ImageFullPath { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<DishIngredientEntity> DishIngredients { get; set; }
        public ICollection<DishCommentEntity> DishComments { get; set; }
        public ICollection<OrderItemEntity> OrderItems { get; set; }
        public ICollection<DishTypeDishEntity> DishTypeDishes { get; set; }
    }
}
