using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class UserResetInfo
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ResetPasswordToken { get; set; }

        public string Password { get; set; }

    }
}
