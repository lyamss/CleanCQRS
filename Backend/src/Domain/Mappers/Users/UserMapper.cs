using Domain.Dtos.Query.Users;
using Domain.Models;

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

            return new GetUserQuery()
            {
                Id_User = user.Id_User,
                Email = user.Email,
                AccountCreatedAt = user.AccountCreatedAt,
            };
        }


        public GetUserQuery2 ToGetUserAndGetPasswordMapper(User user)
        {
            if (user == null)
            {
                return null;
            }

            return new GetUserQuery2()
            {
                Id_User = user.Id_User,
                Email = user.Email,
                AccountCreatedAt = user.AccountCreatedAt,
                PasswordHash = user.PasswordHash,
            };
        }


        public GetUserQuery ToGetUserAndWithoutGetPasswordMapper(GetUserQuery2 getUserQuery2)
        {
            if (getUserQuery2 == null)
            {
                return null;
            }

            return new GetUserQuery()
            {
                Id_User = getUserQuery2.Id_User,
                Email = getUserQuery2.Email,
                AccountCreatedAt = getUserQuery2.AccountCreatedAt,
            };
        }
    }
}