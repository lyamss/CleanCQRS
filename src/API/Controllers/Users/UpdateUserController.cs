using API.Filters;
using Domain.Commands.Users;
using Domain.Dtos.AppLayerDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    //[ServiceFilter(typeof(AuthorizeAuth))]
    public class UpdateUserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        [HttpPatch("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ApiResponseDto _responseApi = await this._mediator.Send(updateUserCommand, cancellationToken);

            return _responseApi.SuccesResponse ? Ok(_responseApi) : BadRequest(_responseApi);
        }
    }
}