﻿using Domain.Dtos.AppLayerDtos;
using MediatR;

namespace Domain.Dtos.Query.Users
{
    public record class GetUserQuery : IRequest<ApiResponseDto>
    {
        public int Id_User { get; set; }
        public string Email { get; set; }
    }
}