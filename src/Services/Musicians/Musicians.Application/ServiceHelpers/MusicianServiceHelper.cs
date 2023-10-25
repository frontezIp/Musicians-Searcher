using Musicians.Application.Interfaces.Persistance;
using Musicians.Domain.Exceptions.NotFoundException;
using Musicians.Application.Interfaces.ServiceHelpers;
using Musicians.Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Musicians.Application.Options.RedisOptions;
using Musicians.Application.Extensions;
using Shared.Utilities;

namespace Musicians.Application.ServiceHelpers
{
    public class MusicianServiceHelper : IMusicianServiceHelper
    {
        private readonly IMusiciansRepository _musicianRepository;
        private readonly IDistributedCache _distributedCache;
        private readonly IOptions<RedisOptions> _options;

        public MusicianServiceHelper(IMusiciansRepository musicianRepository,
            IDistributedCache distributedCache,
            IOptions<RedisOptions> options)
        {
            _musicianRepository = musicianRepository;
            _distributedCache = distributedCache;
            _options = options;
        }

        public async Task<Musician> CheckIfMusicianExistsAndGetAsync(Guid musicianId, CancellationToken cancellationToken = default)
        {
            Musician? musician;

            var isCached = _distributedCache.TryGetRecord(musicianId.ToString(), out musician);

            if (isCached)
            {
                return musician!;
            }

            musician = await _musicianRepository.GetByIdAsync(musicianId, cancellationToken);

            if (musician == null)
                throw new MusicianNotFoundException();
            
            var cacheOptions = _distributedCache.CreateDistributedCacheEntryOptions(TimeSpan.FromMinutes(_options.Value.MusicianCacheOptions.AbsoluteExpirationMinutes),
                TimeSpanUtility.FromMinutesNullable(_options.Value.MusicianCacheOptions.SlidingExpirationMinutes));

            await _distributedCache.SetRecordAsync(musicianId.ToString(), musician, cacheOptions);

            return musician;
        }

        public async Task CheckIfMusicianExistsAsync(Guid musicianId, CancellationToken cancellationToken = default)
        {
            await CheckIfMusicianExistsAndGetAsync(musicianId, cancellationToken);
        }
    }
}