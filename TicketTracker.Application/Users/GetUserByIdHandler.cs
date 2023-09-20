using TicketTracker.Application.Users.Extensions;
using TicketTracker.Repositories.Users;

using UserContract = TicketTracker.Application.Users.Contracts.User;
using UserEntity = TicketTracker.Repositories.Users.Entities.User;

namespace TicketTracker.Application.Users
{
    public class GetUserByIdHandler
    {
        public class Request
        {
            public int UserId { get; set; }
        }

        public class Response
        {
            public UserContract? User { get; set; }
        }

        private IUsersRepository _usersRepository;

        public GetUserByIdHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<Response> HandleAsync(Request request)
        {
            UserEntity? userEntity = await _usersRepository.GetUserByIdAsync(request.UserId);

            UserContract? userResponse = userEntity.TranslateFromEntityToContract();

            return new Response
            {
                User = userResponse
            };
        }
    }
}