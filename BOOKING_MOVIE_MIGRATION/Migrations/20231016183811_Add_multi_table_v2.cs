using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BOOKING_MOVIE_MIGRATION.Migrations
{
    public partial class Add_multi_table_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cinema",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinema", x => x.Id);
                });
            
            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Director", x => x.Id);
                });
            
            migrationBuilder.CreateTable(
                name: "MovieDateSetting",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    MovieId = table.Column<long>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Director", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieDateSetting_Movie_RoomId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            
            migrationBuilder.CreateTable(
                name: "MovieCinema",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CinemaId = table.Column<long>(nullable: true),
                    MovieDateSettingId = table.Column<long>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Director", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieCinema_Cinema_CinemaId",
                        column: x => x.CinemaId,
                        principalTable: "Cinema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieCinema_MovieDateSetting_MovieDateSettingId",
                        column: x => x.MovieDateSettingId,
                        principalTable: "MovieDateSetting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieRoom",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    RoomId = table.Column<long>(nullable: true),
                    MovieCinemaId = table.Column<long>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Director", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieRoom_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieRoom_MovieCinema_MovieCinemaId",
                        column: x => x.MovieCinemaId,
                        principalTable: "MovieCinema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            
            migrationBuilder.CreateTable(
                name: "MovieTimeSetting",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Time = table.Column<string>(nullable: true),
                    MovieRoomId = table.Column<long>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Director", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieTimeSetting_MovieRoom_MovieRoomId",
                        column: x => x.MovieRoomId,
                        principalTable: "MovieRoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieCinema");
            
            migrationBuilder.DropTable(
                name: "MovieDateSetting");
            
            migrationBuilder.DropTable(
                name: "MovieRoom");
            
            migrationBuilder.DropTable(
                name: "MovieTimeSetting");
            
            migrationBuilder.DropTable(
                name: "Cinema");
            
            migrationBuilder.DropTable(
                name: "Room");
            
        }
    }
}
