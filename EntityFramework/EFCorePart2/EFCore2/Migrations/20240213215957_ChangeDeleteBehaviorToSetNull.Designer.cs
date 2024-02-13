﻿// <auto-generated />
using System;
using EFCore2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCore2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240213215957_ChangeDeleteBehaviorToSetNull")]
    partial class ChangeDeleteBehaviorToSetNull
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EFCore2.Dtos.BookDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BooksDto");
                });

            modelBuilder.Entity("EFCore2.Models.ApplicationDbContext+Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NationalityId")
                        .HasColumnType("int")
                        .HasColumnName("NationalityId");

                    b.Property<int?>("NationalityId1")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NationalityId");

                    b.HasIndex("NationalityId1")
                        .IsUnique()
                        .HasFilter("[NationalityId1] IS NOT NULL");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("EFCore2.Models.ApplicationDbContext+Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("EFCore2.Models.ApplicationDbContext+Nationality", b =>
                {
                    b.Property<int>("NationalityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NationalityId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NationalityId");

                    b.ToTable("Nationalities");
                });

            modelBuilder.Entity("EFCore2.Models.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BlogId"));

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BlogId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("EFCore2.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"));

                    b.Property<int?>("BlogId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostId");

                    b.HasIndex(new[] { "BlogId" }, "IX_Posts_BlogId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("EFCore2.Models.ApplicationDbContext+Author", b =>
                {
                    b.HasOne("EFCore2.Models.ApplicationDbContext+Nationality", "Nationality")
                        .WithMany("Authors")
                        .HasForeignKey("NationalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFCore2.Models.ApplicationDbContext+Nationality", null)
                        .WithOne("Author")
                        .HasForeignKey("EFCore2.Models.ApplicationDbContext+Author", "NationalityId1");

                    b.Navigation("Nationality");
                });

            modelBuilder.Entity("EFCore2.Models.ApplicationDbContext+Book", b =>
                {
                    b.HasOne("EFCore2.Models.ApplicationDbContext+Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("EFCore2.Models.Post", b =>
                {
                    b.HasOne("EFCore2.Models.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("EFCore2.Models.ApplicationDbContext+Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("EFCore2.Models.ApplicationDbContext+Nationality", b =>
                {
                    b.Navigation("Author");

                    b.Navigation("Authors");
                });

            modelBuilder.Entity("EFCore2.Models.Blog", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}