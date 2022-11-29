using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetIdentityModel.Migrations
{
    /// <inheritdoc />
    public partial class IdentityExtend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserExtended",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserExtended",
                table: "AspNetUsers");
        }
    }
}
