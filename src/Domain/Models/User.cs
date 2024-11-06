using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User
    {
        public User(string email, string PasswordHash)
        {
            this.AccountCreatedAt = DateTime.UtcNow;
            this.Email = email;
            this.PasswordHash = PasswordHash;
        }

        public User(User usr, string email, string passwordHash)
        {
            this.Email = email ?? usr.Email;
            this.PasswordHash= passwordHash ?? usr.PasswordHash;
        }

        [Key, Required]
        public int Id_User { get; private set; }

        [Required]
        [MaxLength(64)]
        public string Email { get; private set; }

        [Required]
        [MaxLength(150)]
        public string PasswordHash { get; private set; }

        [Required]
        public DateTime AccountCreatedAt { get; private set; }

        public ICollection<AuthToken> AuthToken { get; set; }
        public ICollection<Transaction> Transaction { get; set; }
    }
}