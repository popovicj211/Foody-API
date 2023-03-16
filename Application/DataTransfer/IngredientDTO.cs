using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer
{
    public class IngredientDTO : BaseDTO
    {
        public string Name { get; set; }
        public List<DishIngredientDTO> DishIngredients { get; set; }
    }
}
