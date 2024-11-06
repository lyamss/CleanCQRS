using Domain.Dtos.AppLayerDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using API.Filters;
using Domain.Dtos.Commands;
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
                return this.BadRequest(this.ModelState);

            var getItemsByIdCommand = new ByIdCommand(idUser);
            ApiResponseDto _responseApi = await this._mediator.Send(getItemsByIdCommand, cancellationToken);

            return _responseApi.SuccesResponse 
                ? this.Ok(_responseApi) 
                : this.BadRequest(_responseApi);
        }
    }
}