using Domain.Dtos.AppLayerDtos;
using Domain.Dtos.Commands.Authentification;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Authentification
{
    [Route("api/auth")]
    [ApiController]
    public partial class Authentification(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]
        LoginUserCommand setuserLoginDto, CancellationToken cancellationToken)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            ApiResponseDto _responseApi = await this._mediator.Send(setuserLoginDto, cancellationToken);

            return _responseApi.SuccesResponse
            ? this.Ok(_responseApi)
            : this.BadRequest(_responseApi);
        }
    }
}