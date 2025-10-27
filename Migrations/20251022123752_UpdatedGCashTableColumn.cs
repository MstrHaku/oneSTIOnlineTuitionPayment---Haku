using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace oneSTIOnlineTuitionPayment.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedGCashTableColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "GCashTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RemBalance",
                table: "GCashTable",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "GCashTable");

            migrationBuilder.DropColumn(
                name: "RemBalance",
                table: "GCashTable");
        }
    }
}
