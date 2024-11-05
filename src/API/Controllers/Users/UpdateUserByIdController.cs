using API.Filters;
using Domain.Commands.Users;
using Domain.Dtos.AppLayerDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Users
{
    [Route("api/users")]
    [ApiController]
    [ServiceFilter(typeof(AuthorizeAuth))]
    public class UpdateUserByIdController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPatch("UpdateUserById")]
        public async Task<IActionResult> UpdateUserById(
        [FromBody] UpdateUserCommand updateUserCommand, CancellationToken cancellationToken)
        {
            Console.WriteLine("Request received");
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            ApiResponseDto _responseApi = await this._mediator.Send(updateUserCommand, cancellationToken);

            return _responseApi.SuccesResponse 
                ? this.Ok(_responseApi) 
                : this.BadRequest(_responseApi);
        }
    }
}