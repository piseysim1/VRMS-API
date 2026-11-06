using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vrms_api.Migrations
{
    /// <inheritdoc />
    public partial class v03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StatusId",
                table: "vrms_Bookings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "vrms_Bookings");
        }
    }
}
