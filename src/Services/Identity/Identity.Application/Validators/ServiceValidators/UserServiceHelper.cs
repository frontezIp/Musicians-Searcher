using Identity.Application.Interfaces.ServiceHelpers;
using Identity.Domain.Exceptions.NotFoundException;
using Identity.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Validators.ServiceHelpers
{
    internal class UserServiceHelper : IUserServiceHelper
    {
        private readonly UserManager<User> _userManager;

        public UserServiceHelper(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> CheckIfUserExistsAndGetByIdAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }
    }
}
