using API.Filters;
using Domain.Commands.Users;
using Domain.Dtos.AppLayerDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AuthorizeAuth))]
    public class GetUserByIdController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("GetUserId")]
        public async Task<IActionResult> GetUserId([FromQuery] int idUser, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var getUserByIdCommand = new GetUserByIdCommand(idUser);
            ApiResponseDto _responseApi = await this._mediator.Send(getUserByIdCommand, cancellationToken);

            return _responseApi.SuccesResponse ? Ok(_responseApi) : BadRequest(_responseApi);
        }
    }
}