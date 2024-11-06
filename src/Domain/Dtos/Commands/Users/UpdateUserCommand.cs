using Domain.Dtos.AppLayerDtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Commands.Users
{
    public record class UpdateUserCommand : IRequest<ApiResponseDto>
    {
        public UpdateUserCommand(int IdUser, string Email, string Password)
        {
            this.IdUser = IdUser;
            this.Email = Email;
            this.Password = Password;
        }

        [Required]
        public int IdUser { get; private set; }

        [MaxLength(64)]
        public string? Email { get; private set; }

        [MaxLength(100)]
        public string? Password { get; private set; }
    }
}