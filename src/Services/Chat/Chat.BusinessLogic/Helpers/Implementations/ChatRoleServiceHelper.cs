using Chat.BusinessLogic.Exceptions.NotFoundExceptions;
using Chat.BusinessLogic.Helpers.Interfaces;
using Chat.DataAccess.Models;
using Chat.DataAccess.Options;
using Chat.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace Chat.BusinessLogic.Helpers.Implementations
{
    internal class ChatRoleServiceHelper : IChatRoleServiceHelper
    {
        private readonly IChatRoleRepository _chatRoleRepository;
        private readonly Dictionary<string, int> RolePrecedence;

        public ChatRoleServiceHelper(IChatRoleRepository chatRoleRepository,
            IOptions<ChatRolesOptions> options)
        {
            _chatRoleRepository = chatRoleRepository;
            RolePrecedence = new Dictionary<string, int>
            {
                { options.Value.Admin, options.Value.AdminRolePrecedence},
                { options.Value.Creator, options.Value.CreatorRolePrecedence },
                { options.Value.User, options.Value.UserRolePrecedence }
            };
        }

        public async Task<ChatRole> CheckIfChatRoleExistsAndGetByNameAsync(string name, bool trackChanges, CancellationToken cancellationToken)
        {
            var chatRole = await _chatRoleRepository.GetChatRoleByNameAsync(name, trackChanges, cancellationToken);

            if (chatRole == null)
                throw new ChatRoleNotFoundException();

            return chatRole;
        }

        public async Task<ChatRole> CheckIfChatRoleExistsAndGetByIdAsync(Guid chatRoleId, bool trackChanges, CancellationToken cancellationToken = default)
        {
            var chatRole = await _chatRoleRepository.GetByIdAsync(chatRoleId, trackChanges, cancellationToken);

            if (chatRole == null)
                throw new ChatRoleNotFoundException();

            return chatRole;
        }

        public bool IsFirstRoleHasMorePrecedenceThenSecond(ChatRole firstRole, ChatRole chatRole)
        {
            if (RolePrecedence[firstRole.Name] > RolePrecedence[chatRole.Name])
                return true;

            return false;
        }
    }
}
