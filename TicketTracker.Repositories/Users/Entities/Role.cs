namespace TicketTracker.Repositories.Users.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }
    }
}