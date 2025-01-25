using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APPFactusFacturacion.Migrations
{
    /// <inheritdoc />
    public partial class Add_Columns_Bills_PersonalInfoClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientEmail",
                table: "Bills",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Bills",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClientPhoneNumber",
                table: "Bills",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientEmail",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "ClientPhoneNumber",
                table: "Bills");
        }
    }
}
