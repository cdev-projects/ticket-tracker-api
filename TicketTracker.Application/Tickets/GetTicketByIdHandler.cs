using TicketTracker.Application.Tickets.Extensions;
using TicketTracker.Repositories.Tickets;

using TicketContract = TicketTracker.Application.Tickets.Contracts.Ticket;
using TicketEntity = TicketTracker.Repositories.Tickets.Entities.Ticket;

namespace TicketTracker.Application.Tickets
{
    public class GetTicketByIdHandler
    {
        public class Request
        {
            public int TicketId { get; set; }
        }

        public class Response
        {
            public TicketContract? Ticket { get; set; }
        }

        private ITicketsRepository _ticketsRepository;

        public GetTicketByIdHandler(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
        }

        public async Task<Response> HandleAsync(Request request)
        {
            TicketEntity? ticketEntity = await _ticketsRepository.GetTicketByIdAsync(request.TicketId);

            return new Response
            {
                Ticket = ticketEntity.TranslateFromEntityToContract()
            };
        }
    }
}