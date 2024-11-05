using Domain.Dtos.AppLayerDtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Domain.Commands.Items
{
    public record class GetItemsCommand : IRequest<ApiResponseDto>
    {
        [Required]
        public int ItemIdToGet { get; private set; }

        public GetItemsCommand(int itemIdToGet)
        {
            this.ItemIdToGet = itemIdToGet;
        }
    } 
}