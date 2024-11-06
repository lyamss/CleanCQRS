using Domain.Dtos.AppLayerDtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Commands.Items
{
    public record class AddItemsCommand : IRequest<ApiResponseDto>
    {
        public AddItemsCommand(double Price, string Description, string Name) 
        { 
            this.Price = Price;
            this.Description = Description;
            this.Name = Name;
        }

        [Required]
        public double Price { get; private set; }

        [Required]
        public string Description { get; private set; }

        [Required]
        public string Name { get; private set; }
    }
}