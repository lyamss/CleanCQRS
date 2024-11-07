using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User
    {
        public User () { }

        public User(Guid UserId, string email, string PasswordHash, DateTime AccountCreatedAt)
        {
            this.Id_User = UserId;
            this.AccountCreatedAt = AccountCreatedAt;
            this.Email = email;
            this.PasswordHash = PasswordHash;
        }

        public void UpdateUser(User usr, string email, string passwordHash)
        {
            this.Email = email ?? usr.Email;
            this.PasswordHash= passwordHash ?? usr.PasswordHash;
        }

        [Key, Required]
        public Guid Id_User { get; private set; }

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