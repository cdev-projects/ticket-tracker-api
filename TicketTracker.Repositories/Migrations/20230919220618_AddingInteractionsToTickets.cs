using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddingInteractionsToTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketInteractions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    SentTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SentById = table.Column<int>(type: "INTEGER", nullable: false),
                    ReceivedById = table.Column<int>(type: "INTEGER", nullable: false),
                    TicketId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketInteractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketInteractions_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketInteractions_Users_ReceivedById",
                        column: x => x.ReceivedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketInteractions_Users_SentById",
                        column: x => x.SentById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedTimestamp", "LastModifiedTimestamp" },
                values: new object[] { new DateTime(2023, 9, 19, 22, 6, 18, 782, DateTimeKind.Utc).AddTicks(3796), new DateTime(2023, 9, 19, 22, 6, 18, 782, DateTimeKind.Utc).AddTicks(3797) });

            migrationBuilder.CreateIndex(
                name: "IX_TicketInteractions_ReceivedById",
                table: "TicketInteractions",
                column: "ReceivedById");

            migrationBuilder.CreateIndex(
                name: "IX_TicketInteractions_SentById",
                table: "TicketInteractions",
                column: "SentById");

            migrationBuilder.CreateIndex(
                name: "IX_TicketInteractions_TicketId",
                table: "TicketInteractions",
                column: "TicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketInteractions");

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedTimestamp", "LastModifiedTimestamp" },
                values: new object[] { new DateTime(2023, 9, 19, 15, 4, 22, 649, DateTimeKind.Utc).AddTicks(9277), new DateTime(2023, 9, 19, 15, 4, 22, 649, DateTimeKind.Utc).AddTicks(9278) });
        }
    }
}
