using Domain.Dtos.AppLayerDtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Commands
{
    public record class ByIdCommand : IRequest<ApiResponseDto>
    {
        [Required]
        public int ById { get; private set; }

        public ByIdCommand(int byId)
        {
            this.ById = byId;
        }
    }
}