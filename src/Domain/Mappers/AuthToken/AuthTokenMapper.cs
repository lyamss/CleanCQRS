using Domain.Query.AuthToken;

namespace Domain.Mappers.AuthToken
{
    public class AuthTokenMapper
    {
        public GetAuthTokenQuery ToGetAuthTokenMapper(Domain.Models.AuthToken authToken)
        {
            if (authToken == null)
            {
                return null;
            }

            return new GetAuthTokenQuery
            {
                Token = authToken.Token,
                ExpirationDate = authToken.ExpirationDate,
            };
        }
    }
}