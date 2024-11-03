using Domain.Dtos.AppLayerDtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Domain.Commands.Users
{
    public class RemoveUserByIdCommand : IRequest<ApiResponseDto>
    {
        [Required]
        public int UserIdToDelete { get; private set; }

        public RemoveUserByIdCommand(int userIdToDelete) 
        {
            UserIdToDelete = userIdToDelete;
        }
    }
}