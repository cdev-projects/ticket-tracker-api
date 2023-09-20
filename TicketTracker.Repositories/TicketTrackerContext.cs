using Microsoft.EntityFrameworkCore;
using TicketTracker.Repositories.Tickets.Entities;
using TicketTracker.Repositories.Users.Entities;

namespace TicketTracker.Repositories
{
    public class TicketTrackerContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketNote> TicketNotes { get; set; }
        public DbSet<TicketInteraction> TicketInteractions { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public TicketTrackerContext(DbContextOptions<TicketTrackerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedTicketContext(modelBuilder);
            SeedUserContext(modelBuilder);
        }

        private void SeedTicketContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .Property(ticket => ticket.Title)
                .IsRequired();

            modelBuilder.Entity<Ticket>()
                .HasData(
                    new
                    {
                        Id = -1,
                        Title = "Application requires update to support new customers",
                        Description = "Customer onboarding screen needs to be developed so that customers can be dynamically added into the system.",
                        Status = 1,
                        CreatedTimestamp = DateTime.UtcNow,
                        CreatedById = 1,
                        LastModifiedTimestamp = DateTime.UtcNow,
                        LastModifiedById = 1,
                    }
                );

            modelBuilder.Entity<TicketNote>()
                .Property(ticketNote => ticketNote.Message)
                .IsRequired();

            modelBuilder.Entity<TicketInteraction>()
                .Property(ticketInteraction => ticketInteraction.Message)
                .IsRequired();
        }

        private void SeedUserContext(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Permission>()
                .HasData(
                    new Permission { Id = 1, Name = "Create Ticket" },
                    new Permission { Id = 2, Name = "Assign Ticket" },
                    new Permission { Id = 3, Name = "Change Ticket Status" },
                    new Permission { Id = 4, Name = "Close Ticket" },
                    new Permission { Id = 5, Name = "Add Ticket Note" },
                    new Permission { Id = 6, Name = "Add Ticket Interaction" }
                );

            modelBuilder.Entity<Role>()
                .HasData(
                    new Role
                    {
                        Id = 1,
                        Name = "Customer"
                    },
                    new Role
                    {
                        Id = 2,
                        Name = "IT Staff"
                    }
                );

            modelBuilder.Entity(@"PermissionRole")
                .HasData(
                    new { RolesId = 1, PermissionsId = 1 },
                    new { RolesId = 1, PermissionsId = 3 },
                    new { RolesId = 1, PermissionsId = 4 },
                    new { RolesId = 1, PermissionsId = 6 },
                    new { RolesId = 2, PermissionsId = 2 },
                    new { RolesId = 2, PermissionsId = 3 },
                    new { RolesId = 2, PermissionsId = 5 },
                    new { RolesId = 2, PermissionsId = 6 }
                );

            modelBuilder.Entity<User>()
                .HasData(
                    new { Id = 1, Name = "Bob", RoleId = 1 },
                    new { Id = 2, Name = "Jane", RoleId = 1 },
                    new { Id = 3, Name = "Tom", RoleId = 2 },
                    new { Id = 4, Name = "Samantha", RoleId = 2 }
                );
        }
    }
}