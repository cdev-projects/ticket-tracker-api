﻿
namespace TicketTracker.Application.Users.Contracts
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
    }
}