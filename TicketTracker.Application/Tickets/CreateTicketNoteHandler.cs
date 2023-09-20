using System.Text.Json.Serialization;
using TicketTracker.Application.Tickets.Extensions;
using TicketTracker.Domain.Tickets;
using TicketTracker.Domain.Users;
using TicketTracker.Repositories.Tickets;
using TicketTracker.Repositories.Users;

using TicketNoteEntity = TicketTracker.Repositories.Tickets.Entities.TicketNote;

namespace TicketTracker.Application.Tickets
{
    public class CreateTicketNoteHandler
    {
        public class Request
        {
            public TicketNoteToCreate TicketNote { get; set; }

            public class TicketNoteToCreate
            {
                [JsonIgnore]
                public int TicketId { get; set; }
                public string Message { get; set; }
                public int CreatedByUserId { get; set; }
            }
        }

        public class Response
        {
            public int TicketNoteId { get; set; }
        }

        private ITicketsRepository _ticketsRepository;
        private IUsersRepository _usersRepository;

        public CreateTicketNoteHandler(ITicketsRepository ticketsRepository, IUsersRepository usersRepository)
        {
            _ticketsRepository = ticketsRepository;
            _usersRepository = usersRepository;
        }

        public async Task<Response> HandleAsync(Request request)
        {
            User createdBy = await Utilities.FindUser(_usersRepository, request.TicketNote.CreatedByUserId);

            Ticket ticketToUpdate = await Utilities.FindTicket(_ticketsRepository, request.TicketNote.TicketId);

            TicketNote ticketNote = ticketToUpdate.AddNote(request.TicketNote.Message, createdBy);

            TicketNoteEntity ticketNoteEntity = ticketNote.TranslateFromDomainToEntity();

            int createdTicketNoteId = await _ticketsRepository.CreateTicketNote(ticketToUpdate.Id, ticketNoteEntity);

            return new Response
            {
                TicketNoteId = createdTicketNoteId,
            };
        }
    }
}