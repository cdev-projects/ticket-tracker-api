
namespace TicketTracker.Domain.Users
{
    public class Role
    {
        private readonly IEnumerable<Permission> _permissions;

        public int Id { get; private set; }
        public string Name { get; private set; }

        public bool IsAbleToCreateTickets => HasPermission(PermissionType.CreateTicket);
        public bool IsAbleToChangeTicketStatus => HasPermission(PermissionType.ChangeTicketStatus);
        public bool IsAbleToAssignTickets => HasPermission(PermissionType.AssignTicket);
        public bool IsAbleToCloseTickets => HasPermission(PermissionType.CloseTicket);
        public bool IsAbleToAddNotes => HasPermission(PermissionType.AddTicketNote);
        public bool IsAbleToAddInteractions => HasPermission(PermissionType.AddTicketInteraction);

        private Role(string name, IEnumerable<Permission> permissions)
        {
            Name = name;

            _permissions = permissions;
        }

        public static Role CreateNew(string name, IEnumerable<Permission> permissions)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            if (permissions == null || !permissions.Any()) throw new ArgumentNullException(nameof(permissions));

            return new Role(name, permissions);
        }

        public static Role CreateFrom(int id, string name, IEnumerable<Permission> permissions)
        {
            Role role = CreateNew(name, permissions);
            role.Id = id;
            
            return role;
        }

        private bool HasPermission(PermissionType permissionType)
        {
            return _permissions.Any(permission => permission.Id == (byte)permissionType);
        }

        private enum PermissionType
        {
            CreateTicket = 1,
            AssignTicket = 2,
            ChangeTicketStatus = 3,
            CloseTicket = 4,
            AddTicketNote = 5,
            AddTicketInteraction = 6
        }
    }
}