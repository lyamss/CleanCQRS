using Domain.Dtos.AppLayerDtos;
using MediatR;

namespace Domain.Dtos.Query.AuthToken
{
    public record GetAuthTokenQuery : IRequest<ApiResponseDto>
    {
        public string Token { get; init; }
        public DateTime ExpirationDate { get; init; }
    }
}