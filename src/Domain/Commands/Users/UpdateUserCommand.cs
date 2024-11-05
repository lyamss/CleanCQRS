using Domain.Dtos.AppLayerDtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Domain.Commands.Users
{
    public record class UpdateUserCommand : IRequest<ApiResponseDto>
    {
        [Required]
        public int IdUser { get; set; }

        [MaxLength(64)]
        public string? Email { get; set; }

        [MaxLength(100)]
        public string? Password { get; set; }
    }
}