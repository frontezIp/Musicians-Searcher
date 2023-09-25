using Chat.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DataAccess.Repositories.Interfaces
{
    public interface IChatRoleRepository : IRepositoryBase<ChatRole>
    {
        Task<ChatRole?> GetChatRoleByNameAsync(string name, bool trackChanges, CancellationToken cancellationToken);
    }
}
