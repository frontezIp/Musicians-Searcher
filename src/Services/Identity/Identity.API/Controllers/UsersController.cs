using Identity.Domain.Models;
using Identity.Infrastructure.Persistance.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly IdentityContext _context;

        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, IdentityContext identityContext)
        {
            _logger = logger;
            _context = identityContext;
        }

        [HttpGet]
        public int Get()
        {
            return _context.Set<User>().Count();
            
        }
    }
}