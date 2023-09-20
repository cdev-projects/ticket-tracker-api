using TicketTracker.Domain.Users;

namespace TicketTracker.Domain.Tickets
{
    public class Ticket
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public int Status { get; private set; }
        public User? AssignedTo { get; private set; }
        public DateTime CreatedTimestamp { get; private set; }
        public User CreatedBy { get; private set; }
        public DateTime LastModifiedTimestamp { get; private set; }
        public User LastModifiedBy { get; private set; }
        public DateTime? ClosedTimestamp { get; private set; }
        public User? ClosedBy { get; private set; }
        public IList<TicketNote> Notes { get; private set; }
        public IList<TicketInteraction> Interactions { get; private set; }

        private Ticket(string title, string? description, User createdBy, DateTime? createdTimestamp)
        {
            Title = title;
            Description = description;
            CreatedBy = createdBy;
            LastModifiedBy = createdBy;

            Status = (int)TicketStatus.New;
            CreatedTimestamp = createdTimestamp ?? DateTime.UtcNow;
            LastModifiedTimestamp = DateTime.UtcNow;
            Notes = new List<TicketNote>();
            Interactions = new List<TicketInteraction>();
        }

        public static Ticket CreateNew(string title, string? description, User createdBy, DateTime? createdTimestamp = null)
        {
            if (!createdBy.IsCustomer || !createdBy.Role.IsAbleToCreateTickets)
            {
                throw new UnauthorizedAccessException($"Error: User {createdBy.Name} is not authorized to create a ticket.");
            }
            else if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(Title));
            }

            return new Ticket(title, description, createdBy, createdTimestamp);
        }

        public static Ticket CreateFrom(int id, string title, string? description, int status, User? assignedTo,
            DateTime createdTimestamp, User createdBy, DateTime lastModifiedTimestamp, User lastModifiedBy, 
            DateTime? closedTimestamp, User? closedBy, IList<TicketNote> notes, IList<TicketInteraction> interactions)
        {
            Ticket ticket = CreateNew(title, description, createdBy, createdTimestamp);

            if (ticket != null)
            {
                ticket.Id = id;
                ticket.Status = status;
                ticket.AssignedTo = assignedTo;
                ticket.LastModifiedTimestamp = lastModifiedTimestamp;
                ticket.LastModifiedBy = lastModifiedBy;
                ticket.ClosedTimestamp = closedTimestamp;
                ticket.ClosedBy = closedBy;
                ticket.Notes = notes;
                ticket.Interactions = interactions;
            }

            return ticket!;
        }

        public void ChangeStatus(int status, User changedBy)
        {
            TicketStatus ticketStatus = (TicketStatus)status;

            if (!changedBy.Role.IsAbleToChangeTicketStatus)
            {
                throw new UnauthorizedAccessException($"Error: User {changedBy.Name} is not authorized to change the status of ticket {Id} to {ticketStatus}.");
            }

            Status = status;
            LastModifiedBy = changedBy;
            LastModifiedTimestamp = DateTime.UtcNow;

            if (ticketStatus == TicketStatus.Closed) { 
                Close(changedBy); 
            }
            else
            {
                ClosedBy = null;
                ClosedTimestamp = null;
            }
        }

        private void Close(User closedBy)
        {
            if (!closedBy.IsCustomer || !closedBy.Role.IsAbleToCloseTickets)
            {
                throw new UnauthorizedAccessException($"Error: User {closedBy.Name} is not authorized to close ticket {Id}.");
            }

            ClosedBy = closedBy;
            ClosedTimestamp = DateTime.UtcNow;
        }

        public void Assign(User assignedBy, User assignedTo)
        {
            if (!assignedBy.IsInformationTechnologyStaff || !assignedBy.Role.IsAbleToAssignTickets)
            {
                throw new UnauthorizedAccessException($"Error: User {assignedBy.Name} is not authorized to assign ticket {Id}.");
            }
            
            if (!assignedTo.IsInformationTechnologyStaff)
            {
                throw new UnauthorizedAccessException($"Error: Cannot assign ticket {Id} to non-IT staff {assignedTo.Name}.");
            }

            AssignedTo = assignedTo;
            LastModifiedBy = assignedBy; 
            LastModifiedTimestamp = DateTime.UtcNow;
        }

        public TicketNote AddNote(string message, User createdBy)
        {
            if (!createdBy.IsInformationTechnologyStaff || !createdBy.Role.IsAbleToAddNotes)
            {
                throw new UnauthorizedAccessException($"Error: User {createdBy.Name} is not authorized to add a note to ticket {Id}.");
            }

            TicketNote note = TicketNote.CreateNew(message, createdBy);

            Notes.Add(note);

            return note;
        }

        public TicketInteraction AddInteraction(string message, User sentBy, User receivedBy)
        {
            if (!sentBy.Role.IsAbleToAddInteractions)
            {
                throw new UnauthorizedAccessException($"Error: User {sentBy.Name} is not authorized to add an interaction to ticket {Id}.");
            }

            TicketInteraction interaction = TicketInteraction.CreateNew(message, sentBy, receivedBy);

            Interactions.Add(interaction);

            return interaction;
        }

        private enum TicketStatus : int
        {
            New,
            Assigned,
            InProgress,
            Completed,
            Closed
        }
    }
}