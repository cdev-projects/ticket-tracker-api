using Microsoft.EntityFrameworkCore;
using TicketTracker.Repositories.Users.Entities;

namespace TicketTracker.Repositories.Users
{
    public class UsersRepository : IUsersRepository
    {
        private readonly TicketTrackerContext _ticketTrackerContext;

        public UsersRepository(TicketTrackerContext ticketTrackerContext)
        {
            _ticketTrackerContext = ticketTrackerContext?? throw new ArgumentNullException(nameof(ticketTrackerContext));
        }

        public async Task<IEnumerable<User>> GetUsersAsync(int? roleId)
        {
            return await _ticketTrackerContext.Users
                    .Include(user => user.Role)
                    .Include(user => user.Role.Permissions)
                    .AsNoTracking()
                    .Where(user =>
                        (!roleId.HasValue || user.Role.Id == roleId)
                    )
                    .ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _ticketTrackerContext.Users
                    .Include(user => user.Role)
                    .Include(user => user.Role.Permissions)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(user => user.Id == userId);
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _ticketTrackerContext.Roles
                    .Include(role => role.Permissions)
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(int roleId)
        {
            return await _ticketTrackerContext.Roles
                    .Include(role => role.Permissions)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(role => role.Id == roleId);
        }

        public async Task<IEnumerable<Permission?>> GetPermissionsAsync()
        {
            return await _ticketTrackerContext.Permissions
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<Permission?> GetPermissionByIdAsync(int permissionId)
        {
            return await _ticketTrackerContext.Permissions
                    .AsNoTracking()
                    .SingleOrDefaultAsync(permission => permission.Id == permissionId);
        }
    }
}