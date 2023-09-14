using MediatR;
using Musicians.Application.DTOs.RequestDTOs;

namespace Musicians.Application.MediatoR.Features.Musicians.Commands.UpdateMusicianProfile
{
    public record UpdateMusicianProfileCommand(UpdateMusicianProfileRequestDto MusicianProfileRequestDto, Guid musicianId)
        : IRequest;
}
