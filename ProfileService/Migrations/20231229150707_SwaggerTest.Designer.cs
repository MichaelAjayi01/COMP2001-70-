﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProfileService.Models;

#nullable disable

namespace ProfileService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231229150707_SwaggerTest")]
    partial class SwaggerTest
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int?>("ProfileUser_ID")
                        .HasColumnType("int");

                    b.HasKey("Audit_ID");

                    b.HasIndex("ProfileUser_ID");

                    b.ToTable("CW2_Audit_Log");
                });

            modelBuilder.Entity("ProfileService.Models.CompletedTrail", b =>
                {
                    b.Property<int>("Completed_Trail_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Completed_Trail_ID"));

                    b.Property<int>("Completed_Trail_Count")
                        .HasColumnType("int");

                    b.HasKey("Completed_Trail_ID");

                    b.ToTable("CW2_COMPLETED_TRAILS");
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

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

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

                    b.ToTable("CW2_USER_PROFILE");
                });

            modelBuilder.Entity("ProfileService.Models.Trail", b =>
                {
                    b.Property<int>("Trail_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Trail_ID"));

                    b.Property<string>("List_of_Trails")
                        .IsRequired()
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Trail_Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Trail_ID");

                    b.ToTable("CW2_Trails");
                });

            modelBuilder.Entity("ProfileService.Models.UserProfileCompletedTrail", b =>
                {
                    b.Property<int>("User_ID")
                        .HasColumnType("int");

                    b.Property<int>("Trail_ID")
                        .HasColumnType("int");

                    b.Property<int>("CompletedTrail_ID")
                        .HasColumnType("int");

                    b.Property<int>("User_Trail_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("User_Trail_ID"));

                    b.HasKey("User_ID", "Trail_ID", "CompletedTrail_ID");

                    b.HasIndex("CompletedTrail_ID");

                    b.HasIndex("Trail_ID");

                    b.ToTable("CW2_UserProfile_CompletedTrails_JT");
                });

            modelBuilder.Entity("ProfileService.Models.AuditLog", b =>
                {
                    b.HasOne("ProfileService.Models.Profile", null)
                        .WithMany("AuditLogs")
                        .HasForeignKey("ProfileUser_ID");
                });

            modelBuilder.Entity("ProfileService.Models.UserProfileCompletedTrail", b =>
                {
                    b.HasOne("ProfileService.Models.CompletedTrail", "CompletedTrail")
                        .WithMany("UserProfileCompletedTrails")
                        .HasForeignKey("CompletedTrail_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

                    b.Navigation("CompletedTrail");

                    b.Navigation("Trail");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProfileService.Models.CompletedTrail", b =>
                {
                    b.Navigation("UserProfileCompletedTrails");
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
#pragma warning restore 612, 618
        }
    }
}
