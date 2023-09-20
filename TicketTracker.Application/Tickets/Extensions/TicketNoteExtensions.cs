using TicketTracker.Application.Users.Extensions;
using TicketTracker.Domain.Tickets;
using TicketTracker.Domain.Users;

using TicketNoteEntity = TicketTracker.Repositories.Tickets.Entities.TicketNote;

namespace TicketTracker.Application.Tickets.Extensions
{
    internal static class TicketNoteExtensions
    {
        internal static TicketNote TranslateFromEntityToDomain(this TicketNoteEntity ticketNoteEntity)
        {
            User? createdBy = ticketNoteEntity.CreatedBy != null ? ticketNoteEntity.CreatedBy.TranslateFromEntityToDomain() : null;

            return TicketNote.CreateFrom(ticketNoteEntity.Id, ticketNoteEntity.Message, createdBy, ticketNoteEntity.CreatedTimestamp);
        }

        internal static TicketNoteEntity TranslateFromDomainToEntity(this TicketNote ticketNote)
        {
            return new TicketNoteEntity
            {
                Id = ticketNote.Id,
                Message = ticketNote.Message,
                CreatedTimestamp = ticketNote.CreatedTimestamp,
                CreatedById = ticketNote.CreatedBy.Id,
            };
        }
    }
}