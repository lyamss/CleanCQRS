﻿using Domain.Dtos.AppLayerDtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Commands
{
    public record ByIdCommand : IRequest<ApiResponseDto>
    {
        [Required]
        public Guid ById { get; init; }
    }
}