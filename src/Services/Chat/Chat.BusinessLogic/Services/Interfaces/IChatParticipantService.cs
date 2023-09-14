using Chat.BusinessLogic.DTOs.RequestDTOs.ChatParticipantRequestsDTOs;
using Chat.BusinessLogic.DTOs.ResponseDTOs;
using Chat.BusinessLogic.RequestFeatures;

namespace Chat.BusinessLogic.Services.Interfaces
{
    public interface IChatParticipantService
    {
        Task AddChatParticipantsAsync(Guid messengerUserId, Guid chatRoomId, List<Guid> messengerUserIdsToAdd, CancellationToken cancellationToken = default);
        Task DeleteChatParticipantsAsync(Guid messengerUserId, Guid chatRoomId, List<Guid> chatParticipantsToDelete, CancellationToken cancellationToken);
        Task<(IEnumerable<ChatParticipantResponseDto> chatParticipants, MetaData metaData)> GetChatParticipantsAsync(Guid messengerUserId, Guid chatRoomId, GetFilteredChatParticipantsRequestDto getFilteredChatParticipantsRequestDTO, CancellationToken cancellationToken = default);
        Task UpdateChatParticipantRoleAsync(Guid messengerUserIssuerId, Guid chatRoomId, Guid chatParticipantToUpdateId, Guid newRoleId, CancellationToken cancellationToken = default);
    }
}
