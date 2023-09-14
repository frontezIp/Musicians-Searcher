using Chat.BusinessLogic.DTOs.RequestDTOs.MessageRequestsDTOs;
using Chat.BusinessLogic.DTOs.ResponseDTOs;

namespace Chat.BusinessLogic.Services.Interfaces
{
    public interface IMessageService
    {
        Task CreateMessageAsync(Guid messengerUserId, Guid chatRoomId, CreateMessageRequestDto createMessageRequestDto, CancellationToken cancellationToken = default);
        Task DeleteMessagesAsync(Guid messengerUserId, Guid chatRoomId, IEnumerable<Guid> messagesToDeleteIds, CancellationToken cancellationToken = default);
        Task UpdateMessageAsync(Guid messengerUserId, Guid chatRoomId, Guid messageId, UpdateMessageRequestDto updateMessageRequestDto, CancellationToken cancellationToken = default);
    }
}
