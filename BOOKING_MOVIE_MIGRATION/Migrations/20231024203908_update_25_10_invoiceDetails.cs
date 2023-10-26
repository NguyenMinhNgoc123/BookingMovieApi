using Microsoft.EntityFrameworkCore.Migrations;

namespace BOOKING_MOVIE_MIGRATION.Migrations
{
    public partial class update_25_10_invoiceDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MovieTimeSettingIds",
                table: "InvoiceDetails",
                newName: "MovieTimeSettingId");

            migrationBuilder.RenameColumn(
                name: "MovieDateSettingIds",
                table: "InvoiceDetails",
                newName: "MovieDateSettingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MovieTimeSettingId",
                table: "InvoiceDetails",
                newName: "MovieTimeSettingIds");

            migrationBuilder.RenameColumn(
                name: "MovieDateSettingId",
                table: "InvoiceDetails",
                newName: "MovieDateSettingIds");
        }
    }
}
