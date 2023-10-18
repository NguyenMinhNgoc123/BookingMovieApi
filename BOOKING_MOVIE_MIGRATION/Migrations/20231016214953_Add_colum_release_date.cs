using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BOOKING_MOVIE_MIGRATION.Migrations
{
    public partial class Add_colum_release_date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PremiereDate",
                table: "Movie",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Movie",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_MovieTimeSetting_MovieCinemaId",
                table: "MovieTimeSetting",
                column: "MovieCinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTimeSetting_MovieDateSettingId",
                table: "MovieTimeSetting",
                column: "MovieDateSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTimeSetting_MovieRoomId",
                table: "MovieTimeSetting",
                column: "MovieRoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Movie_MovieCinemaId",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Movie_MovieDateSettingId",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Movie_MovieRoomId",
                table: "Movie");
        }
    }
}
