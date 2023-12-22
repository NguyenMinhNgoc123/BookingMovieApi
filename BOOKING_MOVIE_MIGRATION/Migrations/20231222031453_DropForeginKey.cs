using Microsoft.EntityFrameworkCore.Migrations;

namespace BOOKING_MOVIE_MIGRATION.Migrations
{
    public partial class DropForeginKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Customer_CustomerId",
                table: "Invoice");
            
            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Customer_CustomerId",
                table: "Invoice",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
