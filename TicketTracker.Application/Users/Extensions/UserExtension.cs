using TicketTracker.Domain.Users;

using UserContract = TicketTracker.Application.Users.Contracts.User;
using RoleContract = TicketTracker.Application.Users.Contracts.Role;
using PermissionContract = TicketTracker.Application.Users.Contracts.Permission;
using UserEntity = TicketTracker.Repositories.Users.Entities.User;

namespace TicketTracker.Application.Users.Extensions
{
    internal static class UserExtension
    {
        internal static UserContract? TranslateFromEntityToContract(this UserEntity? userEntity)
        {
            return userEntity != null ? new UserContract
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Role = new RoleContract
                {
                    Id = userEntity.Role.Id,
                    Name = userEntity.Role.Name,
                    Permissions = userEntity.Role.Permissions
                            .Select(permission => new PermissionContract
                            {
                                Id = permission.Id,
                                Name = permission.Name,
                            }
                        )
                }
            } : null;
        }

        internal static User TranslateFromEntityToDomain(this UserEntity userEntity)
        {
            IEnumerable<Permission> permissions = userEntity.Role.Permissions
                .Select(permission => Permission.CreateFrom(permission.Id, permission.Name));

            Role role = Role.CreateFrom(userEntity.Role.Id, userEntity.Role.Name, permissions);

            return User.CreateFrom(userEntity.Id, userEntity.Name, role);
        }
    }
}