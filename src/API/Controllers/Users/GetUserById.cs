using Domain.Dtos.AppLayerDtos;
using Domain.Dtos.Commands.Users;
using Domain.Dtos.Query.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Users
{
    public partial class Users
    {
        [HttpGet("GetUserById")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetUserQuery>))]
        public async Task<IActionResult> GetUserById
        ([FromQuery] GetUserByIdCommand idUser, CancellationToken cancellationToken)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            ApiResponseDto _responseApi = await this._mediator.Send(idUser, cancellationToken);

            return _responseApi.SuccesResponse 
                ? this.Ok(_responseApi) 
                : this.BadRequest(_responseApi);
        }
    }
}