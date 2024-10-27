using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User
    {
        public User() => AccountCreatedAt = DateTime.UtcNow;

        [Key]
        public int Id_User { get; set; }

        [Required]
        [MaxLength(15)]
        public string Pseudo { get; set; }

        [Required]
        [MaxLength(150)]
        public string PasswordHash { get; set; }

        [Required]
        public DateTime AccountCreatedAt { get; private set; }
    }
}