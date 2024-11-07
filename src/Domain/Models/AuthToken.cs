using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class AuthToken
    {
        public AuthToken()
        {
        }

        public AuthToken(Guid id_AuthToken, DateTime EmissionDate, DateTime ExpirationDate, string token, Guid IdUser)
        {
            this.Id_AuthToken = id_AuthToken;
            this.EmissionDate = EmissionDate;
            this.ExpirationDate = ExpirationDate;
            this.IdUser = IdUser;
            this.Token = token;
        }

        public void UpdateAuthToken(AuthToken authToken, DateTime? ExpirationDate, string token)
        {
            this.ExpirationDate = ExpirationDate ?? authToken.ExpirationDate;
            this.Token = token ?? authToken.Token;
        }

        [Key, Required]
        public Guid Id_AuthToken { get; private set; }

        [Required]
        public DateTime EmissionDate { get; private set; }

        [Required]
        public DateTime ExpirationDate { get; private set; }

        [Required]
        public string Token { get; private set; }

        [Required]
        public Guid IdUser { get; private set; }

        public User User { get; set; }
    }
}