using TicketTracker.Repositories.Tickets.Entities;

namespace TicketTracker.Repositories.Tickets
{
    public interface ITicketsRepository
    {
        Task<IEnumerable<Ticket>> GetTicketsAsync(int? status);
        Task<Ticket?> GetTicketByIdAsync(int ticketId);
        Task<int> CreateTicket(Ticket ticket);
        Task ChangeTicketStatus(int ticketId, int status, int lastModifiedByUserId, int? closedByUserId);
        Task ChangeTicketAssignment(int ticketId, int assignedToUserId, int lastModifiedByUserId);
        Task<int> CreateTicketNote(int ticketId, TicketNote note);
        Task<int> CreateTicketInteraction(int ticketId, TicketInteraction interaction);
    }
}