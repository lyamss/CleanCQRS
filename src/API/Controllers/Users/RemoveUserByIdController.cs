using API.Filters;
using Domain.Dtos.AppLayerDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Commands.Users;

namespace API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AuthorizeAuth))]
    public class RemoveUserByIdController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpDelete("{idUser}")]
        public async Task<IActionResult> Delete([FromRoute] int idUser, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var removeUserByIdCommand = new RemoveUserByIdCommand(idUser);
            ApiResponseDto _responseApi = await this._mediator.Send(removeUserByIdCommand, cancellationToken);

            return _responseApi.SuccesResponse ? Ok(_responseApi) : BadRequest(_responseApi);
        }
    }
}
