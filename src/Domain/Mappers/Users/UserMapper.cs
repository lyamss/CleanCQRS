using Domain.Models;
using Domain.Query.Users;

namespace Domain.Mappers.Users
{
    public sealed record class UserMapper
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
                Email = user.Email,
            };
        }
    }
}