﻿using Domain.Dtos.AppLayerDtos;
using MediatR;

namespace Domain.Query.Users
{
    public class GetUserQuery : IRequest<ApiResponseDto>
    {
        public int Id_User { get; set; }
        public string Email { get; set; }
    }
}