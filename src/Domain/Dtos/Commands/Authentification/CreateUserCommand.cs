using Domain.Dtos.AppLayerDtos;
using MediatR;
using System.ComponentModel.DataAnnotations;
namespace Domain.Dtos.Commands.Authentification
{
    public record class CreateUserCommand : IRequest<ApiResponseDto>
    {
        public CreateUserCommand(string Email, string Password)
        {
            this.Email = Email;
            this.Password = Password;
        }

        [Required]
        [MaxLength(64)]
        public string Email { get; private set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; private set; }
    }
}