using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class AuthToken
    {
        public AuthToken() => this.EmissionDate = DateTime.UtcNow;

        [Key, Required]
        public int Id_AuthToken { get; set; }

        [Required]
        public DateTime EmissionDate { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public int IdUser { get; set; }

        public User User { get; set; }
    }
}