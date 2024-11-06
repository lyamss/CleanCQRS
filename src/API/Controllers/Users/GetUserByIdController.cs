using API.Filters;
using Domain.Dtos.AppLayerDtos;
using Domain.Dtos.Commands;
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
        public async Task<IActionResult> GetUserById([FromQuery] int idUser, CancellationToken cancellationToken)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            var getUserByIdCommand = new ByIdCommand(idUser);
            ApiResponseDto _responseApi = await this._mediator.Send(getUserByIdCommand, cancellationToken);

            return _responseApi.SuccesResponse 
                ? this.Ok(_responseApi) 
                : this.BadRequest(_responseApi);
        }
    }
}