using Identity.Application.DTOs.RequestDTOs;
using Identity.Application.DTOs.ResponseDTOs;

namespace Identity.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserResponseDto> CreateUserAsync(UserRequestDto user);
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto> GetCurrentUserAsync();
        Task<UserResponseDto> GetUserByIdAsync(Guid userId);
        Task<IEnumerable<RoleResponseDto>> GetUserRolesAsync(Guid userId);
    }
}
