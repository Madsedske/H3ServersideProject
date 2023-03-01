using System.ComponentModel.DataAnnotations;

namespace H3ServersideProject.Models
{
    public class UserPassword
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
