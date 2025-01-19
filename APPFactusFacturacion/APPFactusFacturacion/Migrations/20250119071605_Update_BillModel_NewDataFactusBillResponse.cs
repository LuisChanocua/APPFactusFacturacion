using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APPFactusFacturacion.Migrations
{
    /// <inheritdoc />
    public partial class Update_BillModel_NewDataFactusBillResponse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BillLink",
                table: "Bills",
                newName: "ReferenceCodeFactus");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "AspNetUserTokens",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "AspNetUsers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "AspNetRoles",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "BillIdFactus",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CufeFactus",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NumberFactus",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillIdFactus",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "CufeFactus",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "NumberFactus",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "ReferenceCodeFactus",
                table: "Bills",
                newName: "BillLink");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AspNetUserTokens",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "AspNetUsers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AspNetRoles",
                newName: "name");
        }
    }
}
