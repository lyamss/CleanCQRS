using Domain.Dtos.AppLayerDtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Domain.Commands.Users
{
    public class GetUserByIdCommand : IRequest<ApiResponseDto>
    {
        [Required]
        public int UserIdToGet { get; private set; }

        public GetUserByIdCommand(int userIdToGet)
        {
            UserIdToGet = userIdToGet;
        }
    }
}