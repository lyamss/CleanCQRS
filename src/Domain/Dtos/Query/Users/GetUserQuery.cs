using Domain.Dtos.AppLayerDtos;
using MediatR;

namespace Domain.Dtos.Query.Users
{
    public record GetUserQuery : IRequest<ApiResponseDto>
    {
        public int Id_User { get; init; }
        public string Email { get; init; }
    }
}