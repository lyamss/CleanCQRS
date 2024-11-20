using Domain.Dtos.AppLayerDtos;
using Domain.Dtos.Query.Items;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Items
{
    public partial class Items
    {
        [HttpGet("GetAllItems")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetItemsQuery>))]
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
