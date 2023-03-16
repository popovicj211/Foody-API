using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderEntity : BaseEntity
    {
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public ICollection<OrderItemEntity> OrderItems { get; set; }
    }
}
