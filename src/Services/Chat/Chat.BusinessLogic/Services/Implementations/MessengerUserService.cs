using Chat.BusinessLogic.DTOs.ResponseDTOs;
using Chat.BusinessLogic.Grpc.v1.Clients.Interfaces;
using Chat.BusinessLogic.Helpers.Interfaces;
using Chat.BusinessLogic.Services.Interfaces;
using MapsterMapper;

namespace Chat.BusinessLogic.Services.Implementations
{
    internal class MessengerUserService : IMessengerUserService
    {
        private readonly IMessengerUserServiceHelper _messengerUserServiceHelper;
        private readonly IMusicianClient _musicianClient;
        private readonly IMapper _mapper;

        public MessengerUserService(IMessengerUserServiceHelper messengerUserServiceHelper,
            IMusicianClient musicianClient,
            IMapper mapper)
        {
            _messengerUserServiceHelper = messengerUserServiceHelper;
            _musicianClient = musicianClient;
            _mapper = mapper;
        }

        public async Task<MessengerUserProfileResponseDto> GetMessengerUserProfileInfoAsync(Guid messengerUserId,
            CancellationToken cancellationToken = default)
        {
            var messengerUser = await _messengerUserServiceHelper.CheckIfMessengerUserExistsAndGetAsync(messengerUserId, false, cancellationToken);

            var messengerUserProfile = await _musicianClient.GetMessengerUserProfileAsync(messengerUserId.ToString());

            var messengerUseResponseDto = _mapper.Map<MessengerUserProfileResponseDto>(messengerUserProfile);
            messengerUseResponseDto = _mapper.Map(messengerUser, messengerUseResponseDto);

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
