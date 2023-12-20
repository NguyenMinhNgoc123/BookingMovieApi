using Microsoft.EntityFrameworkCore.Migrations;

namespace BOOKING_MOVIE_MIGRATION.Migrations
{
    public partial class AddMoreInovicePayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameAtm",
                table: "InvoicePayment",
                defaultValue: null
            );
            migrationBuilder.AddColumn<string>(
                name: "NumberAtm",
                table: "InvoicePayment",
                defaultValue: null
            );
            migrationBuilder.AddColumn<string>(
                name: "NameShortCutAtm",
                table: "InvoicePayment",
                defaultValue: null
            );
            migrationBuilder.AddColumn<string>(
                name: "NotePayment",
                table: "InvoicePayment",
                defaultValue: null
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
