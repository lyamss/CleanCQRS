using Domain.Dtos.AppLayerDtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Domain.Commands.Items
{
    public class AddItemsCommand : IRequest<ApiResponseDto>
    {
        [Required]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Name { get; set; }
    }
}