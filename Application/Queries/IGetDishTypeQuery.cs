using Application.DataTransfer;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public interface IGetDishTypeQuery : IQuery<int, DishTypeDTO>
    {
    }
}
