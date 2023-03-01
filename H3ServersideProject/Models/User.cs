using System.ComponentModel.DataAnnotations;

namespace H3ServersideProject.Models
{
    public class User
    {
        [Required]
        [MaxLength(40)]
        public string? Name { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Må ikke indeholde bogstaver.")]
        [MaxLength(12)]
        [MinLength(8)]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        [MinLength(8)]
        public string? Password { get; set; }
    }
}
