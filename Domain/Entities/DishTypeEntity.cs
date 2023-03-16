using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DishTypeEntity : BaseEntity
    {
        public string Name { get; set; }
       public ICollection<DishTypeDishEntity> DishTypeDishes { get; set; }
    }
}
