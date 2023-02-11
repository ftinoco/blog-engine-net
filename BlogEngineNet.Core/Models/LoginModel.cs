using System.ComponentModel.DataAnnotations;

namespace BlogEngineNet.Core.Models
{
    public class LoginModel
    {
        [Required]
        [MaxLength(8)]
        public string Username { get; set; }

        [Required]
        [MaxLength(16)]
        public string Password { get; set; }
    }
}