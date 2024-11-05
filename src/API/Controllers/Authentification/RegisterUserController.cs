﻿using Domain.Dtos.AppLayerDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Commands.Authentification;
namespace API.Controllers.Authentification
{
    [Route("api/auth")]
    [ApiController]
    public class RegisterUserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]
        CreateUserCommand setuserRegistrationDto, CancellationToken cancellationToken)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            ApiResponseDto _responseApi = await this._mediator.Send(setuserRegistrationDto, cancellationToken);

            return _responseApi.SuccesResponse
            ? this.Ok(_responseApi)
            : this.BadRequest(_responseApi);
        }
    }
}