using Identity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Interfaces.ServiceHelpers
{
    public interface IUserServiceHelper
    {
        Task<User> CheckIfUserExistsAndGetByIdAsync(Guid userId);
    }
}
