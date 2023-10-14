using Chat.BusinessLogic.DTOs.ResponseDTOs;

namespace Chat.BusinessLogic.Services.Interfaces
{
    public interface IMessengerUserService
    {
        Task<IEnumerable<MessengerUserProfileResponseDto>> GetMessengerUserFriendsAsync(Guid messengerUserId, CancellationToken cancellationToken = default);
        Task<MessengerUserProfileResponseDto> GetMessengerUserProfileInfoAsync(Guid messengerUserId, CancellationToken cancellationToken = default);
    }
}
