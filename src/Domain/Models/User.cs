using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User
    {
        public User() => AccountCreatedAt = DateTime.UtcNow;

        [Key, Required]
        public int Id_User { get; set; }

        [Required]
        [MaxLength(64)]
        public string Email { get; set; }

        [Required]
        [MaxLength(150)]
        public string PasswordHash { get; set; }

        [Required]
        public DateTime AccountCreatedAt { get; set; }

        public ICollection<AuthToken> AuthToken { get; set; }
        public ICollection<Transaction> Transaction { get; set; }
    }
}