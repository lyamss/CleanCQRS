using Domain.Dtos.AppLayerDtos;
using MediatR;

namespace Domain.Dtos.Query.Items
{
    public record GetItemsQuery : IRequest<ApiResponseDto>
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public double Price { get; init; }
        public Guid Id_items { get; init; }

        public DateTime CreatedAt { get; init; }
    }
}