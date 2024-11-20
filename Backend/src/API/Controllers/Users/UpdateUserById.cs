using Domain.Dtos.AppLayerDtos;
using Domain.Dtos.Commands.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Users
{
    public partial class Users
    {
        [HttpPatch("UpdateUserById")]
        public async Task<IActionResult> UpdateUserById(
        [FromBody] UpdateUserCommand updateUserCommand, CancellationToken cancellationToken)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            ApiResponseDto _responseApi = await this._mediator.Send(updateUserCommand, cancellationToken);

            return _responseApi.SuccesResponse 
                ? this.Ok(_responseApi) 
                : this.BadRequest(_responseApi);
        }
    }
}