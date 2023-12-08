using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BOOKING_MOVIE_MIGRATION.Migrations
{
    public partial class updateInvoiceDetailsForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_MovieTimeSetting_MovieTimeSettingId",
                table: "InvoiceDetails",
                column: "MovieTimeSettingId",
                principalTable: "MovieTimeSetting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
            
            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_MovieDateSetting_MovieDateSettingId",
                table: "InvoiceDetails",
                column: "MovieDateSettingId",
                principalTable: "MovieDateSetting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
            
            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_Rooms_RoomId",
                table: "InvoiceDetails",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
            
            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_Cinema_CinemaId",
                table: "InvoiceDetails",
                column: "CinemaId",
                principalTable: "Cinema",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
