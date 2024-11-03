using Domain.Dtos.AppLayerDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Commands.Authentification;
namespace API.Controllers.Authentification
{
    [Route("api/users")]
    [ApiController]
    public class CreateUserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody]
        CreateUserCommand setuserRegistrationDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ApiResponseDto _responseApi = await this._mediator.Send(setuserRegistrationDto, cancellationToken);

            return _responseApi.SuccesResponse
            ? Ok(_responseApi)
            : BadRequest(_responseApi);
        }
    }
}