﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieApplication;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MovieApplication.Migrations
{
    [DbContext(typeof(MovieDbContext))]
    [Migration("20250306165320_InitialDataWithHasData")]
    partial class InitialDataWithHasData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("app")
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MovieApplication.Models.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTimeOffset>("LastModifed")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Rating")
                        .HasColumnType("double precision");

                    b.Property<DateTimeOffset>("ReleseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Title");

                    b.ToTable("Movies", "app");

                    b.HasData(
                        new
                        {
                            Id = new Guid("01c681ed-278c-444c-b53c-917cefa3b02c"),
                            Created = new DateTimeOffset(new DateTime(2025, 3, 6, 16, 53, 19, 566, DateTimeKind.Unspecified).AddTicks(3873), new TimeSpan(0, 0, 0, 0, 0)),
                            Genre = "Fantasy",
                            LastModifed = new DateTimeOffset(new DateTime(2025, 3, 6, 16, 53, 19, 566, DateTimeKind.Unspecified).AddTicks(3877), new TimeSpan(0, 0, 0, 0, 0)),
                            Rating = 8.0,
                            ReleseDate = new DateTimeOffset(new DateTime(2025, 3, 6, 16, 53, 19, 566, DateTimeKind.Unspecified).AddTicks(5564), new TimeSpan(0, 0, 0, 0, 0)),
                            Title = "Sonic the hedgeh 3"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
