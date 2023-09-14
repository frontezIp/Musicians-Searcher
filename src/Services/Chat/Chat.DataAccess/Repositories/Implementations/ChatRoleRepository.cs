using Chat.DataAccess.Contexts;
using Chat.DataAccess.Models;
using Chat.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.DataAccess.Repositories.Implementations
{
    internal class ChatRoleRepository : RepositoryBase<ChatRole>, IChatRoleRepository
    {
        public ChatRoleRepository(ChatContext context) : base(context)
        {
        }

        public async Task<ChatRole?> GetChatRoleByNameAsync(string name, bool trackChanges, CancellationToken cancellationToken)
        {
            return await GetByCondition(chatRole => chatRole.Name.ToLower() == name.ToLower(), trackChanges).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
