using Chat.BusinessLogic.DTOs.ResponseDTOs;
using Chat.BusinessLogic.Extensions;
using Chat.BusinessLogic.Grpc.v1.Clients.Interfaces;
using Chat.BusinessLogic.Helpers.Interfaces;
using Chat.BusinessLogic.Options.RedisOptions;
using Chat.BusinessLogic.Services.Interfaces;
using MapsterMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Shared.Utilities;

namespace Chat.BusinessLogic.Services.Implementations
{
    internal class MessengerUserService : IMessengerUserService
    {
        private readonly IMessengerUserServiceHelper _messengerUserServiceHelper;
        private readonly IMusicianClient _musicianClient;
        private readonly IDistributedCache _distributedCache;
        private readonly IOptions<RedisOptions> _redisOptions;
        private readonly IMapper _mapper;

        public MessengerUserService(IMessengerUserServiceHelper messengerUserServiceHelper,
            IMusicianClient musicianClient,
            IMapper mapper,
            IDistributedCache distributedCache,
            IOptions<RedisOptions> redisOptions)
        {
            _messengerUserServiceHelper = messengerUserServiceHelper;
            _musicianClient = musicianClient;
            _mapper = mapper;
            _distributedCache = distributedCache;
            _redisOptions = redisOptions;
        }
        public async Task<MessengerUserProfileResponseDto> GetMessengerUserProfileInfoAsync(Guid messengerUserId,
            CancellationToken cancellationToken = default)
        {
            MessengerUserProfileResponseDto? cachedMessengerUserProfileResponseDto;

            var isCached = _distributedCache.TryGetRecord(messengerUserId.ToString(), out cachedMessengerUserProfileResponseDto);
            if (isCached)
            {
                return cachedMessengerUserProfileResponseDto!;
            }

            var messengerUser = await _messengerUserServiceHelper.CheckIfMessengerUserExistsAndGetAsync(messengerUserId, false, cancellationToken);

            var messengerUserProfile = await _musicianClient.GetMessengerUserProfileAsync(messengerUserId.ToString());

            var messengerUseResponseDto = _mapper.Map<MessengerUserProfileResponseDto>(messengerUserProfile);
            messengerUseResponseDto = _mapper.Map(messengerUser, messengerUseResponseDto);
            var cacheOptions = _distributedCache.CreateDistributedCacheEntryOptions(TimeSpan.FromMinutes(_redisOptions.Value.MessengerUserProfileCacheOptions.AbsoluteExpirationMinutes),
               TimeSpanUtility.FromMinutesNullable(_redisOptions.Value.MessengerUserProfileCacheOptions.SlidingExpirationMinutes));

            await _distributedCache.SetRecordAsync(messengerUserId.ToString(), messengerUseResponseDto,
                cacheOptions);

            return messengerUseResponseDto;
        }

        public async Task<IEnumerable<MessengerUserProfileResponseDto>> GetMessengerUserFriendsAsync(Guid messengerUserId,
            CancellationToken cancellationToken = default)
        {
            await _messengerUserServiceHelper.CheckIfMessengerUserExistsAsync(messengerUserId, cancellationToken);

            var messengerUserFriendsResponse = await _musicianClient.GetMessengerUserFriendsAsync(messengerUserId.ToString());
            var friendsUsersDtosToReturn = _mapper.Map<IEnumerable<MessengerUserProfileResponseDto>>(messengerUserFriendsResponse.Friends);

            return friendsUsersDtosToReturn;
        }
    }
}
