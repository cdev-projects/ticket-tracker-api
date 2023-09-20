using TicketTracker.Domain.Users;

namespace TicketTracker.Domain.Tickets
{
    public class TicketInteraction
    {
        public int Id { get; private set; }
        public string Message { get; private set; }
        public DateTime SentTimestamp { get; private set; }
        public User SentBy { get; private set; }
        public User ReceivedBy { get; private set; }

        private TicketInteraction(string message, User sentBy, User receivedBy, DateTime? sentTimestamp)
        {
            Message = message;
            SentTimestamp = sentTimestamp ?? DateTime.UtcNow;
            SentBy = sentBy;
            ReceivedBy = receivedBy;
        }

        public static TicketInteraction CreateNew(string message, User sentBy, User receivedBy, DateTime? sentTimestamp = null)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException(nameof(message));

            if (sentBy == null) throw new ArgumentNullException(nameof(sentBy));

            if (receivedBy == null) throw new ArgumentNullException(nameof(receivedBy));

            if (
                (sentBy.IsCustomer && receivedBy.IsCustomer) || 
                (sentBy.IsInformationTechnologyStaff && receivedBy.IsInformationTechnologyStaff)
            )
            {
                throw new InvalidOperationException($"Error: Cannot add a ticket interaction between two customers or two IT staff.");
            }

            return new TicketInteraction(message, sentBy, receivedBy, sentTimestamp);
        }

        public static TicketInteraction CreateFrom(int id, string message, User sentBy, User receivedBy, DateTime sentTimestamp)
        {
            TicketInteraction ticketInteraction = CreateNew(message, sentBy, receivedBy, sentTimestamp);
            ticketInteraction.Id = id;

            return ticketInteraction;
        }
    }
}