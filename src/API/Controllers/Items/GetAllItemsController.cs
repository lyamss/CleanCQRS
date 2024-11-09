using API.Filters;
using Domain.Dtos.AppLayerDtos;
using Domain.Dtos.Query.Items;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Items
{
    [Route("api/items")]
    [ApiController]
    [ServiceFilter(typeof(AuthorizeAuth))]
    [ProducesResponseType(200, Type = typeof(IEnumerable<GetItemsQuery>))]
    public class GetAllItemsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("GetAllItems")]
        public async Task<IActionResult> GetAllItems(CancellationToken cancellationToken)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            var GetItemsQuery = new GetItemsQuery();
            ApiResponseDto _responseApi = await this._mediator.Send(GetItemsQuery, cancellationToken);

            return _responseApi.SuccesResponse
            ? this.Ok(_responseApi)
            : this.BadRequest(_responseApi);
        }
    }
}
