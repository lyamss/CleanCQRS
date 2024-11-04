using API.Filters;
using Domain.Dtos.AppLayerDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Commands.Users;

namespace API.Controllers.Users
{
    [Route("api/users")]
    [ApiController]
    [ServiceFilter(typeof(AuthorizeAuth))]
    public class UpdateAndRemoveUserByIdController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpDelete("RemoveUserById")]
        public async Task<IActionResult> RemoveUserById(
        [FromQuery] int idUser, CancellationToken cancellationToken)
        {
            if (!this.ModelState.IsValid)
                return BadRequest(this.ModelState);

            var removeUserByIdCommand = new RemoveUserByIdCommand(idUser);
            ApiResponseDto _responseApi = await this._mediator.Send(removeUserByIdCommand, cancellationToken);

            return _responseApi.SuccesResponse ? Ok(_responseApi) : BadRequest(_responseApi);
        }
    }
}
