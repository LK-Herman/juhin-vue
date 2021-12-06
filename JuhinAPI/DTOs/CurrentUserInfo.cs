using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class CurrentUserInfo
    {
        public string UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string UserRole { get; set; }

    }
}
