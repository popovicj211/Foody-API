﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImagePath { get; set; }
        public string? Token { get; set; }
        public bool IsActived { get; set; }
        public int RoleId { get; set; }
        public RoleEntity Role { get; set; }
        public ICollection<CommentEntity> Comments { get; set; }
        public ICollection<OrderEntity> Orders { get; set; }
    }
}
