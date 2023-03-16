using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
