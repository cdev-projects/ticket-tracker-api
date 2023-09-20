using TicketTracker.Domain.Users;

namespace TicketTracker.Domain.Tickets
{
    public class TicketNote
    {
        public int Id { get; private set; }
        public string Message { get; private set; }
        public DateTime CreatedTimestamp { get; private set; }
        public User CreatedBy { get; private set; }

        private TicketNote (string message, User createdBy, DateTime? createdTimestamp)
        {
            Message = message;
            CreatedBy = createdBy;
            CreatedTimestamp = createdTimestamp ?? DateTime.UtcNow;
        }

        public static TicketNote CreateNew(string message, User createdBy, DateTime? createdTimestamp = null)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException(nameof(message));
            
            if (createdBy == null) throw new ArgumentNullException(nameof(createdBy));

            return new TicketNote(message, createdBy, createdTimestamp);
        }

        public static TicketNote CreateFrom(int id, string message, User createdBy, DateTime createdTimestamp)
        {
            TicketNote ticketNote = CreateNew(message, createdBy, createdTimestamp);
            ticketNote.Id = id;

            return ticketNote;
        }
    }
}