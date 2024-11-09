using API.Filters;
using Domain.Dtos.AppLayerDtos;
using Domain.Dtos.Commands.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Users
{
    [Route("api/users")]
    [ApiController]
    [ServiceFilter(typeof(AuthorizeAuth))]
    public class GetUserByIdController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById([FromQuery] GetUserByIdCommand idUser, CancellationToken cancellationToken)
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