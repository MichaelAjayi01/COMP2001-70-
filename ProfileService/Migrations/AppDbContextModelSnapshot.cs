﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProfileService.Models;

#nullable disable

namespace ProfileService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProfileService.Models.AuditLog", b =>
                {
                    b.Property<int>("Audit_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Audit_ID"));

                    b.Property<DateTime>("Operation_DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Operation_Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Operation_Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("User_ID")
                        .HasColumnType("int");

                    b.HasKey("Audit_ID");

                    b.HasIndex("User_ID");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("ProfileService.Models.CompletedTrail", b =>
                {
                    b.Property<int>("User_Trail_ID")
                        .HasColumnType("int");

                    b.Property<int>("Completed_Trail_Count")
                        .HasColumnType("int");

                    b.HasKey("User_Trail_ID");

                    b.ToTable("CompletedTrails");
                });

            modelBuilder.Entity("ProfileService.Models.Profile", b =>
                {
                    b.Property<int>("User_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("User_ID"));

                    b.Property<string>("About")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Calorie_Counter_Info")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("First_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<string>("Last_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Profile_Picture")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Set_Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Units")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("User_ID");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("ProfileService.Models.Trail", b =>
                {
                    b.Property<int>("Trail_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Trail_ID"));

                    b.Property<string>("Trail_Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Trail_ID");

                    b.ToTable("Trails");
                });

            modelBuilder.Entity("ProfileService.Models.UserProfileCompletedTrail", b =>
                {
                    b.Property<int>("User_Trail_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("User_Trail_ID"));

                    b.Property<int>("Trail_ID")
                        .HasColumnType("int");

                    b.Property<int>("User_ID")
                        .HasColumnType("int");

                    b.HasKey("User_Trail_ID");

                    b.HasIndex("Trail_ID");

                    b.HasIndex("User_ID");

                    b.ToTable("UserProfileCompletedTrails");
                });

            modelBuilder.Entity("ProfileService.Models.AuditLog", b =>
                {
                    b.HasOne("ProfileService.Models.Profile", "User")
                        .WithMany("AuditLogs")
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProfileService.Models.CompletedTrail", b =>
                {
                    b.HasOne("ProfileService.Models.UserProfileCompletedTrail", "UserProfileCompletedTrail")
                        .WithOne("CompletedTrail")
                        .HasForeignKey("ProfileService.Models.CompletedTrail", "User_Trail_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserProfileCompletedTrail");
                });

            modelBuilder.Entity("ProfileService.Models.UserProfileCompletedTrail", b =>
                {
                    b.HasOne("ProfileService.Models.Trail", "Trail")
                        .WithMany("CompletedTrails")
                        .HasForeignKey("Trail_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfileService.Models.Profile", "User")
                        .WithMany("CompletedTrails")
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trail");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProfileService.Models.Profile", b =>
                {
                    b.Navigation("AuditLogs");

                    b.Navigation("CompletedTrails");
                });

            modelBuilder.Entity("ProfileService.Models.Trail", b =>
                {
                    b.Navigation("CompletedTrails");
                });

            modelBuilder.Entity("ProfileService.Models.UserProfileCompletedTrail", b =>
                {
                    b.Navigation("CompletedTrail")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
