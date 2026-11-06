using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vrms_api.Migrations
{
    /// <inheritdoc />
    public partial class v02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "vrms_Rates",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "vrms_Bookings",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "IdentityCardUrl",
                table: "vrms_Bookings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LocationId",
                table: "vrms_Bookings",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PaymentMethodId",
                table: "vrms_Bookings",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentProofUrl",
                table: "vrms_Bookings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleDocumentUrl",
                table: "vrms_Bookings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleRegistrationUrl",
                table: "vrms_Bookings",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "vrms_BookingDetails",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "SubTotal",
                table: "vrms_BookingDetails",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_vrms_BookingDetails_BookingId",
                table: "vrms_BookingDetails",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_vrms_BookingDetails_vrms_Bookings_BookingId",
                table: "vrms_BookingDetails",
                column: "BookingId",
                principalTable: "vrms_Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vrms_BookingDetails_vrms_Bookings_BookingId",
                table: "vrms_BookingDetails");

            migrationBuilder.DropIndex(
                name: "IX_vrms_BookingDetails_BookingId",
                table: "vrms_BookingDetails");

            migrationBuilder.DropColumn(
                name: "IdentityCardUrl",
                table: "vrms_Bookings");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "vrms_Bookings");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "vrms_Bookings");

            migrationBuilder.DropColumn(
                name: "PaymentProofUrl",
                table: "vrms_Bookings");

            migrationBuilder.DropColumn(
                name: "VehicleDocumentUrl",
                table: "vrms_Bookings");

            migrationBuilder.DropColumn(
                name: "VehicleRegistrationUrl",
                table: "vrms_Bookings");

            migrationBuilder.AlterColumn<long>(
                name: "Price",
                table: "vrms_Rates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<long>(
                name: "Total",
                table: "vrms_Bookings",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<long>(
                name: "UnitPrice",
                table: "vrms_BookingDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<long>(
                name: "SubTotal",
                table: "vrms_BookingDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
