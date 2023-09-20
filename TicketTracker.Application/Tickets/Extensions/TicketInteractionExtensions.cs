using TicketTracker.Application.Users.Extensions;
using TicketTracker.Domain.Tickets;
using TicketTracker.Domain.Users;

using TicketInteractionEntity = TicketTracker.Repositories.Tickets.Entities.TicketInteraction;

namespace TicketTracker.Application.Tickets.Extensions
{
    internal static class TicketInteractionExtensions
    {
        internal static TicketInteraction TranslateFromEntityToDomain(this TicketInteractionEntity ticketInteractionEntity)
        {
            User? sentBy = ticketInteractionEntity.SentBy != null ? ticketInteractionEntity.SentBy.TranslateFromEntityToDomain() : null;
            User? receivedBy = ticketInteractionEntity.ReceivedBy != null ? ticketInteractionEntity.ReceivedBy.TranslateFromEntityToDomain() : null;

            return TicketInteraction.CreateFrom(ticketInteractionEntity.Id, ticketInteractionEntity.Message, sentBy!, receivedBy!, ticketInteractionEntity.SentTimestamp);
        }

        internal static TicketInteractionEntity TranslateFromDomainToEntity(this TicketInteraction ticketInteraction)
        {
            return new TicketInteractionEntity
            {
                Id = ticketInteraction.Id,
                Message = ticketInteraction.Message,
                SentTimestamp = ticketInteraction.SentTimestamp,
                SentById = ticketInteraction.SentBy.Id,
                ReceivedById = ticketInteraction.ReceivedBy.Id,
            };
        }
    }
}