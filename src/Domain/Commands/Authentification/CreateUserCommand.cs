using Domain.Dtos.AppLayerDtos;
using MediatR;
using System.ComponentModel.DataAnnotations;
namespace Domain.Commands.Authentification
{
    public class CreateUserCommand : IRequest<ApiResponseDto>
    {
        [Required]
        [MaxLength(64)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}