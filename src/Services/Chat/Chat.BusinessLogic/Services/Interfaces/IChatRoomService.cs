using Chat.BusinessLogic.DTOs.RequestDTOs;
using Chat.BusinessLogic.DTOs.RequestDTOs.ChatRoomRequestsDTOs;
using Chat.BusinessLogic.DTOs.RequestDTOs.MessageRequestsDTOs;
using Chat.BusinessLogic.DTOs.ResponseDTOs;
using Chat.BusinessLogic.RequestFeatures;

namespace Chat.BusinessLogic.Services.Interfaces
{
    public interface IChatRoomService
    {
        Task<ChatRoomResponseDto> CreateChatRoomAsync(Guid messengerUserId, CreateChatRoomRequestDto chatRoomRequestDto, CancellationToken cancellationToken = default);
        Task<ChatRoomResponseDto> GetChatRoomAsync(Guid messengerUserId, Guid chatRoomId, GetMessagesRequestDto getMessagesRequestDto, CancellationToken cancellationToken = default);
        Task<(IEnumerable<ChatRoomResponseDto> chatRooms, MetaData metaData)> GetChatRoomsOfGivenMessengerUserAsync(Guid messengerUserId,
            PaginatedUserChatRoomsRequestDto paginatedUserChatRoomsRequestDto,
            CancellationToken cancellationToken = default);
        Task UpdateChatRoomAsync(Guid messengerUserId, Guid chatRoomId, UpdateChatRoomRequestDto chatRoomUpdateRequestDto, CancellationToken cancellationToken = default);
    }
}
