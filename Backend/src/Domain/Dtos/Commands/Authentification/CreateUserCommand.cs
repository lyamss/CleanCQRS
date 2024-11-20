using Domain.Dtos.AppLayerDtos;
using MediatR;
using System.ComponentModel.DataAnnotations;
namespace Domain.Dtos.Commands.Authentification
{
    public record CreateUserCommand : IRequest<ApiResponseDto>
    {
        [Required]
        [MaxLength(64)]
        public string Email { get; init; }

        [Required]
        [MaxLength(100)]
        public string Password { get; init; }
    }
}