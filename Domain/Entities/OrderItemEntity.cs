using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderItemEntity : BaseEntity
    {
        public int DishId { get; set; }
        public int OrderId { get; set; }
        public DishEntity Dish { get; set; }
        public OrderEntity Order { get; set; }
        public int Qty { get; set; }
    }
}
