using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer
{
    public class UserDTO : BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImagePath { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }
        public int RoleId { get; set; }
        public string JwtToken { get; set; }
        public RoleDTO? Role { get; set; }
        //public List<CommentDTO> Comments { get; set; }
        //public List<OrderDTO> Orders { get; set; }
    }
}
