using Chat.BusinessLogic.DTOs.ResponseDTOs;
using Chat.BusinessLogic.Extensions;
using Chat.BusinessLogic.Options.RedisOptions;
using Chat.BusinessLogic.Services.Interfaces;
using Chat.DataAccess.Models;
using Chat.DataAccess.Repositories.Interfaces;
using MapsterMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Shared.Utilities;

namespace Chat.BusinessLogic.Services.Implementations
{
    internal class ChatRoleService : IChatRoleService
    {
        private readonly IChatRoleRepository _chatRoleRepository;
        private readonly IDistributedCache _cacher;
        private readonly IOptions<RedisOptions> _redisOptions;
        private readonly IMapper _mapper;

        public ChatRoleService(IChatRoleRepository chatRoleRepository,
            IMapper mapper,
            IDistributedCache cacher,
            IOptions<RedisOptions> redisOptions)
        {
            _chatRoleRepository = chatRoleRepository;
            _mapper = mapper;
            _cacher = cacher;
            _redisOptions = redisOptions;
        }

        public async Task<IEnumerable<ChatRoleResponseDto>> GetAllChatRolesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<ChatRole>? chatRoles;

            var isCached = _cacher.TryGetRecord(nameof(ChatRole), out chatRoles);

            if (!isCached)
            {
                chatRoles = await _chatRoleRepository.GetAllAsync(false, cancellationToken);

                var cacheOptions = _cacher.CreateDistributedCacheEntryOptions(TimeSpan.FromMinutes(_redisOptions.Value.MessengerUserProfileCacheOptions.AbsoluteExpirationMinutes),
                   TimeSpanUtility.FromMinutesNullable(_redisOptions.Value.MessengerUserProfileCacheOptions.SlidingExpirationMinutes));

                await _cacher.SetRecordAsync(nameof(ChatRole), chatRoles, cacheOptions, cancellationToken);
            }

            var chatRolesDtos = _mapper.Map<IEnumerable<ChatRoleResponseDto>>(chatRoles!);

            return chatRolesDtos;
        }
    }
}
