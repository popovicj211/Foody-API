﻿namespace Domain.Entities
{
    public class ContactEntity : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
