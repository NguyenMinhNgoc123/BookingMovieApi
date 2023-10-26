﻿// <auto-generated />
using System;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BOOKING_MOVIE_MIGRATION.Migrations
{
    [DbContext(typeof(movie_context))]
    partial class movie_contextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.Actor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Name");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("Actor");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Name");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.Cinema", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Name");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("Cinema");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.ComboFood", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<long>("FoodId");

                    b.Property<string>("Name");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.ToTable("ComboFood");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.Customer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Code");

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<long>("CurrentLoyaltyPoint");

                    b.Property<string>("Email");

                    b.Property<string>("Mobile");

                    b.Property<string>("Name");

                    b.Property<string>("Note");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Sex");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.CustomerPromotion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<long>("CustomerId");

                    b.Property<long>("PromotionId");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PromotionId");

                    b.ToTable("CustomerPromotion");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.Director", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Name");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("Director");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.Food", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Name");

                    b.Property<decimal?>("Price");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("Food");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.Invoice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<long?>("CustomerId");

                    b.Property<decimal?>("DiscountTotal");

                    b.Property<string>("DiscountUnit");

                    b.Property<decimal?>("DiscountValue");

                    b.Property<bool?>("IsDisplay");

                    b.Property<string>("Note");

                    b.Property<string>("NotePayment");

                    b.Property<DateTime?>("PaidAt");

                    b.Property<decimal?>("PaidTotal");

                    b.Property<string>("PaymentStatus");

                    b.Property<long?>("PromotionId");

                    b.Property<string>("Status");

                    b.Property<decimal?>("Total");

                    b.Property<decimal>("TotalDetails");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PromotionId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.InvoiceDetails", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CinemaId");

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("DiscountUnit");

                    b.Property<decimal?>("DiscountValue");

                    b.Property<long?>("InvoiceId");

                    b.Property<bool?>("IsPaid");

                    b.Property<long?>("MovieDateSettingId");

                    b.Property<long?>("MovieId");

                    b.Property<long?>("MovieTimeSettingId");

                    b.Property<string>("ObjectCode");

                    b.Property<long?>("ObjectId");

                    b.Property<string>("ObjectName");

                    b.Property<decimal>("ObjectPrice");

                    b.Property<long?>("PromotionId");

                    b.Property<decimal>("Quantity");

                    b.Property<long?>("RoomId");

                    b.Property<string>("Status");

                    b.Property<decimal>("Total");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("MovieId");

                    b.HasIndex("PromotionId");

                    b.ToTable("InvoiceDetails");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.InvoicePayment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<long>("InvoiceId");

                    b.Property<long>("InvoiceMethodId");

                    b.Property<string>("Status");

                    b.Property<decimal?>("Total");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("InvoiceMethodId");

                    b.ToTable("InvoicePayment");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.Movie", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country");

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<string>("MovieStatus");

                    b.Property<string>("Name");

                    b.Property<DateTime>("PremiereDate");

                    b.Property<string>("Rate");

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<string>("Status");

                    b.Property<string>("Time");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.Property<string>("YearOfRelease");

                    b.HasKey("Id");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.MovieActor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ActorId");

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<long>("MovieId");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieActors");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.MovieCategories", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CategoryId");

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<long>("MovieId");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieCategories");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.MovieCinema", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CinemaId")
                        .HasColumnName("CinemaId");

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<long>("MovieDateSettingId");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("CinemaId");

                    b.HasIndex("MovieDateSettingId");

                    b.ToTable("MovieCinema");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.MovieDateSetting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<long>("MovieId");

                    b.Property<string>("Status");

                    b.Property<DateTime>("Time");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieDateSetting");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.MovieDirector", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<long>("DirectorId");

                    b.Property<long>("MovieId");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("DirectorId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieDirectors");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.MovieRoom", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<long>("MovieCinemaId");

                    b.Property<long>("RoomId");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("MovieCinemaId");

                    b.HasIndex("RoomId");

                    b.ToTable("MovieRoom");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.MovieTimeSetting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<long>("MovieRoomId");

                    b.Property<decimal>("Price");

                    b.Property<string>("Status");

                    b.Property<string>("Time");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("MovieRoomId");

                    b.ToTable("MovieTimeSetting");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.PaymentMethod", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Name");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethod");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.Photo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<long?>("ObjectId");

                    b.Property<string>("Status");

                    b.Property<string>("Type");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.Property<string>("url");

                    b.HasKey("Id");

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.Promotion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("AvailableFrom");

                    b.Property<DateTime?>("AvailableTo");

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<string>("DiscountUnit");

                    b.Property<decimal>("DiscountValue");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("Promotion");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.Rate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<long>("CustomerId");

                    b.Property<long>("MovieId");

                    b.Property<string>("Note");

                    b.Property<decimal>("Rating");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("Rate");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.Room", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Name");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Email");

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("Mobile");

                    b.Property<string>("Name");

                    b.Property<string>("PasswordHash");

                    b.Property<long?>("PhotoId");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.Video", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<long?>("ObjectId");

                    b.Property<string>("Status");

                    b.Property<string>("Type");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.Property<string>("url");

                    b.HasKey("Id");

                    b.ToTable("Video");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.ComboFood", b =>
                {
                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.Food", "Foods")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.CustomerPromotion", b =>
                {
                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.Promotion", "Promotion")
                        .WithMany()
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.Invoice", b =>
                {
                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.Promotion", "Promotion")
                        .WithMany()
                        .HasForeignKey("PromotionId");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.InvoiceDetails", b =>
                {
                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.Invoice", "Invoice")
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("InvoiceId");

                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId");

                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.Promotion", "Promotion")
                        .WithMany()
                        .HasForeignKey("PromotionId");
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.InvoicePayment", b =>
                {
                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.Invoice")
                        .WithMany("InvoicePayment")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.PaymentMethod", "InvoiceMethod")
                        .WithMany()
                        .HasForeignKey("InvoiceMethodId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.MovieActor", b =>
                {
                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.Actor", "Actor")
                        .WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.Movie", "Movie")
                        .WithMany("MovieActors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.MovieCategories", b =>
                {
                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.Movie", "Movie")
                        .WithMany("MovieCategories")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.MovieCinema", b =>
                {
                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.Cinema", "Cinema")
                        .WithMany()
                        .HasForeignKey("CinemaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.MovieDateSetting", "MovieDateSetting")
                        .WithMany("MovieCinemas")
                        .HasForeignKey("MovieDateSettingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.MovieDateSetting", b =>
                {
                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.Movie", "Movie")
                        .WithMany("MovieDateSettings")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.MovieDirector", b =>
                {
                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.Director", "Director")
                        .WithMany()
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.Movie", "Movie")
                        .WithMany("MovieDirectors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.MovieRoom", b =>
                {
                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.MovieCinema", "MovieCinema")
                        .WithMany("MovieRooms")
                        .HasForeignKey("MovieCinemaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.MovieTimeSetting", b =>
                {
                    b.HasOne("BOOKING_MOVIE_ENTITY.Entities.MovieRoom", "MovieRoom")
                        .WithMany("MovieTimeSettings")
                        .HasForeignKey("MovieRoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
