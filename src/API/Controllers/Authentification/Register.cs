using Domain.Dtos.AppLayerDtos;
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos.Commands.Authentification;
namespace API.Controllers.Authentification
{
    public partial class Authentification
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]
        CreateUserCommand setuserRegistrationDto, CancellationToken cancellationToken)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            ApiResponseDto _responseApi = await this._mediator.Send(setuserRegistrationDto, cancellationToken);

            return _responseApi.SuccesResponse
            ? this.Ok(_responseApi)
            : this.BadRequest(_responseApi);
        }
    }
}