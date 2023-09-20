using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTracker.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddingNotesToTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedById = table.Column<int>(type: "INTEGER", nullable: false),
                    TicketId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketNotes_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketNotes_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedTimestamp", "LastModifiedTimestamp" },
                values: new object[] { new DateTime(2023, 9, 19, 15, 4, 22, 649, DateTimeKind.Utc).AddTicks(9277), new DateTime(2023, 9, 19, 15, 4, 22, 649, DateTimeKind.Utc).AddTicks(9278) });

            migrationBuilder.CreateIndex(
                name: "IX_TicketNotes_CreatedById",
                table: "TicketNotes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TicketNotes_TicketId",
                table: "TicketNotes",
                column: "TicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketNotes");

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedTimestamp", "LastModifiedTimestamp" },
                values: new object[] { new DateTime(2023, 9, 19, 1, 22, 53, 792, DateTimeKind.Utc).AddTicks(6701), new DateTime(2023, 9, 19, 1, 22, 53, 792, DateTimeKind.Utc).AddTicks(6703) });
        }
    }
}
