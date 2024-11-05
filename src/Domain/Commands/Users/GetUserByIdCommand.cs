using Domain.Dtos.AppLayerDtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Domain.Commands.Users
{
    public record class GetUserByIdCommand : IRequest<ApiResponseDto>
    {
        [Required]
        public int UserIdToGet { get; private set; }

        public GetUserByIdCommand(int userIdToGet)
        {
            this.UserIdToGet = userIdToGet;
        }
    }
}