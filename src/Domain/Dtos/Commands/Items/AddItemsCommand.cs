using Domain.Dtos.AppLayerDtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Commands.Items
{
    public record AddItemsCommand : IRequest<ApiResponseDto>
    {
        [Required]
        public double Price { get; init; }

        [Required]
        public string Description { get; init; }

        [Required]
        public string Name { get; init; }
    }
}