using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BOOKING_MOVIE_MIGRATION.Migrations
{
    public partial class newUpdateMigrateMovieAndPromtion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Movie",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            );

            
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Movie",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            );
            
            migrationBuilder.AddColumn<decimal>(
                name: "SpendPrice",
                table: "Promotion",
                nullable: false
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
