using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BOOKING_MOVIE_MIGRATION.Migrations
{
    public partial class Add_created : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.AddColumn<DateTime>(
            //     name: "Created",
            //     table: "InvoiceDetails",
            //     nullable: false);
            
            // migrationBuilder.AddColumn<string>(
            //     name: "Status",
            //     table: "InvoiceDetails",
            //     nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
