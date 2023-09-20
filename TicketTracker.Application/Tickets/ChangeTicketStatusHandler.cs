using System.Text.Json.Serialization;
using TicketTracker.Domain.Tickets;
using TicketTracker.Domain.Users;
using TicketTracker.Repositories.Tickets;
using TicketTracker.Repositories.Users;

namespace TicketTracker.Application.Tickets
{
    public class ChangeTicketStatusHandler
    {
        public class Request
        {
            [JsonIgnore]
            public int TicketId { get; set; }
            public int Status { get; set; }
            public int ChangedByUserId { get; set; }
        }

        public class Response
        {

        }

        private ITicketsRepository _ticketsRepository;
        private IUsersRepository _usersRepository;

        public ChangeTicketStatusHandler(ITicketsRepository ticketsRepository, IUsersRepository usersRepository)
        {
            _ticketsRepository = ticketsRepository;
            _usersRepository = usersRepository;
        }

        public async Task<Response> HandleAsync(Request request)
        {
            User changedBy = await Utilities.FindUser(_usersRepository, request.ChangedByUserId);

            Ticket ticketToUpdate = await Utilities.FindTicket(_ticketsRepository, request.TicketId);

            ticketToUpdate.ChangeStatus(request.Status, changedBy);

            await _ticketsRepository.ChangeTicketStatus(ticketToUpdate.Id, ticketToUpdate.Status, 
                ticketToUpdate.LastModifiedBy.Id, ticketToUpdate.ClosedBy?.Id);

            return new Response();
        }
    }
}