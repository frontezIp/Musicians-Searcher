using Amazon.Runtime.Internal.Util;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Musicians.Application.DTOs.ResponseDTOs;
using Musicians.Application.Extensions;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Application.Interfaces.ServiceHelpers;
using Musicians.Application.Options.RedisOptions;
using Musicians.Domain.Models;
using Shared.Utilities;

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
        private readonly IOptions<RedisOptions> _redisOptions;
        private readonly IDistributedCache _distributedCache;

        public UpdateMusicianProfileCommandHandler(IMusiciansRepository musicianRepository,
            IMusicianServiceHelper musicianServiceHelper,
            IMapper mapper,
            IGenreServiceHelper genreServiceHelper,
            ISkillServiceHelper skillServiceHelper,
            IOptions<RedisOptions> redisOptions,
            IDistributedCache distributedCache)
        {
            _musicianRepository = musicianRepository;
            _musicianServiceHelper = musicianServiceHelper;
            _mapper = mapper;
            _genreServiceHelper = genreServiceHelper;
            _skillServiceHelper = skillServiceHelper;
            _redisOptions = redisOptions;
            _distributedCache = distributedCache;
        }

        public async Task Handle(UpdateMusicianProfileCommand request, CancellationToken cancellationToken)
        {
            var musician = await _musicianServiceHelper.CheckIfMusicianExistsAndGetAsync(request.musicianId, cancellationToken);
            var genres =  await _genreServiceHelper.CheckIfGenresByGivenIdsExistsAndGet(request.MusicianProfileRequestDto.FavouriteGenresIds, cancellationToken);
            var skills = await _skillServiceHelper.CheckIfSkillsByGivenIdsExistsAndGet(request.MusicianProfileRequestDto.SkillsIds, cancellationToken);

            musician = _mapper.Map(request.MusicianProfileRequestDto, musician);
            musician.Skills = skills.ToList();
            musician.FavouriteGenres = genres.ToList();

            await _musicianRepository.UpdateMusicianProfileAsync(musician);

            var cacheOptions = _distributedCache.CreateDistributedCacheEntryOptions(TimeSpan.FromMinutes(_redisOptions.Value.MusicianCacheOptions.AbsoluteExpirationMinutes),
               TimeSpanUtility.FromMinutesNullable(_redisOptions.Value.MusicianCacheOptions.SlidingExpirationMinutes));

            await _distributedCache.SetRecordAsync(request.musicianId.ToString(), musician, cacheOptions, cancellationToken);
        }
    }
}
