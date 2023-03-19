using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer
{
    public class DishTypeDishDTO : BaseDTO
    {
        public DishTypeDishDTO()
        {
            DishTypeIds = new List<int>();
        }

        public List<int> DishTypeIds { get; set; }
        public int DishId { get; set; }
        public int TypeId { get; set; }
        public DishDTO Dish { get; set; }
        public DishTypeDTO DishType { get; set; }
    }
}
