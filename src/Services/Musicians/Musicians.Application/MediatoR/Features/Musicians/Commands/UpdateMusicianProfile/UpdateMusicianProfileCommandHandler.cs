using MapsterMapper;
using MediatR;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Application.Interfaces.ServiceHelpers;
using Musicians.Domain.Models;

namespace Musicians.Application.MediatoR.Features.Musicians.Commands.UpdateMusicianProfile
{
    internal class UpdateMusicianProfileCommandHandler
        : IRequestHandler<UpdateMusicianProfileCommand>
    {

        private readonly IMusiciansRepository _musicianRepository;
        private readonly IMapper _mapper;
        private readonly IMusicianServiceHelper _musicianServiceHelper;
        private readonly IGenreServiceHelper _genreServiceHelper;
        private readonly ISkillServiceHelper _skillServiceHelper;

        public UpdateMusicianProfileCommandHandler(IMusiciansRepository musicianRepository,
            IMusicianServiceHelper musicianServiceHelper,
            IMapper mapper,
            IGenreServiceHelper genreServiceHelper,
            ISkillServiceHelper skillServiceHelper)
        {
            _musicianRepository = musicianRepository;
            _musicianServiceHelper = musicianServiceHelper;
            _mapper = mapper;
            _genreServiceHelper = genreServiceHelper;
            _skillServiceHelper = skillServiceHelper;
        }

        public async Task Handle(UpdateMusicianProfileCommand request, CancellationToken cancellationToken)
        {
            await _musicianServiceHelper.CheckIfMusicianExistsAsync(request.musicianId, cancellationToken);
            var genres =  await _genreServiceHelper.CheckIfGenresByGivenIdsExistsAndGet(request.MusicianProfileRequestDto.FavouriteGenresIds, cancellationToken);
            var skills = await _skillServiceHelper.CheckIfSkillsByGivenIdsExistsAndGet(request.MusicianProfileRequestDto.SkillsIds, cancellationToken);

            var musician = _mapper.Map<Musician>(request.MusicianProfileRequestDto);

            musician.Id = request.musicianId;
            musician.Skills = skills.ToList();
            musician.FavouriteGenres = genres.ToList();

            await _musicianRepository.UpdateMusicianProfileAsync(musician);

        }
    }
}
