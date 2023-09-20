
namespace TicketTracker.Domain.Users
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Role Role { get; private set; }

        public bool IsCustomer => HasRole(RoleType.Customer);
        public bool IsInformationTechnologyStaff => HasRole(RoleType.ITStaff);

        private User(string name, Role role)
        {
            Name = name;
            Role = role;
        }

        public static User CreateNew(string name, Role role)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            if (role == null) throw new ArgumentNullException(nameof(role));

            return new User(name, role);
        }

        public static User CreateFrom(int id, string name, Role role)
        {
            User user = CreateNew(name, role);
            user.Id = id;

            return user;
        }

        private bool HasRole(RoleType roleType) => Role.Id == (byte) roleType;

        private enum RoleType
        {
            Customer = 1,
            ITStaff = 2,
        }
    }
}