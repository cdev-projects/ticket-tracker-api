﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketTracker.Repositories;

#nullable disable

namespace TicketTracker.Repositories.Migrations
{
    [DbContext(typeof(TicketTrackerContext))]
    partial class TicketTrackerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.Property<int>("PermissionsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RolesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PermissionsId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("PermissionRole");

                    b.HasData(
                        new
                        {
                            PermissionsId = 1,
                            RolesId = 1
                        },
                        new
                        {
                            PermissionsId = 3,
                            RolesId = 1
                        },
                        new
                        {
                            PermissionsId = 4,
                            RolesId = 1
                        },
                        new
                        {
                            PermissionsId = 6,
                            RolesId = 1
                        },
                        new
                        {
                            PermissionsId = 2,
                            RolesId = 2
                        },
                        new
                        {
                            PermissionsId = 3,
                            RolesId = 2
                        },
                        new
                        {
                            PermissionsId = 5,
                            RolesId = 2
                        },
                        new
                        {
                            PermissionsId = 6,
                            RolesId = 2
                        });
                });

            modelBuilder.Entity("TicketTracker.Repositories.Tickets.Entities.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AssignedToId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ClosedById")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ClosedTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<int>("CreatedById")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("LastModifiedById")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastModifiedTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AssignedToId");

                    b.HasIndex("ClosedById");

                    b.HasIndex("CreatedById");

                    b.HasIndex("LastModifiedById");

                    b.ToTable("Tickets");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            CreatedById = 1,
                            CreatedTimestamp = new DateTime(2023, 9, 19, 22, 6, 18, 782, DateTimeKind.Utc).AddTicks(3796),
                            Description = "Customer onboarding screen needs to be developed so that customers can be dynamically added into the system.",
                            LastModifiedById = 1,
                            LastModifiedTimestamp = new DateTime(2023, 9, 19, 22, 6, 18, 782, DateTimeKind.Utc).AddTicks(3797),
                            Status = 1,
                            Title = "Application requires update to support new customers"
                        });
                });

            modelBuilder.Entity("TicketTracker.Repositories.Tickets.Entities.TicketInteraction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ReceivedById")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SentById")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("SentTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<int>("TicketId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ReceivedById");

                    b.HasIndex("SentById");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketInteractions");
                });

            modelBuilder.Entity("TicketTracker.Repositories.Tickets.Entities.TicketNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedById")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TicketId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketNotes");
                });

            modelBuilder.Entity("TicketTracker.Repositories.Users.Entities.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Create Ticket"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Assign Ticket"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Change Ticket Status"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Close Ticket"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Add Ticket Note"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Add Ticket Interaction"
                        });
                });

            modelBuilder.Entity("TicketTracker.Repositories.Users.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Customer"
                        },
                        new
                        {
                            Id = 2,
                            Name = "IT Staff"
                        });
                });

            modelBuilder.Entity("TicketTracker.Repositories.Users.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Bob",
                            RoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Jane",
                            RoleId = 1
                        },
                        new
                        {
                            Id = 3,
                            Name = "Tom",
                            RoleId = 2
                        },
                        new
                        {
                            Id = 4,
                            Name = "Samantha",
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.HasOne("TicketTracker.Repositories.Users.Entities.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketTracker.Repositories.Users.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TicketTracker.Repositories.Tickets.Entities.Ticket", b =>
                {
                    b.HasOne("TicketTracker.Repositories.Users.Entities.User", "AssignedTo")
                        .WithMany()
                        .HasForeignKey("AssignedToId");

                    b.HasOne("TicketTracker.Repositories.Users.Entities.User", "ClosedBy")
                        .WithMany()
                        .HasForeignKey("ClosedById");

                    b.HasOne("TicketTracker.Repositories.Users.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketTracker.Repositories.Users.Entities.User", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedTo");

                    b.Navigation("ClosedBy");

                    b.Navigation("CreatedBy");

                    b.Navigation("LastModifiedBy");
                });

            modelBuilder.Entity("TicketTracker.Repositories.Tickets.Entities.TicketInteraction", b =>
                {
                    b.HasOne("TicketTracker.Repositories.Users.Entities.User", "ReceivedBy")
                        .WithMany()
                        .HasForeignKey("ReceivedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketTracker.Repositories.Users.Entities.User", "SentBy")
                        .WithMany()
                        .HasForeignKey("SentById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketTracker.Repositories.Tickets.Entities.Ticket", null)
                        .WithMany("Interactions")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReceivedBy");

                    b.Navigation("SentBy");
                });

            modelBuilder.Entity("TicketTracker.Repositories.Tickets.Entities.TicketNote", b =>
                {
                    b.HasOne("TicketTracker.Repositories.Users.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketTracker.Repositories.Tickets.Entities.Ticket", null)
                        .WithMany("Notes")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("TicketTracker.Repositories.Users.Entities.User", b =>
                {
                    b.HasOne("TicketTracker.Repositories.Users.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TicketTracker.Repositories.Tickets.Entities.Ticket", b =>
                {
                    b.Navigation("Interactions");

                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}
