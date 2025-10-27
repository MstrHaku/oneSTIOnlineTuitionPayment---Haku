using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace oneSTIOnlineTuitionPayment.Migrations
{
    /// <inheritdoc />
    public partial class AddedANewColumnToMayaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "MayaTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "phoneNum",
                table: "MayaTable",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "MayaTable");

            migrationBuilder.DropColumn(
                name: "phoneNum",
                table: "MayaTable");
        }
    }
}
