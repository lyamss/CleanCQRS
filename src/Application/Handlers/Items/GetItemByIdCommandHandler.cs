using Domain.Commands.Items;
using Domain.Dtos.AppLayerDtos;
using Domain.Mappers.Items;
using MediatR;

namespace Application.Handlers.Items
{
    internal sealed class GetItemByIdCommandHandler
        (
        IRepository<Domain.Models.Items> repositoryItemsExtensions,
        ItemsMapper itemsMapper
        ) : IRequestHandler<GetItemsCommand, ApiResponseDto>
    {
        private readonly IRepository<Domain.Models.Items> _repositoryItemsExtensions = repositoryItemsExtensions;
        private readonly ItemsMapper _itemsMapper = itemsMapper;
        public async Task<ApiResponseDto> Handle(GetItemsCommand command, CancellationToken cancellationToken)
        {
            Domain.Models.Items itm = await this._repositoryItemsExtensions.GetByIdAsync(command.ItemIdToGet, cancellationToken);

            if (itm is null)
            {
                return ApiResponseDto.Failure("item no exist");
            }

            var itemDto = this._itemsMapper.ToGetItemsMapper(itm);

            return ApiResponseDto.Success("Item found", itemDto);
        }
    }
}