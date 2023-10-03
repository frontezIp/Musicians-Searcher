using Identity.Application.DTOs.RequestDTOs;
using Identity.Application.DTOs.ResponseDTOs;
using Identity.Application.Interfaces.Services;
using Identity.Domain.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Identity.Domain.Exceptions.BadRequestException;
using Microsoft.EntityFrameworkCore;
using Identity.Application.Interfaces.Persistance;
using Microsoft.Extensions.Logging;
using Shared.Constants;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Identity.Application.Interfaces.ServiceHelpers;
using Identity.Application.Interfaces.MessageBroker.Producer;
using Shared.Messages.IdentityMessages;
using Microsoft.Extensions.Options;
using Identity.Application.Options;

namespace Identity.Application.Services
{
    internal class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ICityRepository _cityRepository;
        private readonly IUserServiceHelper _userServiceHelper;
        private readonly ICityServiceHelper _cityServiceHelper;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITransactionalEventProducer _eventProducer;
        private readonly IOptions<KafkaTopicOptions> _options;

        public UserService(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IMapper mapper,
            IUserServiceHelper userServiceHelper,
            ICityServiceHelper cityServiceHelper,
            ICityRepository cityRepository,
            ILogger<UserService> logger,
            IHttpContextAccessor contextAccessor,
            ITransactionalEventProducer eventProducer,
            IOptions<KafkaTopicOptions> options)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _userServiceHelper = userServiceHelper;
            _cityServiceHelper = cityServiceHelper;
            _cityRepository = cityRepository;
            _logger = logger;
            _contextAccessor = contextAccessor;
            _eventProducer = eventProducer;
            _options = options;
        }

        public async Task<UserResponseDto> CreateUserAsync(UserRequestDto user)
        {
            var city = await _cityServiceHelper.CheckIfCityExistsAndGetAsync(user.CityId, false);
            var createdUser = _mapper.Map<User>(user);
            var result = await _userManager.CreateAsync(createdUser, user.Password);
            if (!result.Succeeded)
            {
                throw new UserInvalidCredentialsBadRequestException(result.Errors.First().Description.ToString() ?? "Invalid credentials");
            }
            _logger.LogInformation("User with {Id} id was successfully created", createdUser.Id);
            createdUser.City = city;
            var userCreatedMessage = _mapper.Map<UserCreatedMessage>(createdUser);
            await _eventProducer.ProduceAsync<UserCreatedMessage>(_options.Value.IdentityTopic, createdUser.Id.ToString(), userCreatedMessage);
            await _cityRepository.SaveChangesAsync();
            await _userManager.AddToRoleAsync(createdUser, RoleNamesConstants.UserRoleName);
            var userDto = _mapper.Map<UserResponseDto>(createdUser);
            return userDto;
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
            var user = await _userServiceHelper.CheckIfUserExistsAndGetByIdAsync(id);
            var city = await _cityRepository.GetByIdAsync(user.CityId, false);
            user.City = city;
            var userDto = _mapper.Map<UserResponseDto>(user);
            _logger.LogInformation("User with {Id} id was successfully retrieved", user.Id);

            return userDto;
        }

        public async Task<IEnumerable<RoleResponseDto>> GetUserRolesAsync(Guid userId)
        {
            var user = await _userServiceHelper.CheckIfUserExistsAndGetByIdAsync(userId);
            var roleNames = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.AsNoTracking().Where(r => roleNames.Contains(r.Name)).ToListAsync();
            _logger.LogInformation("All roles of the user with {Id} was successfully retrieved", user.Id);

            return _mapper.Map<IEnumerable<RoleResponseDto>>(roles);
        }

        public async Task<UserResponseDto> GetCurrentUserAsync()
        {
            var user = await _userManager.FindByIdAsync(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var city = await _cityRepository.GetByIdAsync(user.CityId, false);
            user.City = city;
            var userDto = _mapper.Map<UserResponseDto>(user);

            return userDto;
        }
    }
}
