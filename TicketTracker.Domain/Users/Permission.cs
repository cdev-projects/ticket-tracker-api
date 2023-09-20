
namespace TicketTracker.Domain.Users
{
    public class Permission
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        private Permission(string name) 
        { 
            Name = name;
        }

        public static Permission CreateNew(string name) 
        { 
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            return new Permission(name);
        }

        public static Permission CreateFrom(int id, string name)
        {
            Permission permission = CreateNew(name);
            permission.Id = id;

            return permission;
        }
    }
}