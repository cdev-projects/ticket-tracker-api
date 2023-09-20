using TicketTracker.Application.Tickets.Extensions;
using TicketTracker.Repositories.Tickets;

using TicketContract = TicketTracker.Application.Tickets.Contracts.Ticket;
using TicketEntity = TicketTracker.Repositories.Tickets.Entities.Ticket;

namespace TicketTracker.Application.Tickets
{
    public class GetTicketsHandler
    {
        public class Request
        {
            public int? Status { get; set; }
        }

        public class Response
        {
            public IEnumerable<TicketContract?>? Tickets { get; set; }
        }

        private ITicketsRepository _ticketsRepository;

        public GetTicketsHandler(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
        }

        public async Task<Response> HandleAsync(Request request)
        {
            IEnumerable<TicketEntity> ticketEntities = await _ticketsRepository.GetTicketsAsync(request.Status);

            return new Response
            {
                Tickets = ticketEntities.Select(ticketEntity => ticketEntity.TranslateFromEntityToContract())
            };
        }
    }
}