using TicketTracker.Application.Tickets.Extensions;
using TicketTracker.Domain.Tickets;
using TicketTracker.Domain.Users;
using TicketTracker.Repositories.Tickets;
using TicketTracker.Repositories.Users;

using TicketEntity = TicketTracker.Repositories.Tickets.Entities.Ticket;

namespace TicketTracker.Application.Tickets
{
    public class CreateTicketHandler
    {
        public class Request
        {
            public TicketToCreate Ticket { get; set; }

            public class TicketToCreate
            {
                public string Title { get; set; }
                public string? Description { get; set; }
                public int CreatedByUserId { get; set; }
            }
        }

        public class Response
        {
            public int TicketId { get; set; }
        }

        private ITicketsRepository _ticketsRepository;
        private IUsersRepository _usersRepository;

        public CreateTicketHandler(ITicketsRepository ticketsRepository, IUsersRepository usersRepository)
        {
            _ticketsRepository = ticketsRepository;
            _usersRepository = usersRepository;
        }

        public async Task<Response> HandleAsync(Request request)
        {
            User createdBy = await Utilities.FindUser(_usersRepository, request.Ticket.CreatedByUserId);

            Ticket ticket = Ticket.CreateNew(request.Ticket.Title, request.Ticket.Description, createdBy);

            TicketEntity ticketEntity = ticket.TranslateFromDomainToEntity();

            int createdTicketId = await _ticketsRepository.CreateTicket(ticketEntity);

            return new Response
            {
                TicketId = createdTicketId,
            };
        }
    }
}