using Domain.Models;
using Domain.Query.Users;

namespace Domain.Mappers.Users
{
    public class UserMapper
    {
        public GetUserQuery ToGetUserMapper(User user)
        {
            if (user == null)
            {
                return null;
            }

            return new GetUserQuery
            {
                Id_User = user.Id_User,
                Pseudo = user.Pseudo,
            };
        }
    }
}