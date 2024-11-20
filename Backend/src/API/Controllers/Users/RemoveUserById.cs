using Domain.Dtos.AppLayerDtos;
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos.Commands.Users;

namespace API.Controllers.Users
{
    public partial class Users
    {
        [HttpDelete("RemoveUserById")]
        public async Task<IActionResult> RemoveUserById(
        [FromQuery] RemoveUserByIdCommand idUser, CancellationToken cancellationToken)
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