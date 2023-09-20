using Microsoft.EntityFrameworkCore;
using TicketTracker.Repositories;
using TicketTracker.Repositories.Tickets;
using TicketTracker.Repositories.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

builder.Services.AddDbContext<TicketTrackerContext>(options =>
{
    var folder = Environment.SpecialFolder.LocalApplicationData;
    var path = Environment.GetFolderPath(folder);

    string databasePath = System.IO.Path.Join(path, "ticket-tracker.db");
    string connectionString = $"Data Source={databasePath}";

    options.UseSqlite(connectionString);
});

builder.Services.AddTransient<ITicketsRepository>(provider =>
{
    TicketTrackerContext ticketTrackerContext = provider.GetService<TicketTrackerContext>()!;

    return new TicketsRepository(ticketTrackerContext);
});

builder.Services.AddTransient<IUsersRepository>(provider =>
{
    TicketTrackerContext ticketTrackerContext = provider.GetService<TicketTrackerContext>()!;

    return new UsersRepository(ticketTrackerContext);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
