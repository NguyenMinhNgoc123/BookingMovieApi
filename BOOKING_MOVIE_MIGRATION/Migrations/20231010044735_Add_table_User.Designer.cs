﻿// <auto-generated />
using System;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BOOKING_MOVIE_MIGRATION.Migrations
{
    [DbContext(typeof(movie_context))]
    [Migration("20231010044735_Add_table_User")]
    partial class Add_table_User
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BOOKING_MOVIE_ENTITY.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<bool>("IsAdmin");

                    b.Property<bool?>("IsSalonRootUser");

                    b.Property<string>("Mobile");

                    b.Property<string>("Name");

                    b.Property<string>("PasswordHash");

                    b.Property<long?>("PhotoId");

                    b.Property<long?>("SalonBranchCurrentId");

                    b.Property<long?>("SalonId");

                    b.Property<string>("SecurityStamp");

                    b.Property<long?>("StaffRatingSalonBranchCurrentId");

                    b.Property<string>("Status");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
