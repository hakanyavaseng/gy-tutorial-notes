﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace _44_Lazy_Loading.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240201104326_mig-1")]
    partial class mig1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegionId")
                        .HasColumnType("int");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Gençay",
                            RegionId = 1,
                            Salary = 1500,
                            Surname = "Yıldız"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Mahmut",
                            RegionId = 2,
                            Salary = 1500,
                            Surname = "Yıldız"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Rıfkı",
                            RegionId = 1,
                            Salary = 1500,
                            Surname = "Yıldız"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Cüneyt",
                            RegionId = 2,
                            Salary = 1500,
                            Surname = "Yıldız"
                        });
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EmployeeId = 1,
                            OrderDate = new DateTime(2024, 2, 1, 13, 43, 26, 709, DateTimeKind.Local).AddTicks(393)
                        },
                        new
                        {
                            Id = 2,
                            EmployeeId = 1,
                            OrderDate = new DateTime(2024, 2, 1, 13, 43, 26, 709, DateTimeKind.Local).AddTicks(403)
                        },
                        new
                        {
                            Id = 3,
                            EmployeeId = 2,
                            OrderDate = new DateTime(2024, 2, 1, 13, 43, 26, 709, DateTimeKind.Local).AddTicks(404)
                        },
                        new
                        {
                            Id = 4,
                            EmployeeId = 2,
                            OrderDate = new DateTime(2024, 2, 1, 13, 43, 26, 709, DateTimeKind.Local).AddTicks(404)
                        },
                        new
                        {
                            Id = 5,
                            EmployeeId = 3,
                            OrderDate = new DateTime(2024, 2, 1, 13, 43, 26, 709, DateTimeKind.Local).AddTicks(405)
                        },
                        new
                        {
                            Id = 6,
                            EmployeeId = 3,
                            OrderDate = new DateTime(2024, 2, 1, 13, 43, 26, 709, DateTimeKind.Local).AddTicks(405)
                        },
                        new
                        {
                            Id = 7,
                            EmployeeId = 3,
                            OrderDate = new DateTime(2024, 2, 1, 13, 43, 26, 709, DateTimeKind.Local).AddTicks(406)
                        },
                        new
                        {
                            Id = 8,
                            EmployeeId = 4,
                            OrderDate = new DateTime(2024, 2, 1, 13, 43, 26, 709, DateTimeKind.Local).AddTicks(407)
                        },
                        new
                        {
                            Id = 9,
                            EmployeeId = 4,
                            OrderDate = new DateTime(2024, 2, 1, 13, 43, 26, 709, DateTimeKind.Local).AddTicks(408)
                        },
                        new
                        {
                            Id = 10,
                            EmployeeId = 1,
                            OrderDate = new DateTime(2024, 2, 1, 13, 43, 26, 709, DateTimeKind.Local).AddTicks(408)
                        },
                        new
                        {
                            Id = 11,
                            EmployeeId = 2,
                            OrderDate = new DateTime(2024, 2, 1, 13, 43, 26, 709, DateTimeKind.Local).AddTicks(409)
                        });
                });

            modelBuilder.Entity("Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Ankara"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Yozgat"
                        });
                });

            modelBuilder.Entity("Employee", b =>
                {
                    b.HasOne("Region", "Region")
                        .WithMany("Employees")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.HasOne("Employee", "Employee")
                        .WithMany("Orders")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Employee", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Region", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}