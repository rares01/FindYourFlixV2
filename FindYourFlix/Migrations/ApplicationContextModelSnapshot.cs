﻿// <auto-generated />
using System;
using FindYourFlix;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FindYourFlix.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("FindYourFlix.Data.Entities.Genre", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("MovieId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("FindYourFlix.Data.Entities.LikedMovie", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("MovieId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("UserId");

                    b.ToTable("LikedMovies");
                });

            modelBuilder.Entity("FindYourFlix.Data.Entities.Movie", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("FindYourFlix.Data.Entities.Tag", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("InsertedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("MovieId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("UserId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("FindYourFlix.Data.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("LastName")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FindYourFlix.Data.Entities.Genre", b =>
                {
                    b.HasOne("FindYourFlix.Data.Entities.Movie", "Movie")
                        .WithMany("Genres")
                        .HasForeignKey("MovieId");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("FindYourFlix.Data.Entities.LikedMovie", b =>
                {
                    b.HasOne("FindYourFlix.Data.Entities.Movie", "Movie")
                        .WithMany("LikedMovies")
                        .HasForeignKey("MovieId");

                    b.HasOne("FindYourFlix.Data.Entities.User", "User")
                        .WithMany("LikedMovies")
                        .HasForeignKey("UserId");

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FindYourFlix.Data.Entities.Tag", b =>
                {
                    b.HasOne("FindYourFlix.Data.Entities.Movie", "Movie")
                        .WithMany("Tags")
                        .HasForeignKey("MovieId");

                    b.HasOne("FindYourFlix.Data.Entities.User", "User")
                        .WithMany("Tags")
                        .HasForeignKey("UserId");

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FindYourFlix.Data.Entities.Movie", b =>
                {
                    b.Navigation("Genres");

                    b.Navigation("LikedMovies");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("FindYourFlix.Data.Entities.User", b =>
                {
                    b.Navigation("LikedMovies");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}