using TicketTracker.Repositories.Users.Entities;

namespace TicketTracker.Repositories.Tickets.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public int? AssignedToId { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public int CreatedById { get; set; }
        public DateTime LastModifiedTimestamp { get; set; }
        public int LastModifiedById { get; set; }
        public DateTime? ClosedTimestamp { get; set; }
        public int? ClosedById { get; set; }
        public IList<TicketNote> Notes { get; set; }
        public IList<TicketInteraction> Interactions { get; set; }

        // Navigation Properties

        public User? AssignedTo { get; set; }
        public User CreatedBy { get; set; }
        public User LastModifiedBy { get; set; }
        public User? ClosedBy { get; set; }
    }
}