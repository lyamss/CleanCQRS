using Domain.Commands.Users;
using Domain.Dtos.AppLayerDtos;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Users
{
    internal sealed class RemoveUserByIdCommandHandler
        (
        IRepository<User> UserRepositoryExtensions
        ) 
        : IRequestHandler<RemoveUserByIdCommand, ApiResponseDto>
    {
        private readonly IRepository<User> _UserRepositoryExtensions = UserRepositoryExtensions;
        public async Task<ApiResponseDto> Handle(RemoveUserByIdCommand command, CancellationToken cancellationToken)
        {
            User usr = await this._UserRepositoryExtensions.GetByIdAsync(command.UserIdToDelete, cancellationToken);

            if(usr is null)
            {
                return ApiResponseDto.Failure("User no exist");
            }

            this._UserRepositoryExtensions.Delete(usr);

            await this._UserRepositoryExtensions.SaveChangesAsync(cancellationToken);

            return ApiResponseDto.Success("User delete succes :)", null);
        }
    }
}