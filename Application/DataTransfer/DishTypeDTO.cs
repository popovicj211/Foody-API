using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer
{
    public class DishTypeDTO : BaseDTO
    {
        public string Name { get; set; }
        public List<DishTypeDishDTO> DishTypeDishes { get; set; }
    }
}
