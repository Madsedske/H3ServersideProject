using System.ComponentModel.DataAnnotations;

namespace H3ServersideProject.Models
{
    public class Login
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        [MinLength(8)]
        public string? Password { get; set; }
    }
}
