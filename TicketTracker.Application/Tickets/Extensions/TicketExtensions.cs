using TicketTracker.Application.Users.Extensions;
using TicketTracker.Domain.Tickets;
using TicketTracker.Domain.Users;

using TicketContract = TicketTracker.Application.Tickets.Contracts.Ticket;
using TicketNoteContract = TicketTracker.Application.Tickets.Contracts.TicketNote;
using TicketInteractionContract = TicketTracker.Application.Tickets.Contracts.TicketInteraction;
using TicketEntity = TicketTracker.Repositories.Tickets.Entities.Ticket;
using UserContract = TicketTracker.Application.Users.Contracts.User;

namespace TicketTracker.Application.Tickets.Extensions
{
    internal static class TicketExtensions
    {
        internal static TicketContract? TranslateFromEntityToContract(this TicketEntity? ticketEntity)
        {
            return ticketEntity != null ? new TicketContract
            {
                Id = ticketEntity.Id,
                Title = ticketEntity.Title,
                Description = ticketEntity.Description,
                Status = ticketEntity.Status,
                AssignedTo = ticketEntity.AssignedTo != null ? new UserContract
                {
                    Id = ticketEntity.AssignedTo.Id,
                    Name = ticketEntity.AssignedTo.Name,
                } : null,
                CreatedTimestamp = ticketEntity.CreatedTimestamp,
                CreatedBy = new UserContract
                {
                    Id = ticketEntity.CreatedBy.Id,
                    Name = ticketEntity.CreatedBy.Name,
                },
                LastModifiedTimestamp = ticketEntity.LastModifiedTimestamp,
                LastModifiedBy = new UserContract
                {
                    Id = ticketEntity.LastModifiedBy.Id,
                    Name = ticketEntity.LastModifiedBy.Name,
                },
                ClosedTimestamp = ticketEntity.ClosedTimestamp,
                ClosedBy = ticketEntity.ClosedBy != null ? new UserContract
                {
                    Id = ticketEntity.ClosedBy.Id,
                    Name = ticketEntity.ClosedBy.Name,
                } : null,
                Notes = ticketEntity.Notes.Select(note => new TicketNoteContract
                {
                    Id = note.Id,
                    Message = note.Message,
                    CreatedTimestamp = note.CreatedTimestamp,
                    CreatedBy = new UserContract
                    {
                        Id = note.CreatedBy.Id,
                        Name = note.CreatedBy.Name,
                    }
                }),
                Interactions = ticketEntity.Interactions.Select(interaction => new TicketInteractionContract
                {
                    Id = interaction.Id,
                    Message = interaction.Message,
                    SentTimestamp = interaction.SentTimestamp,
                    SentBy = new UserContract
                    {
                        Id = interaction.SentBy.Id,
                        Name = interaction.SentBy.Name,
                    },
                    ReceivedBy = new UserContract
                    {
                        Id = interaction.ReceivedBy.Id,
                        Name = interaction.ReceivedBy.Name,
                    }
                })
            } : null;
        }

        internal static Ticket TranslateFromEntityToDomain(this TicketEntity ticketEntity)
        {
            User? assignedTo = ticketEntity.AssignedTo != null ? ticketEntity.AssignedTo.TranslateFromEntityToDomain() : null;
            User? createdBy = ticketEntity.CreatedBy != null ? ticketEntity.CreatedBy.TranslateFromEntityToDomain() : null;
            User? lastModifiedBy = ticketEntity.LastModifiedBy != null ? ticketEntity.LastModifiedBy.TranslateFromEntityToDomain() : null;
            User? closedBy = ticketEntity.ClosedBy != null ? ticketEntity.ClosedBy.TranslateFromEntityToDomain() : null;

            IList<TicketNote> ticketNotes = ticketEntity.Notes.Select(ticketNoteEntity => ticketNoteEntity.TranslateFromEntityToDomain()).ToList();
            IList<TicketInteraction> ticketInteractions = ticketEntity.Interactions.Select(ticketInteractionEntity => ticketInteractionEntity.TranslateFromEntityToDomain()).ToList();

            return Ticket.CreateFrom(ticketEntity.Id, ticketEntity.Title, ticketEntity.Description,
                ticketEntity.Status, assignedTo, ticketEntity.CreatedTimestamp, createdBy!,
                ticketEntity.LastModifiedTimestamp, lastModifiedBy!, ticketEntity.ClosedTimestamp, 
                closedBy, ticketNotes, ticketInteractions);
        }

        internal static TicketEntity TranslateFromDomainToEntity(this Ticket ticket)
        {
            return new TicketEntity
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                AssignedToId = ticket.AssignedTo != null ? ticket.AssignedTo.Id : null,
                CreatedTimestamp = ticket.CreatedTimestamp,
                CreatedById = ticket.CreatedBy.Id,
                LastModifiedTimestamp = ticket.LastModifiedTimestamp,
                LastModifiedById = ticket.LastModifiedBy.Id,
                ClosedTimestamp = ticket.ClosedTimestamp,
                ClosedById = ticket.ClosedBy != null ? ticket.ClosedBy.Id : null,
            };
        }
    }
}