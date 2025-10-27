using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace oneSTIOnlineTuitionPayment.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAColumnInTransactiomTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "TransactionTable");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "TransactionTable");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "TransactionTable",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "TransactionTable",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
