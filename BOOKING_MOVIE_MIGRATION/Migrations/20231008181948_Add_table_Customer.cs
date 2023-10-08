using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BOOKING_MOVIE_MIGRATION.Migrations
{
    public partial class Add_table_Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created = table.Column<DateTime>(nullable: true),
                    status = table.Column<string>(type: "varchar(255)", nullable: true, defaultValue: "ENABLE"),
                    updated = table.Column<DateTime>(nullable: true),
                    address = table.Column<string>(type: "varchar(255)", nullable: true),
                    email = table.Column<string>(type: "varchar(255)", nullable: true),
                    mobile = table.Column<string>(type: "varchar(255)", nullable: true),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    note = table.Column<string>(type: "text", nullable: true),
                    sex = table.Column<string>(type: "varchar(255)", nullable: true),
                    created_by = table.Column<string>(type: "varchar(255)", nullable: true),
                    updated_by = table.Column<string>(type: "varchar(255)", nullable: true),
                    code = table.Column<string>(type: "varchar(45)", nullable: true),
                    CurrentLoyaltyPoint = table.Column<long>(nullable: false, defaultValue: 0),
                    Password = table.Column<string>(type: "nvarchar(300)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.UniqueConstraint("customer_phone_unique", x => new { x.mobile });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieActor");
        }
    }
}
