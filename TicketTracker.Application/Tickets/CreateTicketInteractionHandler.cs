using System.Text.Json.Serialization;
using TicketTracker.Application.Tickets.Extensions;
using TicketTracker.Domain.Tickets;
using TicketTracker.Domain.Users;
using TicketTracker.Repositories.Tickets;
using TicketTracker.Repositories.Users;

using TicketInteractionEntity = TicketTracker.Repositories.Tickets.Entities.TicketInteraction;

namespace TicketTracker.Application.Tickets
{
    public class CreateTicketInteractionHandler
    {
        public class Request
        {
            public TicketInteractionToCreate TicketInteraction { get; set; }

            public class TicketInteractionToCreate
            {
                [JsonIgnore]
                public int TicketId { get; set; }
                public string Message { get; set; }
                public int SentByUserId { get; set; }
                public int ReceivedByUserId { get; set; }
            }
        }

        public class Response
        {
            public int TicketInteractionId { get; set; }
        }

        private ITicketsRepository _ticketsRepository;
        private IUsersRepository _usersRepository;

        public CreateTicketInteractionHandler(ITicketsRepository ticketsRepository, IUsersRepository usersRepository)
        {
            _ticketsRepository = ticketsRepository;
            _usersRepository = usersRepository;
        }

        public async Task<Response> HandleAsync(Request request)
        {
            User sentBy = await Utilities.FindUser(_usersRepository, request.TicketInteraction.SentByUserId);
            User receivedBy = await Utilities.FindUser(_usersRepository, request.TicketInteraction.ReceivedByUserId);

            Ticket ticketToUpdate = await Utilities.FindTicket(_ticketsRepository, request.TicketInteraction.TicketId);

            TicketInteraction ticketInteraction = ticketToUpdate.AddInteraction(request.TicketInteraction.Message, sentBy, receivedBy);

            TicketInteractionEntity ticketInteractionEntity = ticketInteraction.TranslateFromDomainToEntity();

            int createdTicketInteractionId = await _ticketsRepository.CreateTicketInteraction(ticketToUpdate.Id, ticketInteractionEntity);

            return new Response
            {
                TicketInteractionId = createdTicketInteractionId,
            };
        }
    }
}