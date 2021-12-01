﻿using System.ComponentModel.DataAnnotations;

namespace Timetable.Auth.Dto
{
    public class LoginRequest
    {
        [Required]
        [StringLength(4)]
        [MaxLength(16)]
        public string UserName { get; set; }

        [StringLength(8)]
        [MaxLength(16)]
        [Required]
        public string Password { get; set; }
    }
}