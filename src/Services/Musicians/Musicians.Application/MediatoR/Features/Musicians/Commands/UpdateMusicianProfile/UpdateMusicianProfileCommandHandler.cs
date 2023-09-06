using MapsterMapper;
using MediatR;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Application.Interfaces.ServiceHelpers;
using Musicians.Infrastructure.Models;

namespace Musicians.Application.MediatoR.Features.Musicians.Commands.UpdateMusicianProfile
{
    internal class UpdateMusicianProfileCommandHandler
        : IRequestHandler<UpdateMusicianProfileCommand>
    {

        private readonly IMusiciansRepository _musicianRepository;
        private readonly IMapper _mapper;
        private readonly IMusicianServiceHelper _musicianServiceValidator;
        private readonly IGenreServiceHelper _genreServiceValidator;
        private readonly ISkillServiceHelper _skillServiceValidator;

        public UpdateMusicianProfileCommandHandler(IMusiciansRepository musicianRepository,
            IMusicianServiceHelper musicianServiceValidator,
            IMapper mapper,
            IGenreServiceHelper genreServiceValidator,
            ISkillServiceHelper skillServiceValidator)
        {
            _musicianRepository = musicianRepository;
            _musicianServiceValidator = musicianServiceValidator;
            _mapper = mapper;
            _genreServiceValidator = genreServiceValidator;
            _skillServiceValidator = skillServiceValidator;
        }

        public async Task Handle(UpdateMusicianProfileCommand request, CancellationToken cancellationToken)
        {
            await _musicianServiceValidator.CheckIfMusicianExistsAsync(request.musicianId, cancellationToken);
            var genres =  await _genreServiceValidator.CheckIfGenresByGivenIdsExistsAndGet(request.MusicianProfileRequestDto.FavouriteGenresIds, cancellationToken);
            var skills = await _skillServiceValidator.CheckIfSkillsByGivenIdsExistsAndGet(request.MusicianProfileRequestDto.SkillsIds, cancellationToken);

            var musician = _mapper.Map<Musician>(request.MusicianProfileRequestDto);

            musician.Id = request.musicianId;
            musician.Skills = skills.ToList();
            musician.FavouriteGenres = genres.ToList();

            await _musicianRepository.UpdateMusicianProfileAsync(musician);

        }
    }
}
