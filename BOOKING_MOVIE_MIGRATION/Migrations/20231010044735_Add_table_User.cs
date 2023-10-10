using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BOOKING_MOVIE_MIGRATION.Migrations
{
    public partial class Add_table_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhotoId = table.Column<long>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false),
                    Mobile = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(type: "varchar(255)", nullable: true, defaultValue: "ENABLE"),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar(255)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
