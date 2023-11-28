using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BOOKING_MOVIE_MIGRATION.Migrations
{
    public partial class Updatecombo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Combo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(type: "varchar(255)", nullable: true, defaultValue: "ENABLE"),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar(255)", nullable: true),
                    ComboFoodId = table.Column<long>(nullable: false),
                    FoodId = table.Column<long>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Combo_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Combo_ComboFood_ComboFoodId",
                        column: x => x.ComboFoodId,
                        principalTable: "Combo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
