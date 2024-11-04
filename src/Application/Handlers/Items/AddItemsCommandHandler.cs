using Domain.Commands.Items;
using Domain.Dtos.AppLayerDtos;
using Domain.Mappers.Items;
using MediatR;

namespace Application.Handlers.Items
{
    internal class AddItemsCommandHandler
        (
        IRepository<Domain.Models.Items> repositoryItemsExtensions,
        ItemsMapper itemsMapper
        ) : IRequestHandler<AddItemsCommand, ApiResponseDto>
    {
        private readonly IRepository<Domain.Models.Items> _repositoryItemsExtensions = repositoryItemsExtensions;
        private readonly ItemsMapper _itemsMapper = itemsMapper;
        public async Task<ApiResponseDto> Handle(AddItemsCommand command, CancellationToken cancellationToken)
        {
            var NewItems = new Domain.Models.Items
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
            };

            await this._repositoryItemsExtensions.AddAsync(NewItems, cancellationToken);

            await this._repositoryItemsExtensions.SaveChangesAsync(cancellationToken);

            return ApiResponseDto.Success("Add items succes", this._itemsMapper.ToGetItemsMapper(NewItems));
        }
    }
}