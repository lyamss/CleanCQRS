using Domain.Dtos.AppLayerDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Commands.Items;
using API.Filters;
namespace API.Controllers.Items
{
    [Route("api/items")]
    [ApiController]
    [ServiceFilter(typeof(AuthorizeAuth))]
    public class GetItemsByIdController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("GetItemsById")]
        public async Task<IActionResult> GetItemsById([FromQuery] int idUser, CancellationToken cancellationToken)
        {
            if (!this.ModelState.IsValid)
                return BadRequest(this.ModelState);

            var getItemsByIdCommand = new GetItemsCommand(idUser);
            ApiResponseDto _responseApi = await this._mediator.Send(getItemsByIdCommand, cancellationToken);

            return _responseApi.SuccesResponse ? Ok(_responseApi) : BadRequest(_responseApi);
        }
    }
}