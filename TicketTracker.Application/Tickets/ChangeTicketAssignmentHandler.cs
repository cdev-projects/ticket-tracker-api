using System.Text.Json.Serialization;
using TicketTracker.Domain.Tickets;
using TicketTracker.Domain.Users;
using TicketTracker.Repositories.Tickets;
using TicketTracker.Repositories.Users;

namespace TicketTracker.Application.Tickets
{
    public class ChangeTicketAssignmentHandler
    {
        public class Request
        {
            [JsonIgnore]
            public int TicketId { get; set; }
            public int AssignedByUserId { get; set; }
            public int AssignedToUserId { get; set; }
        }

        public class Response
        {

        }

        private ITicketsRepository _ticketsRepository;
        private IUsersRepository _usersRepository;

        public ChangeTicketAssignmentHandler(ITicketsRepository ticketsRepository, IUsersRepository usersRepository)
        {
            _ticketsRepository = ticketsRepository;
            _usersRepository = usersRepository;
        }

        public async Task<Response> HandleAsync(Request request)
        {
            User assignedBy = await Utilities.FindUser(_usersRepository, request.AssignedByUserId);

            User assignedTo = await Utilities.FindUser(_usersRepository, request.AssignedToUserId);

            Ticket ticketToUpdate = await Utilities.FindTicket(_ticketsRepository, request.TicketId);

            ticketToUpdate.Assign(assignedBy, assignedTo);

            await _ticketsRepository.ChangeTicketAssignment(ticketToUpdate.Id, ticketToUpdate.AssignedTo!.Id, ticketToUpdate.LastModifiedBy.Id);

            return new Response();
        }
    }
}