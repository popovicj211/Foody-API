﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RoleEntity : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<UserEntity> Users { get; set; }
    }
}
