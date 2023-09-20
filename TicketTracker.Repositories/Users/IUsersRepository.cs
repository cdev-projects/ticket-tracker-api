using TicketTracker.Repositories.Users.Entities;

namespace TicketTracker.Repositories.Users
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetUsersAsync(int? roleId);
        Task<User?> GetUserByIdAsync(int userId);

        Task<IEnumerable<Role>> GetRolesAsync();
        Task<Role?> GetRoleByIdAsync(int roleId);

        Task<IEnumerable<Permission?>> GetPermissionsAsync();
        Task<Permission?> GetPermissionByIdAsync(int permissionId);
    }
}