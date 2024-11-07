using Microsoft.AspNetCore.Mvc;
using Domain.Dtos.AppLayerDtos;
using MediatR;
using API.Filters;
using Domain.Dtos.Query.Users;
namespace API.Controllers.Users
{
    [Route("api/users")]
    [ApiController]
    [ServiceFilter(typeof(AuthorizeAuth))]
    public class GetAllUsersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("GetAllUsers")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetUserQuery>))]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            var query = new GetUserQuery();
            ApiResponseDto _responseApi = await this._mediator.Send(query, cancellationToken);

            return _responseApi.SuccesResponse 
                ? this.Ok(_responseApi) 
                : this.BadRequest(_responseApi);
        }
    }
}