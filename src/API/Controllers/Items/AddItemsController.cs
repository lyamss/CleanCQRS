using API.Filters;
using Domain.Commands.Items;
using Domain.Dtos.AppLayerDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Items
{
    [Route("api/items")]
    [ApiController]
    [ServiceFilter(typeof(AuthorizeAuth))]
    public class AddItemsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("AddItems")]
        public async Task<IActionResult> AddItems([FromBody]
        AddItemsCommand addItemsCommand, CancellationToken cancellationToken)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            ApiResponseDto _responseApi = await this._mediator.Send(addItemsCommand, cancellationToken);

            return _responseApi.SuccesResponse
            ? this.Ok(_responseApi)
            : this.BadRequest(_responseApi);
        }
    }
}