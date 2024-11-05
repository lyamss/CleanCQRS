using Domain.Dtos.AppLayerDtos;
using MediatR;

namespace Domain.Query.Items
{
    public record class GetItemsQuery : IRequest<ApiResponseDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Id_items { get; set; }
    }
}