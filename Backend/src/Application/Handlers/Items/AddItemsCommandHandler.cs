using Application.Services;
using Domain.Dtos.AppLayerDtos;
using Domain.Dtos.Commands.Items;
using Domain.Mappers.Items;
using MediatR;

namespace Application.Handlers.Items
{
    internal sealed class AddItemsCommandHandler
        (
        IRepository<Domain.Models.Items> repositoryItemsExtensions,
        ItemsMapper itemsMapper,
        AddItemsCommandValidator validator
        ) : IRequestHandler<AddItemsCommand, ApiResponseDto>
    {
        private readonly IRepository<Domain.Models.Items> _repositoryItemsExtensions = repositoryItemsExtensions;
        private readonly ItemsMapper _itemsMapper = itemsMapper;
        private readonly AddItemsCommandValidator _validator = validator;
        public async Task<ApiResponseDto> Handle(AddItemsCommand command, CancellationToken cancellationToken)
        {
            var regex = await this._validator.ValidateAsync(command, cancellationToken);

            if (!regex.IsValid) 
            {
                return ApiResponseDto.Failure(regex.Errors.Select(e => e.ErrorMessage).ToList());
            }

            var NewItems = new Domain.Models.Items (Guid.NewGuid(), command.Name, command.Description,command.Price, DateTime.UtcNow);

            await this._repositoryItemsExtensions.AddAsync(NewItems, cancellationToken);

            await this._repositoryItemsExtensions.SaveChangesAsync(cancellationToken);

            return ApiResponseDto.Success("Add items succes", this._itemsMapper.ToGetItemsMapper(NewItems));
        }
    }
}