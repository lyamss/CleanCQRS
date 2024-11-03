using Domain.Commands.Authentification;
using Domain.Dtos.AppLayerDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Authentification
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginUserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody]
        LoginUserCommand setuserLoginDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ApiResponseDto _responseApi = await this._mediator.Send(setuserLoginDto, cancellationToken);

            return _responseApi.SuccesResponse
            ? Ok(_responseApi)
            : BadRequest(_responseApi);
        }
    }
}