using System.ComponentModel.DataAnnotations;

namespace H3ServersideProject.Models
{
    public class User
    {
        [Required(ErrorMessage = "Navn skal udfyldes")]
        [MaxLength(40)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Adresse skal udfyldes")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Nummer skal udfyldes")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Må ikke indeholde bogstaver.")]
        [MaxLength(12)]
        [MinLength(8, ErrorMessage = "Skal minimum være på 8 karakter")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email skal udfyldes")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Kodeord skal udfyldes")]
        [MinLength(8, ErrorMessage = "Skal minimum være på 8 karakter")]
        public string? Password { get; set; }
    }
}
