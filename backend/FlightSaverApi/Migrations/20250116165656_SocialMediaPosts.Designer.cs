﻿// <auto-generated />
using System;
using FlightSaverApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlightSaverApi.Migrations
{
    [DbContext(typeof(FlightSaverContext))]
<<<<<<<< HEAD:backend/FlightSaverApi/Migrations/20250116165656_SocialMediaPosts.Designer.cs
    [Migration("20250116165656_SocialMediaPosts")]
    partial class SocialMediaPosts
========
    [Migration("20250115191052_NullColumns")]
    partial class NullColumns
>>>>>>>> main:backend/FlightSaverApi/Migrations/20250115191052_NullColumns.Designer.cs
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FlightSaverApi.Models.Aircraft", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AircraftUrl")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("AirlineId")
                        .HasColumnType("integer");

                    b.Property<string>("IataCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<string>("IcaoCode")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("character varying(4)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("RegNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.HasIndex("AirlineId");

                    b.HasIndex("RegNumber")
                        .IsUnique();

                    b.ToTable("Aircrafts");
                });

            modelBuilder.Entity("FlightSaverApi.Models.Airline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("IataCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<string>("IcaoCode")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("character varying(4)");

                    b.Property<string>("LogoUrl")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("IataCode")
                        .IsUnique();

                    b.HasIndex("IcaoCode")
                        .IsUnique();

                    b.ToTable("Airlines");
                });

            modelBuilder.Entity("FlightSaverApi.Models.Airport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("IataCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<string>("IcaoCode")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("character varying(4)");

                    b.Property<double>("Latitude")
                        .HasColumnType("decimal(9, 6)");

                    b.Property<double>("Longitude")
                        .HasColumnType("decimal(9, 6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("IataCode")
                        .IsUnique();

                    b.HasIndex("IcaoCode")
                        .IsUnique();

                    b.ToTable("Airports");
                });

<<<<<<<< HEAD:backend/FlightSaverApi/Migrations/20250116165656_SocialMediaPosts.Designer.cs
            modelBuilder.Entity("FlightSaverApi.Models.Comment", b =>
========
            modelBuilder.Entity("FlightSaverApi.Models.Flight", b =>
>>>>>>>> main:backend/FlightSaverApi/Migrations/20250115191052_NullColumns.Designer.cs
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

<<<<<<<< HEAD:backend/FlightSaverApi/Migrations/20250116165656_SocialMediaPosts.Designer.cs
                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int>("LikesCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("PostedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SocialPostId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SocialPostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("FlightSaverApi.Models.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AircraftId")
                        .HasColumnType("integer");

========
                    b.Property<int?>("AircraftId")
                        .HasColumnType("integer");

>>>>>>>> main:backend/FlightSaverApi/Migrations/20250115191052_NullColumns.Designer.cs
                    b.Property<int?>("AirlineId")
                        .HasColumnType("integer");

                    b.Property<int>("ArrivalAirportId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ClassType")
                        .HasColumnType("integer");

                    b.Property<int>("DepartureAirportId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FlightNumber")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("Reason")
                        .HasColumnType("integer");

                    b.Property<string>("SeatNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SeatType")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AircraftId");

                    b.HasIndex("AirlineId");

                    b.HasIndex("ArrivalAirportId");

                    b.HasIndex("DepartureAirportId");

                    b.HasIndex("UserId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("FlightSaverApi.Models.Review.AircraftReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AircraftId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("FlightId")
                        .HasColumnType("integer");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AircraftId");

                    b.HasIndex("FlightId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("AircraftReviews");
                });

            modelBuilder.Entity("FlightSaverApi.Models.Review.AirlineReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AirlineId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("FlightId")
                        .HasColumnType("integer");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AirlineId");

                    b.HasIndex("FlightId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("AirlineReviews");
                });

            modelBuilder.Entity("FlightSaverApi.Models.Review.AirportReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AirportId")
                        .HasColumnType("integer");

                    b.Property<int>("AirportReviewType")
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("FlightId")
                        .HasColumnType("integer");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AirportId");

                    b.HasIndex("FlightId");

                    b.HasIndex("UserId");

                    b.ToTable("AirportReviews");
                });

<<<<<<<< HEAD:backend/FlightSaverApi/Migrations/20250116165656_SocialMediaPosts.Designer.cs
            modelBuilder.Entity("FlightSaverApi.Models.SocialPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CommentsCount")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("LikesCount")
                        .HasColumnType("integer");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<DateTime>("PostedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SocialPosts");
                });

========
>>>>>>>> main:backend/FlightSaverApi/Migrations/20250115191052_NullColumns.Designer.cs
            modelBuilder.Entity("FlightSaverApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("ProfilePictureUrl")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FlightSaverApi.Models.Aircraft", b =>
                {
                    b.HasOne("FlightSaverApi.Models.Airline", "Airline")
                        .WithMany("Aircrafts")
                        .HasForeignKey("AirlineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airline");
                });

<<<<<<<< HEAD:backend/FlightSaverApi/Migrations/20250116165656_SocialMediaPosts.Designer.cs
            modelBuilder.Entity("FlightSaverApi.Models.Comment", b =>
                {
                    b.HasOne("FlightSaverApi.Models.SocialPost", "SocialPost")
                        .WithMany("Comments")
                        .HasForeignKey("SocialPostId");

                    b.HasOne("FlightSaverApi.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SocialPost");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlightSaverApi.Models.Flight", b =>
                {
                    b.HasOne("FlightSaverApi.Models.Aircraft", "Aircraft")
                        .WithMany("Flights")
                        .HasForeignKey("AircraftId");

                    b.HasOne("FlightSaverApi.Models.Airline", "Airline")
                        .WithMany("Flights")
                        .HasForeignKey("AirlineId");

========
            modelBuilder.Entity("FlightSaverApi.Models.Flight", b =>
                {
                    b.HasOne("FlightSaverApi.Models.Aircraft", "Aircraft")
                        .WithMany("Flights")
                        .HasForeignKey("AircraftId");

                    b.HasOne("FlightSaverApi.Models.Airline", "Airline")
                        .WithMany("Flights")
                        .HasForeignKey("AirlineId");

>>>>>>>> main:backend/FlightSaverApi/Migrations/20250115191052_NullColumns.Designer.cs
                    b.HasOne("FlightSaverApi.Models.Airport", "ArrivalAirport")
                        .WithMany("ArrivingFlights")
                        .HasForeignKey("ArrivalAirportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightSaverApi.Models.Airport", "DepartureAirport")
                        .WithMany("DepartingFlights")
                        .HasForeignKey("DepartureAirportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightSaverApi.Models.User", "User")
                        .WithMany("Flights")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aircraft");

                    b.Navigation("Airline");

                    b.Navigation("ArrivalAirport");

                    b.Navigation("DepartureAirport");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlightSaverApi.Models.Review.AircraftReview", b =>
                {
                    b.HasOne("FlightSaverApi.Models.Aircraft", "Aircraft")
                        .WithMany("AircraftReviews")
                        .HasForeignKey("AircraftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightSaverApi.Models.Flight", "Flight")
                        .WithOne("AircraftReview")
<<<<<<<< HEAD:backend/FlightSaverApi/Migrations/20250116165656_SocialMediaPosts.Designer.cs
                        .HasForeignKey("FlightSaverApi.Models.Review.AircraftReview", "FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
========
                        .HasForeignKey("FlightSaverApi.Models.Review.AircraftReview", "FlightId");
>>>>>>>> main:backend/FlightSaverApi/Migrations/20250115191052_NullColumns.Designer.cs

                    b.HasOne("FlightSaverApi.Models.User", "User")
                        .WithMany("AircraftReviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aircraft");

                    b.Navigation("Flight");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlightSaverApi.Models.Review.AirlineReview", b =>
                {
                    b.HasOne("FlightSaverApi.Models.Airline", "Airline")
                        .WithMany("AirlineReviews")
                        .HasForeignKey("AirlineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightSaverApi.Models.Flight", "Flight")
                        .WithOne("AirlineReview")
<<<<<<<< HEAD:backend/FlightSaverApi/Migrations/20250116165656_SocialMediaPosts.Designer.cs
                        .HasForeignKey("FlightSaverApi.Models.Review.AirlineReview", "FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
========
                        .HasForeignKey("FlightSaverApi.Models.Review.AirlineReview", "FlightId");
>>>>>>>> main:backend/FlightSaverApi/Migrations/20250115191052_NullColumns.Designer.cs

                    b.HasOne("FlightSaverApi.Models.User", "User")
                        .WithMany("AirlineReviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airline");

                    b.Navigation("Flight");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlightSaverApi.Models.Review.AirportReview", b =>
                {
                    b.HasOne("FlightSaverApi.Models.Airport", "Airport")
                        .WithMany("AirportReviews")
                        .HasForeignKey("AirportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightSaverApi.Models.Flight", "Flight")
                        .WithMany("AirportReviews")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightSaverApi.Models.User", "User")
                        .WithMany("AirportReviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airport");

                    b.Navigation("Flight");

                    b.Navigation("User");
                });

<<<<<<<< HEAD:backend/FlightSaverApi/Migrations/20250116165656_SocialMediaPosts.Designer.cs
            modelBuilder.Entity("FlightSaverApi.Models.SocialPost", b =>
                {
                    b.HasOne("FlightSaverApi.Models.User", "User")
                        .WithMany("SocialPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

========
>>>>>>>> main:backend/FlightSaverApi/Migrations/20250115191052_NullColumns.Designer.cs
            modelBuilder.Entity("FlightSaverApi.Models.Aircraft", b =>
                {
                    b.Navigation("AircraftReviews");

                    b.Navigation("Flights");
                });

            modelBuilder.Entity("FlightSaverApi.Models.Airline", b =>
                {
                    b.Navigation("Aircrafts");

                    b.Navigation("AirlineReviews");

                    b.Navigation("Flights");
                });

            modelBuilder.Entity("FlightSaverApi.Models.Airport", b =>
                {
                    b.Navigation("AirportReviews");

                    b.Navigation("ArrivingFlights");

                    b.Navigation("DepartingFlights");
                });

            modelBuilder.Entity("FlightSaverApi.Models.Flight", b =>
                {
                    b.Navigation("AircraftReview");

                    b.Navigation("AirlineReview");

                    b.Navigation("AirportReviews");
                });

<<<<<<<< HEAD:backend/FlightSaverApi/Migrations/20250116165656_SocialMediaPosts.Designer.cs
            modelBuilder.Entity("FlightSaverApi.Models.SocialPost", b =>
                {
                    b.Navigation("Comments");
                });

========
>>>>>>>> main:backend/FlightSaverApi/Migrations/20250115191052_NullColumns.Designer.cs
            modelBuilder.Entity("FlightSaverApi.Models.User", b =>
                {
                    b.Navigation("AircraftReviews");

                    b.Navigation("AirlineReviews");

                    b.Navigation("AirportReviews");

                    b.Navigation("Comments");

                    b.Navigation("Flights");

                    b.Navigation("SocialPosts");
                });
#pragma warning restore 612, 618
        }
    }
}
