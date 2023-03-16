using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer
{
    public class OrderDTO : BaseDTO
    {
        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
