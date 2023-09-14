using Chat.DataAccess.Models;

namespace Chat.BusinessLogic.Helpers.Interfaces
{
    public interface IChatRoleServiceHelper
    {
        Task<ChatRole> CheckIfChatRoleExistsAndGetByIdAsync(Guid chatRoleId, bool trackChanges, CancellationToken cancellationToken = default);
        Task<ChatRole> CheckIfChatRoleExistsAndGetByNameAsync(string name, bool trackChanges, CancellationToken cancellationToken);
        bool IsFirstRoleHasMorePrecedenceThenSecond(ChatRole firstRole, ChatRole chatRole);
    }
}
