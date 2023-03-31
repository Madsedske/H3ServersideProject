using System.ComponentModel.DataAnnotations;

namespace H3ServersideProject.Models
{
    public class UserDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
