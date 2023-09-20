using TicketTracker.Application.Users.Extensions;
using TicketTracker.Repositories.Users;

using UserContract = TicketTracker.Application.Users.Contracts.User;
using UserEntity = TicketTracker.Repositories.Users.Entities.User;

namespace TicketTracker.Application.Users
{
    public class GetUsersHandler
    {
        public class Request
        {
            public int? RoleId { get; set; }
        }

        public class Response
        {
            public IEnumerable<UserContract?>? Users { get; set; }
        }

        private IUsersRepository _usersRepository;

        public GetUsersHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<Response> HandleAsync(Request request)
        {
            IEnumerable<UserEntity> userEntities = await _usersRepository.GetUsersAsync(request.RoleId);

            return new Response
            {
                Users = userEntities.Select(userEntity => userEntity.TranslateFromEntityToContract())
            };
        }
    }
}