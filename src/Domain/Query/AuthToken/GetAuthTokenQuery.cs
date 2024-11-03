using Domain.Dtos.AppLayerDtos;
using MediatR;

namespace Domain.Query.AuthToken
{
    public class GetAuthTokenQuery : IRequest<ApiResponseDto>
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}