using TicketTracker.Application.Tickets.Extensions;
using TicketTracker.Application.Users.Extensions;
using TicketTracker.Domain.Tickets;
using TicketTracker.Domain.Users;
using TicketTracker.Repositories.Tickets;
using TicketTracker.Repositories.Users;

using TicketEntity = TicketTracker.Repositories.Tickets.Entities.Ticket;
using UserEntity = TicketTracker.Repositories.Users.Entities.User;

namespace TicketTracker.Application
{
    internal static class Utilities
    {
        internal static async Task<Ticket> FindTicket(ITicketsRepository ticketsRepository, int ticketId)
        {
            TicketEntity? ticketEntity = await ticketsRepository.GetTicketByIdAsync(ticketId);

            if (ticketEntity == null)
            {
                throw new KeyNotFoundException($"Error: Unable to find ticket with id {ticketId}");
            }

            return ticketEntity!.TranslateFromEntityToDomain();
        }

        internal static async Task<User> FindUser(IUsersRepository usersRepository, int userId)
        {
            UserEntity? userEntity = await usersRepository.GetUserByIdAsync(userId);

            if (userEntity == null)
            {
                throw new KeyNotFoundException($"Error: Unable to find user with id {userId}");
            }

            return userEntity!.TranslateFromEntityToDomain();
        }
    }
}