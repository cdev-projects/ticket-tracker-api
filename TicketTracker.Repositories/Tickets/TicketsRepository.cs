using Microsoft.EntityFrameworkCore;
using TicketTracker.Repositories.Tickets.Entities;

namespace TicketTracker.Repositories.Tickets
{
    public class TicketsRepository : ITicketsRepository
    {
        private readonly TicketTrackerContext _ticketTrackerContext;

        public TicketsRepository(TicketTrackerContext ticketTrackerContext)
        {
            _ticketTrackerContext = ticketTrackerContext ?? throw new ArgumentNullException(nameof(ticketTrackerContext));
        }

        public async Task<IEnumerable<Ticket>> GetTicketsAsync(int? status)
        {
            return await _ticketTrackerContext.Tickets
                    .Include(ticket => ticket.AssignedTo)
                        .ThenInclude(assignedTo => assignedTo!.Role)
                        .ThenInclude(assignedToRole => assignedToRole.Permissions)
                    .Include(ticket => ticket.CreatedBy)
                        .ThenInclude(createdBy => createdBy.Role)
                        .ThenInclude(createdByRole => createdByRole.Permissions)
                    .Include(ticket => ticket.LastModifiedBy)
                        .ThenInclude(lastModifiedBy => lastModifiedBy.Role)
                        .ThenInclude(lastModifiedByRole => lastModifiedByRole.Permissions)
                    .Include(ticket => ticket.ClosedBy)
                        .ThenInclude(closedBy => closedBy!.Role)
                        .ThenInclude(closedByRole => closedByRole.Permissions)
                    .Include(ticket => ticket.Notes)
                        .ThenInclude(note => note.CreatedBy)
                        .ThenInclude(createdBy => createdBy.Role)
                        .ThenInclude(createdByRole => createdByRole.Permissions)
                    .Include(ticket => ticket.Interactions)
                        .ThenInclude(interaction => interaction.SentBy)
                        .ThenInclude(sentBy => sentBy.Role)
                        .ThenInclude(sentByRole => sentByRole.Permissions)
                    .Include(ticket => ticket.Interactions)
                        .ThenInclude(interaction => interaction.ReceivedBy)
                        .ThenInclude(receivedBy => receivedBy.Role)
                        .ThenInclude(receivedByRole => receivedByRole.Permissions)
                    .AsNoTracking()
                    .Where(ticket =>
                        (!status.HasValue || ticket.Status == status)
                    )
                    .ToListAsync();
        }

        public async Task<Ticket?> GetTicketByIdAsync(int ticketId)
        {
            return await _ticketTrackerContext.Tickets
                    .Include(ticket => ticket.AssignedTo)
                        .ThenInclude(assignedTo => assignedTo!.Role)
                        .ThenInclude(assignedToRole => assignedToRole.Permissions)
                    .Include(ticket => ticket.CreatedBy)
                        .ThenInclude(createdBy => createdBy.Role)
                        .ThenInclude(createdByRole => createdByRole.Permissions)
                    .Include(ticket => ticket.LastModifiedBy)
                        .ThenInclude(lastModifiedBy => lastModifiedBy.Role)
                        .ThenInclude(lastModifiedByRole => lastModifiedByRole.Permissions)
                    .Include(ticket => ticket.ClosedBy)
                        .ThenInclude(closedBy => closedBy!.Role)
                        .ThenInclude(closedByRole => closedByRole.Permissions)
                    .Include(ticket => ticket.Notes)
                        .ThenInclude(note => note.CreatedBy)
                        .ThenInclude(createdBy => createdBy.Role)
                        .ThenInclude(createdByRole => createdByRole.Permissions)
                    .Include(ticket => ticket.Interactions)
                        .ThenInclude(interaction => interaction.SentBy)
                        .ThenInclude(sentBy => sentBy.Role)
                        .ThenInclude(sentByRole => sentByRole.Permissions)
                    .Include(ticket => ticket.Interactions)
                        .ThenInclude(interaction => interaction.ReceivedBy)
                        .ThenInclude(receivedBy => receivedBy.Role)
                        .ThenInclude(receivedByRole => receivedByRole.Permissions)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(ticket => ticket.Id == ticketId);
        }

        public async Task<int> CreateTicket(Ticket ticket)
        {
            await _ticketTrackerContext.Tickets.AddAsync(ticket);
            _ticketTrackerContext.SaveChanges();

            return ticket.Id;
        }

        public async Task ChangeTicketStatus(int ticketId, int status, int lastModifiedByUserId, int? closedByUserId)
        {
            Ticket ticketToUpdate = await _ticketTrackerContext.Tickets.SingleAsync(ticket => ticket.Id == ticketId);
            
            ticketToUpdate.Status = status;
            ticketToUpdate.LastModifiedById = lastModifiedByUserId;
            ticketToUpdate.LastModifiedTimestamp = DateTime.UtcNow;

            ticketToUpdate.ClosedById = closedByUserId;
            ticketToUpdate.ClosedTimestamp = closedByUserId.HasValue ? DateTime.UtcNow : null;

            _ticketTrackerContext.SaveChanges();
        }

        public async Task ChangeTicketAssignment(int ticketId, int assignedToUserId, int lastModifiedByUserId)
        {
            Ticket ticketToUpdate = await _ticketTrackerContext.Tickets.SingleAsync(ticket => ticket.Id == ticketId);

            ticketToUpdate.LastModifiedById = lastModifiedByUserId;
            ticketToUpdate.LastModifiedTimestamp = DateTime.UtcNow;

            ticketToUpdate.AssignedToId = assignedToUserId;

            _ticketTrackerContext.SaveChanges();
        }

        public async Task<int> CreateTicketNote(int ticketId, TicketNote note)
        {
            note.TicketId = ticketId;

            Ticket ticketToUpdate = await _ticketTrackerContext.Tickets
                .Include(ticket => ticket.Notes)
                .SingleAsync(ticket => ticket.Id == ticketId);

            ticketToUpdate.Notes.Add(note);

            _ticketTrackerContext.SaveChanges();

            return note.Id;
        }

        public async Task<int> CreateTicketInteraction(int ticketId, TicketInteraction interaction)
        {
            interaction.TicketId = ticketId;

            Ticket ticketToUpdate = await _ticketTrackerContext.Tickets
                .Include(ticket => ticket.Interactions)
                .SingleAsync(ticket => ticket.Id == ticketId);

            ticketToUpdate.Interactions.Add(interaction);

            _ticketTrackerContext.SaveChanges();

            return interaction.Id;
        }
    }
}