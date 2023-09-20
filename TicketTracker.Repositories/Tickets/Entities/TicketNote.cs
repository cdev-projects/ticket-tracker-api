using TicketTracker.Repositories.Users.Entities;

namespace TicketTracker.Repositories.Tickets.Entities
{
    public class TicketNote
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public int CreatedById { get; set; }
        public int TicketId { get; set; }

        public User CreatedBy { get; set; }
    }
}