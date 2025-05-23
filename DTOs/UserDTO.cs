﻿using System.ComponentModel.DataAnnotations;

namespace MovieApplication.DTOs
{
    public class UserDTO
    {

        [Required]  
        public string userName { get; set; }
        
        [Required]
        public string userEmail { get; set; }
        [Required]
        [StringLength(100,MinimumLength = 8)]
        public string password { get; set; }
    }
}
