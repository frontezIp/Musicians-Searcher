using Chat.BusinessLogic.DTOs.ResponseDTOs;

namespace Chat.BusinessLogic.Services.Interfaces
{
    public interface IChatRoleService
    {
        Task<IEnumerable<ChatRoleResponseDto>> GetAllChatRolesAsync(CancellationToken cancellationToken = default);
    }
}
