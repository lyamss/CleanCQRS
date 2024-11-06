using Domain.Dtos.AppLayerDtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Commands.Users
{
    public record class UpdateUserCommand : IRequest<ApiResponseDto>
    {
        [Required]
        public int IdUser { get; init; }

        [MaxLength(64)]
        public string? Email { get; init; }

        [MaxLength(100)]
        public string? Password { get; init; }
    }
}