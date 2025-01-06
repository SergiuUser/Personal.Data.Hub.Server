using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Personal.Dara.Hub.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_more_fields_to_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Workspaces",
                newName: "Identifier");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Workspaces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Workspaces");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Identifier",
                table: "Workspaces",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
