using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace oneSTIOnlineTuitionPayment.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAmountTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Amount",
                table: "Amount");

            migrationBuilder.RenameTable(
                name: "Amount",
                newName: "AmountTable");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AmountTable",
                table: "AmountTable",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AmountTable",
                table: "AmountTable");

            migrationBuilder.RenameTable(
                name: "AmountTable",
                newName: "Amount");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Amount",
                table: "Amount",
                column: "Id");
        }
    }
}
