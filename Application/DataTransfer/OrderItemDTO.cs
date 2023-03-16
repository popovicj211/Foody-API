using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer
{
    public class OrderItemDTO : BaseDTO
    {
        public int DishId { get; set; }
        public int OrderId { get; set; }
        public DishDTO Dish { get; set; }
        public OrderDTO Order { get; set; }
        public int Qty { get; set; }
    }
}
