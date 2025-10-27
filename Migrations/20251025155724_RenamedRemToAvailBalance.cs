using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace oneSTIOnlineTuitionPayment.Migrations
{
    /// <inheritdoc />
    public partial class RenamedRemToAvailBalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RemBalance",
                table: "MayaTable",
                newName: "AvailBalance");

            migrationBuilder.RenameColumn(
                name: "RemBalance",
                table: "GCashTable",
                newName: "AvailBalance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvailBalance",
                table: "MayaTable",
                newName: "RemBalance");

            migrationBuilder.RenameColumn(
                name: "AvailBalance",
                table: "GCashTable",
                newName: "RemBalance");
        }
    }
}
