using TicketTracker.Repositories.Users.Entities;

namespace TicketTracker.Repositories.Tickets.Entities
{
    public class TicketInteraction
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime SentTimestamp { get; set; }
        public int SentById{ get; set; }
        public int ReceivedById { get; set; }
        public int TicketId { get; set; }

        public User SentBy { get; set; }
        public User ReceivedBy { get; set; }
    }
}