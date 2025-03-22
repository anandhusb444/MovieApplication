﻿using System.ComponentModel.DataAnnotations;

namespace MovieApplication.Models
{
    public class Users
    {
        [Key]
        public Guid id { get; private set; }
        public string userName { get; private set; }
        public string role { get; set; }
        public string email { get; private set; }
        public string passwordHash { get; private set; }
        public DateTimeOffset createdAt { get; set; }
        public DateTimeOffset updateAt { get; set; }
        public ICollection<Commands> Commands { get; set; }

        

    }
}
