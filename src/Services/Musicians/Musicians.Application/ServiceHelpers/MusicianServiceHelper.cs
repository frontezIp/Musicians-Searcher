using Musicians.Application.Interfaces.Persistance;
using Musicians.Domain.Exceptions.NotFoundException;
using Musicians.Application.Interfaces.ServiceHelpers;
using Musicians.Domain.Models;

namespace Musicians.Application.ServiceHelpers
{
    public class MusicianServiceHelper : IMusicianServiceHelper
    {
        private readonly IMusiciansRepository _musicianRepository;

        public MusicianServiceHelper(IMusiciansRepository musicianRepository)
        {
            _musicianRepository = musicianRepository;
        }

        public async Task<Musician> CheckIfMusicianExistsAndGetAsync(Guid musicianId, CancellationToken cancellationToken = default)
        {
            var musician = await _musicianRepository.GetByIdAsync(musicianId, cancellationToken);

            if (musician == null)
                throw new MusicianNotFoundException();

            return musician;
        }

        public async Task CheckIfMusicianExistsAsync(Guid musicianId, CancellationToken cancellationToken = default)
        {
            await CheckIfMusicianExistsAndGetAsync(musicianId, cancellationToken);
        }
    }
}
