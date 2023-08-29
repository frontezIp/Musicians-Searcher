using FluentValidation;
using FluentValidation.Results;
using Identity.API.Extensions;
using Identity.Application.DTOs.RequestDTOs;
using Identity.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;

namespace Identity.API.Controllers.v1
{
    [Route("api/{v:apiversion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserRequestDto> _validator;

        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger,
            IUserService userService,
            IValidator<UserRequestDto> validator)
        {
            _logger = logger;
            _validator = validator;
            _userService = userService;
        }

        [Authorize(Policy = AvailablePolicies.AdminOnlyPolicy)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsersAsync()
        {
            var usersToReturn = await _userService.GetAllUsersAsync();
            return Ok(usersToReturn);
        }

        [Authorize(Policy = AvailablePolicies.AdminOnlyPolicy)]
        [HttpGet("{userId}", Name = "UserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserAsync(Guid userId)
        {
            var userToReturn = await _userService.GetUserByIdAsync(userId);
            return Ok(userToReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserAsync(UserRequestDto userForCreation)
        {
            ValidationResult result = await _validator.ValidateAsync(userForCreation);
            if (!result.IsValid)
            {
                _logger.LogInformation("Invalid users credentials");
                result.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }
            var createdUser = await _userService.CreateUserAsync(userForCreation);
            return CreatedAtRoute("UserById", new { userId = createdUser.Id }, createdUser);
        }

        [Authorize(Policy = AvailablePolicies.AdminOnlyPolicy)]
        [HttpGet("{userId}/roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserRolesAsync(Guid userId)
        {
            var rolesToReturn = await _userService.GetUserRolesAsync(userId);
            return Ok(rolesToReturn);
        }

        [Authorize]
        [HttpGet("current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            var userToReturn = await _userService.GetCurrentUserAsync();
            return Ok(userToReturn);
        }
    }
}