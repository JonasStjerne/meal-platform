using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Food_Like.Shared
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string EncryptedPassword { get; set; }
    }
}
