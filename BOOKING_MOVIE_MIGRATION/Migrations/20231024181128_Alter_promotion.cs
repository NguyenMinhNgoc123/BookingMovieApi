using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BOOKING_MOVIE_MIGRATION.Migrations
{
    public partial class Alter_promotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Description",
                table: "Promotion",
                nullable: true,
                oldNullable: false);
            
            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountValue",
                table: "Promotion",
                nullable: true,
                oldNullable: false);
            
            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountUnit",
                table: "Promotion",
                nullable: true,
                oldNullable: false);
            
            migrationBuilder.AlterColumn<decimal>(
                name: "AvailableFrom",
                table: "Promotion",
                nullable: true,
                oldNullable: false);
            
            migrationBuilder.AlterColumn<decimal>(
                name: "AvailableTo",
                table: "Promotion",
                nullable: true,
                oldNullable: false);
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
