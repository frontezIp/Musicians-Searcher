using Chat.DataAccess.Contexts;
using Chat.DataAccess.Models;
using Chat.DataAccess.Repositories.Interfaces;

namespace Chat.DataAccess.Repositories.Implementations
{
    internal class MessengerUserRepository : RepositoryBase<MessengerUser>, IMessengerUserRepository
    {
        public MessengerUserRepository(ChatContext context) : base(context)
        {
        }
    }
}
