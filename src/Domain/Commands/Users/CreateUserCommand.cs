using Domain.Dtos.AppLayerDtos;
using MediatR;
using System.ComponentModel.DataAnnotations;
namespace Domain.Commands.Users
{
    public class CreateUserCommand : IRequest<ApiResponseDto>
    {
        [Required]
        [MaxLength(15)]
        public string Pseudo { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}