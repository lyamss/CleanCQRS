using Application.Services;
using Domain.Dtos.AppLayerDtos;
using Domain.Dtos.Commands.Users;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Users
{
    internal sealed class RemoveUserByIdCommandHandler
        (
        IRepository<User> UserRepositoryExtensions,
        IdDtoValidator validator
        )
        : IRequestHandler<RemoveUserByIdCommand, ApiResponseDto>
    {
        private readonly IRepository<User> _UserRepositoryExtensions = UserRepositoryExtensions;
        private readonly IdDtoValidator _validator = validator;
        public async Task<ApiResponseDto> Handle(RemoveUserByIdCommand command, CancellationToken cancellationToken)
        {
            var rsl = await this._validator.ValidateAsync(command.ById, cancellationToken);

            if (!rsl.IsValid)
            {
                return ApiResponseDto.Failure(rsl.Errors.Select(e => e.ErrorMessage).ToList());
            }

            User usr = await this._UserRepositoryExtensions.GetByIdAsync(command.ById, cancellationToken);

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