﻿// <auto-generated />
using System;
using DbContextClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DbContextClasses.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20231213204309_SecondMig")]
    partial class SecondMig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DbContextClasses.Course", b =>
                {
                    b.Property<Guid>("Course_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Course_Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Course_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Course_ID");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("DbContextClasses.GroupStudent", b =>
                {
                    b.Property<Guid>("Group_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Group_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Group_Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("DbContextClasses.Student", b =>
                {
                    b.Property<Guid>("Student_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("First_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Last_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Student_Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("DbContextClasses.Teacher", b =>
                {
                    b.Property<Guid>("Teacher_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Teacher_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Teacher_Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Teacher_Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("DbContextClasses.GroupStudent", b =>
                {
                    b.HasOne("DbContextClasses.Course", "Course")
                        .WithMany("Groups")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DbContextClasses.Teacher", "Teacher")
                        .WithMany("Groups")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("DbContextClasses.Student", b =>
                {
                    b.HasOne("DbContextClasses.GroupStudent", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("DbContextClasses.Course", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("DbContextClasses.Teacher", b =>
                {
                    b.Navigation("Groups");
                });
#pragma warning restore 612, 618
        }
    }
}
