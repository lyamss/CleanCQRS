using Domain.Dtos.AppLayerDtos;
using Domain.Dtos.Query.Items;
using Domain.Mappers.Items;
using MediatR;

namespace Application.Handlers.Items
{
    internal sealed class GetAllItemsCommandHandler
        (
        IRepository<Domain.Models.Items> repositoryExtensionsItems,
        ItemsMapper itemsMapper
        )
        : IRequestHandler<GetItemsQuery, ApiResponseDto>
    {
        private readonly IRepository<Domain.Models.Items> _repositoryExtensionsItems = repositoryExtensionsItems;
        private readonly ItemsMapper _itemsMapper = itemsMapper;
        public async Task<ApiResponseDto> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Models.Items> Allitems = await this._repositoryExtensionsItems.GetAllAsync(cancellationToken);

            if(!Allitems.Any())
            {
                return ApiResponseDto.Failure("No items exist in DB");
            }

            return ApiResponseDto.Success("Items found in DB", Allitems.Select(item => this._itemsMapper.ToGetItemsMapper(item)).ToList());
        }
    }
}