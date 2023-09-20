using Microsoft.AspNetCore.Mvc;
using TicketTracker.Application.Users;
using TicketTracker.Repositories.Users;

namespace TicketTracker.Api.Users
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;

        private readonly ILogger<UsersController> _logger;

        public UsersController(IUsersRepository usersRepository, ILogger<UsersController> logger)
        {
            _usersRepository = usersRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync(int? roleId)
        {
            GetUsersHandler.Request getUsersRequest = new GetUsersHandler.Request { RoleId = roleId };
            GetUsersHandler.Response getUsersResponse = await new GetUsersHandler(_usersRepository).HandleAsync(getUsersRequest);

            return Ok(getUsersResponse.Users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(int userId)
        {
            GetUserByIdHandler.Request getUserByIdRequest = new GetUserByIdHandler.Request { UserId = userId };
            GetUserByIdHandler.Response getUserByIdResponse = await new GetUserByIdHandler(_usersRepository).HandleAsync(getUserByIdRequest);

            return getUserByIdResponse.User != null ? Ok(getUserByIdResponse.User) : NotFound();
        }
    }
}