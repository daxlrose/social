﻿using System.ComponentModel.DataAnnotations;

namespace Social.Api.Contracts.Identity
{
    public class Login
    {
        [EmailAddress]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
