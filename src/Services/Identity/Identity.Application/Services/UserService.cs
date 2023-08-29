using Identity.Application.DTOs.RequestDTOs;
using Identity.Application.DTOs.ResponseDTOs;
using Identity.Application.Interfaces.Services;
using Identity.Domain.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Identity.Application.Interfaces.ServiceValidators;
using Identity.Domain.Exceptions.BadRequestException;
using Microsoft.EntityFrameworkCore;
using Identity.Application.Interfaces.Persistance;
using Microsoft.Extensions.Logging;
using Shared.Constants;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Identity.Application.Services
{
    internal class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ICityRepository _cityRepository;
        private readonly IUserServiceValidator _userServiceValidator;
        private readonly ICityServiceValidator _cityServiceValidator;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IMapper mapper,
            IUserServiceValidator userServiceValidator,
            ICityServiceValidator cityServiceValidator,
            ICityRepository cityRepository,
            ILogger<UserService> logger,
            IHttpContextAccessor contextAccessor
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _userServiceValidator = userServiceValidator;
            _cityServiceValidator = cityServiceValidator;
            _cityRepository = cityRepository;
            _logger = logger;
            _contextAccessor = contextAccessor;
        }

        public async Task<UserResponseDto> CreateUserAsync(UserRequestDto user)
        {
            var city = await _cityServiceValidator.CheckIfCityExistsAndGetAsync(user.CityId, false);
            var userDto = _mapper.Map<User>(user);
            var result = await _userManager.CreateAsync(userDto, user.Password);
            if (!result.Succeeded)
            {
                throw new UserInvalidCredentialsBadRequestException(result.Errors.First().ToString() ?? "Invalid credentials");
            }
            _logger.LogInformation("User with {Id} id was successfully created", userDto.Id);
            await _userManager.AddToRoleAsync(userDto, RoleNamesConstants.UserRoleName);
            userDto.City = city;
            var userToReturn = _mapper.Map<UserResponseDto>(userDto);
            return userToReturn;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var allUsers = await _userManager.Users
                      .Include(u => u.City)
                          .ThenInclude(c => c.Country)
                      .AsNoTracking().ToListAsync();
            var usersDtos = _mapper.Map<IEnumerable<UserResponseDto>>(allUsers);
            _logger.LogInformation("All users was successfully retrieved");
            return usersDtos;
        }

        public async Task<UserResponseDto> GetUserByIdAsync(Guid id)
        {
            var user = await _userServiceValidator.CheckIfUserExistsAndGetByIdAsync(id);
            var city = await _cityRepository.GetCityById(user.CityId, false);
            user.City = city;
            var userDto = _mapper.Map<UserResponseDto>(user);
            _logger.LogInformation("User with {Id} id was successfully retrieved", user.Id);
            return userDto;
        }

        public async Task<IEnumerable<RoleResponseDto>> GetUserRolesAsync(Guid userId)
        {
            var user = await _userServiceValidator.CheckIfUserExistsAndGetByIdAsync(userId);
            var roleNames = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.AsNoTracking().Where(r => roleNames.Contains(r.Name)).ToListAsync();
            _logger.LogInformation("All roles of the user with {Id} was successfully retrieved", user.Id);
            return _mapper.Map<IEnumerable<RoleResponseDto>>(roles);
        }

        public async Task<UserResponseDto> GetCurrentUserAsync()
        {
            var user = await _userManager.FindByIdAsync(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var city = await _cityRepository.GetCityById(user.CityId, false);
            user.City = city;
            var userDto = _mapper.Map<UserResponseDto>(user);
            return userDto;
        }
    }
}
