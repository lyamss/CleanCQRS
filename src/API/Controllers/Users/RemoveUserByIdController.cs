﻿using API.Filters;
using Domain.Dtos.AppLayerDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos.Commands;

namespace API.Controllers.Users
{
    [Route("api/users")]
    [ApiController]
    [ServiceFilter(typeof(AuthorizeAuth))]
    public class RemoveUserByIdController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpDelete("RemoveUserById")]
        public async Task<IActionResult> RemoveUserById(
        [FromQuery] Guid idUser, CancellationToken cancellationToken)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            var removeUserByIdCommand = new ByIdCommand(idUser);
            ApiResponseDto _responseApi = await this._mediator.Send(removeUserByIdCommand, cancellationToken);

            return _responseApi.SuccesResponse 
                ? this.Ok(_responseApi) 
                : this.BadRequest(_responseApi);
        }
    }
}