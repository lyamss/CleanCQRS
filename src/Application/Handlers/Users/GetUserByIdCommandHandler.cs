using Application.Services;
using Domain.Dtos.AppLayerDtos;
using Domain.Dtos.Commands;
using Domain.Dtos.Query.Users;
using MediatR;

namespace Application.Handlers.Users
{
    internal sealed class GetUserByIdCommandHandler
        (
        IdDtoValidator validator,
        IAddOrGetCacheSvsScoped addOrGetCache
        )
        : IRequestHandler<ByIdCommand, ApiResponseDto>
    {
        private readonly IdDtoValidator _validator = validator;
        private readonly IAddOrGetCacheSvsScoped _addOrGetCacheSvsScoped = addOrGetCache;

        public async Task<ApiResponseDto> Handle(ByIdCommand command, CancellationToken cancellationToken)
        {
            var rsl = await this._validator.ValidateAsync(command.ById, cancellationToken);

            if (!rsl.IsValid)
            {
                return ApiResponseDto.Failure(rsl.Errors.Select(e => e.ErrorMessage).ToList());
            }

            GetUserQuery usr = await this._addOrGetCacheSvsScoped.GetUserByIdAsyncCache(command.ById, cancellationToken);

            if (usr is null)
            {
                return ApiResponseDto.Failure("User no exist");
            }

            return ApiResponseDto.Success("User found", usr);
        }
    }
}