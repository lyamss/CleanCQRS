using Domain.Dtos.AppLayerDtos;
using MediatR;

namespace Domain.Dtos.Query.Users
{
    public record GetUserQuery : IRequest<ApiResponseDto>
    {
        public Guid Id_User { get; init; }
        public string Email { get; init; }
        public DateTime AccountCreatedAt { get; init; }
    }
}