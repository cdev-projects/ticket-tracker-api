using TicketTracker.Application.Users.Contracts;

namespace TicketTracker.Application.Tickets.Contracts
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public User? AssignedTo { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public User CreatedBy { get; set; }
        public DateTime LastModifiedTimestamp { get; set; }
        public User LastModifiedBy { get; set; }
        public DateTime? ClosedTimestamp { get; set; }
        public User? ClosedBy { get; set; }
        public IEnumerable<TicketNote> Notes { get; set; }
        public IEnumerable<TicketInteraction> Interactions { get; set; }
    }
}