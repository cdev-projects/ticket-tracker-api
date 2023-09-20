using TicketTracker.Application.Users.Contracts;

namespace TicketTracker.Application.Tickets.Contracts
{
    public class TicketInteraction
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime SentTimestamp { get; set; }
        public User SentBy { get; set; }
        public User ReceivedBy { get; set; }
    }
}