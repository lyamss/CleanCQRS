using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class AuthToken
    {
        public AuthToken(DateTime ExpirationDate, string token, int IdUser)
        {
            this.EmissionDate = DateTime.UtcNow;
            this.ExpirationDate = ExpirationDate;
            this.IdUser = IdUser;
            this.Token = token;
        }

        public void UpdateAuthToken(AuthToken authToken, DateTime? ExpirationDate, string token = null)
        {
            this.ExpirationDate = ExpirationDate ?? authToken.ExpirationDate;
            this.Token = token ?? authToken.Token;
        }

        [Key, Required]
        public int Id_AuthToken { get; private set; }

        [Required]
        public DateTime EmissionDate { get; private set; }

        [Required]
        public DateTime ExpirationDate { get; private set; }

        [Required]
        public string Token { get; private set; }

        [Required]
        public int IdUser { get; private set; }

        public User User { get; set; }
    }
}