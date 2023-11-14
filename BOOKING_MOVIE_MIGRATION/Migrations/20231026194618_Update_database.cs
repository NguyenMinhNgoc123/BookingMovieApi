using Microsoft.EntityFrameworkCore.Migrations;

namespace BOOKING_MOVIE_MIGRATION.Migrations
{
    public partial class Update_database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Genre");
            
            migrationBuilder.RenameTable(
                name: "MovieCategories",
                newName: "MovieGenres");
            
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCategories_Category_CategoryId",
                table: "MovieGenres"
            );
            
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "MovieGenres"
                );
            
            migrationBuilder.AddColumn<long>(
                name: "GenreId",
                table: "MovieGenres"
            );
            
            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Genre_GenreId",
                table: "MovieGenres",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
            
            migrationBuilder.AddColumn<string>(
                name: "Overview",
                table: "Movie"
            );
            
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Video"
            );
            
            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "Movie",
                type: "decimal(18, 2)",
                oldType: "longtext"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
