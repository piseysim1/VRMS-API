using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vrms_api.Migrations
{
    /// <inheritdoc />
    public partial class v04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Profile",
                table: "vrms_Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profile",
                table: "vrms_Users");
        }
    }
}
