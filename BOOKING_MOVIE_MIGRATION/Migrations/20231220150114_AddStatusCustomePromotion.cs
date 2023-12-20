using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BOOKING_MOVIE_MIGRATION.Migrations
{
    public partial class AddStatusCustomePromotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPromotion_Promotion_promotionId",
                table: "CustomerPromotion");
            
            migrationBuilder.RenameColumn(
                name: "PromtionId",
                table: "CustomerPromotion",
                newName: "PromotionId");
            
            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPromotion_Promotion_promotionId",
                table: "CustomerPromotion",
                column: "PromotionId",
                principalTable: "Promotion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
