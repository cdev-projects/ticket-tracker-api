using TicketTracker.Application.Users.Contracts;

namespace TicketTracker.Application.Tickets.Contracts
{
    public class TicketNote
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public User CreatedBy { get; set; }
    }
}