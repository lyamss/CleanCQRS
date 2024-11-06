using Domain.Dtos.AppLayerDtos;
using MediatR;

namespace Domain.Dtos.Query.AuthToken
{
    public record class GetAuthTokenQuery : IRequest<ApiResponseDto>
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}