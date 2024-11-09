using Domain.Dtos.AppLayerDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using API.Filters;
using Domain.Dtos.Commands.Items;
namespace API.Controllers.Items
{
    [Route("api/items")]
    [ApiController]
    [ServiceFilter(typeof(AuthorizeAuth))]
    public class GetItemsByIdController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("GetItemsById")]
        public async Task<IActionResult> GetItemsById([FromQuery] GetItemByIdCommand idItem, CancellationToken cancellationToken)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            ApiResponseDto _responseApi = await this._mediator.Send(idItem, cancellationToken);

            return _responseApi.SuccesResponse 
                ? this.Ok(_responseApi) 
                : this.BadRequest(_responseApi);
        }
    }
}