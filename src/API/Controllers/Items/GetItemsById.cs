using Domain.Dtos.AppLayerDtos;
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos.Commands.Items;
using Domain.Dtos.Query.Items;
namespace API.Controllers.Items
{
    public partial class Items
    {
        [HttpGet("GetItemsById")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetItemsQuery>))]
        public async Task<IActionResult> GetItemsById
        ([FromQuery] GetItemByIdCommand idItem, CancellationToken cancellationToken)
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