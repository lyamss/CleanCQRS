using Application.Services;
using Domain.Dtos.AppLayerDtos;
using Domain.Dtos.Commands.Items;
using Domain.Mappers.Items;
using MediatR;

namespace Application.Handlers.Items
{
    internal sealed class GetItemByIdCommandHandler
    (
        IRepository<Domain.Models.Items> repositoryItemsExtensions,
        ItemsMapper itemsMapper,
        IdDtoValidator validator
    ) 
        : IRequestHandler<GetItemByIdCommand, ApiResponseDto>
    {
        private readonly IRepository<Domain.Models.Items> _repositoryItemsExtensions = repositoryItemsExtensions;
        private readonly ItemsMapper _itemsMapper = itemsMapper;
        private readonly IdDtoValidator _validator = validator;
        public async Task<ApiResponseDto> Handle(GetItemByIdCommand command, CancellationToken cancellationToken)
        {
            var rsl = await this._validator.ValidateAsync(command.ById, cancellationToken);

            if(!rsl.IsValid)
            {
                return ApiResponseDto.Failure(rsl.Errors.Select(e => e.ErrorMessage).ToList());
            }

            Domain.Models.Items itm = await this._repositoryItemsExtensions.GetByIdAsync(command.ById, cancellationToken);

            if (itm is null)
            {
                return ApiResponseDto.Failure("item no exist");
            }

            var itemDto = this._itemsMapper.ToGetItemsMapper(itm);

            return ApiResponseDto.Success("Item found", itemDto);
        }
    }
}