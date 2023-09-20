
namespace TicketTracker.Repositories.Users.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}