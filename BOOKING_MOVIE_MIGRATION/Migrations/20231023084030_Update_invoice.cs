using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BOOKING_MOVIE_MIGRATION.Migrations
{
    public partial class Update_invoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.CreateTable(
            //     name: "Food",
            //     columns: table => new
            //     {
            //         Id = table.Column<long>(nullable: false)
            //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //         CreatedBy = table.Column<string>(nullable: true),
            //         UpdatedBy = table.Column<string>(nullable: true),
            //         Updated = table.Column<DateTime>(nullable: true),
            //         Created = table.Column<DateTime>(nullable: true),
            //         Status = table.Column<string>(nullable: true),
            //         Name = table.Column<string>(nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Food", x => x.Id);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "ComboFood",
            //     columns: table => new
            //     {
            //         Id = table.Column<long>(nullable: false)
            //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //         CreatedBy = table.Column<string>(nullable: true),
            //         UpdatedBy = table.Column<string>(nullable: true),
            //         Updated = table.Column<DateTime>(nullable: true),
            //         Created = table.Column<DateTime>(nullable: true),
            //         Status = table.Column<string>(nullable: true),
            //         Name = table.Column<string>(nullable: true),
            //         FoodId = table.Column<long>(nullable: false),
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Food", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_InvoiceDetails_Food_FoodId",
            //             column: x => x.FoodId,
            //             principalTable: "Food",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "Invoice",
            //     columns: table => new
            //     {
            //         Id = table.Column<long>(nullable: false)
            //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //         CreatedBy = table.Column<string>(nullable: true),
            //         UpdatedBy = table.Column<string>(nullable: true),
            //         Updated = table.Column<DateTime>(nullable: true),
            //         Created = table.Column<DateTime>(nullable: true),
            //         Status = table.Column<string>(type: "varchar(255)", nullable: true, defaultValue: "ENABLE"),
            //         Code = table.Column<string>(nullable: true),
            //         CustomerId = table.Column<long>(nullable: true),
            //         DiscountUnit = table.Column<string>(nullable: true),
            //         DiscountValue = table.Column<decimal>(nullable: false),
            //         IsDisplay = table.Column<bool>(nullable: true),
            //         Note = table.Column<string>(nullable: true),
            //         Total = table.Column<decimal>(nullable: true),
            //         NotePayment = table.Column<string>(nullable: true),
            //         NoteArrangement = table.Column<string>(nullable: true),
            //         CashBack = table.Column<decimal>(nullable: true),
            //         TotalDetails = table.Column<decimal>(nullable: false),
            //         PaymentStatus = table.Column<string>(nullable: true),
            //         BookingId = table.Column<long>(nullable: true),
            //         PaidTotal = table.Column<decimal>(nullable: true),
            //         LoyaltyPoint = table.Column<long>(nullable: true),
            //         DiscountTotal = table.Column<decimal>(nullable: true),
            //         Type = table.Column<string>(nullable: true),
            //         PaidAt = table.Column<DateTime>(nullable: true),
            //         PromotionId = table.Column<long>(nullable: true)
            //     },
            //     
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Invoice", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_Invoice_Promotion_PromotionId",
            //             column: x => x.PromotionId,
            //             principalTable: "Promotion",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_Invoice_Customer_CustomerId",
            //             column: x => x.PromotionId,
            //             principalTable: "Customer",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "InvoiceDetails",
            //     columns: table => new
            //     {
            //         Id = table.Column<long>(nullable: false)
            //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //         CreatedBy = table.Column<string>(nullable: true),
            //         InvoiceId = table.Column<long>(nullable: true),
            //         MovieId = table.Column<long>(nullable: true),
            //         RoomId = table.Column<long>(nullable: true),
            //         CinemaId = table.Column<long>(nullable: true),
            //         MovieDateSettingIds = table.Column<long>(nullable: true),
            //         MovieTimeSettingIds = table.Column<long>(nullable: true),
            //         ObjectId = table.Column<long>(nullable: true),
            //         ObjectName = table.Column<string>(nullable: true),
            //         ObjectCode = table.Column<string>(nullable: true),
            //         ObjectPrice = table.Column<decimal>(nullable: false),
            //         DiscountUnit = table.Column<string>(nullable: true),
            //         DiscountValue = table.Column<decimal>(nullable: true),
            //         Total = table.Column<decimal>(nullable: true),
            //         Quantity = table.Column<decimal>(nullable: true),
            //         IsPaid = table.Column<bool>(nullable: true),
            //         PromotionId = table.Column<long>(nullable: true),
            //         Updated = table.Column<DateTime>(nullable: true),
            //         UpdatedBy = table.Column<string>(type: "varchar(255)", nullable: true),
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_InvoiceDetails", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_InvoiceDetails_Invoice_InvoiceId",
            //             column: x => x.InvoiceId,
            //             principalTable: "Invoice",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_InvoiceDetails_Room_RoomId",
            //             column: x => x.RoomId,
            //             principalTable: "Room",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_InvoicePayment_Movie_MovieId",
            //             column: x => x.MovieId,
            //             principalTable: "Movie",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_InvoicePayment_Cinema_CinemaId",
            //             column: x => x.CinemaId,
            //             principalTable: "Cinema",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            
            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    CustomerId = table.Column<long>(nullable: true),
                    DiscountUnit = table.Column<string>(nullable: true),
                    DiscountValue = table.Column<decimal>(nullable: false),
                    IsDisplay = table.Column<bool>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Total = table.Column<decimal>(nullable: true),
                    NotePayment = table.Column<string>(nullable: true),
                    NoteArrangement = table.Column<string>(nullable: true),
                    CashBack = table.Column<decimal>(nullable: true),
                    TotalDetails = table.Column<decimal>(nullable: false),
                    PaymentStatus = table.Column<string>(nullable: true),
                    BookingId = table.Column<long>(nullable: true),
                    PaidTotal = table.Column<decimal>(nullable: true),
                    LoyaltyPoint = table.Column<long>(nullable: true),
                    DiscountTotal = table.Column<decimal>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    PaidAt = table.Column<DateTime>(nullable: true),
                    PromotionId = table.Column<long>(nullable: true),
                    CashierId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_User_CashierId",
                        column: x => x.CashierId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComboFood",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    FoodId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboFood", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComboFood_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    InvoiceId = table.Column<long>(nullable: true),
                    MovieId = table.Column<long>(nullable: true),
                    RoomId = table.Column<long>(nullable: true),
                    CinemaId = table.Column<long>(nullable: true),
                    MovieDateSettingIds = table.Column<long>(nullable: true),
                    MovieTimeSettingIds = table.Column<long>(nullable: true),
                    ObjectId = table.Column<long>(nullable: true),
                    ObjectName = table.Column<string>(nullable: true),
                    ObjectCode = table.Column<string>(nullable: true),
                    ObjectPrice = table.Column<decimal>(nullable: false),
                    DiscountUnit = table.Column<string>(nullable: true),
                    DiscountValue = table.Column<decimal>(nullable: true),
                    Total = table.Column<decimal>(nullable: true),
                    Quantity = table.Column<decimal>(nullable: true),
                    IsPaid = table.Column<bool>(nullable: true),
                    PromotionId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayment_InvoiceId",
                table: "InvoicePayment",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboFood_FoodId",
                table: "ComboFood",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CashierId",
                table: "Invoice",
                column: "CashierId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CustomerId",
                table: "Invoice",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_PromotionId",
                table: "Invoice",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_InvoiceId",
                table: "InvoiceDetails",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_MovieId",
                table: "InvoiceDetails",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_PromotionId",
                table: "InvoiceDetails",
                column: "PromotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayment_Invoice_InvoiceId",
                table: "InvoicePayment",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePayment_Invoice_InvoiceId",
                table: "InvoicePayment");

            migrationBuilder.DropTable(
                name: "ComboFood");

            migrationBuilder.DropTable(
                name: "InvoiceDetails");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_InvoicePayment_InvoiceId",
                table: "InvoicePayment");
        }
    }
}
