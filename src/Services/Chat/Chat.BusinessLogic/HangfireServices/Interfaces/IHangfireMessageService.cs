using Chat.BusinessLogic.DTOs.RequestDTOs.MessageRequestsDTOs;

namespace Chat.BusinessLogic.HangfireServices.Interfaces
{
    public interface IHangfireMessageService
    {
        Task SendDelayedMessageAsync(Guid messengerUserId, Guid chatRoomId, CreateMessageRequestDto createMessageRequestDto, CancellationToken cancellationToken = default);
    }
}
