﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace _45_Complex_Relational_Queries.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240201122459_mig-1")]
    partial class mig1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("PersonId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            Description = "...",
                            PersonId = 1
                        },
                        new
                        {
                            OrderId = 2,
                            Description = "...",
                            PersonId = 2
                        },
                        new
                        {
                            OrderId = 3,
                            Description = "...",
                            PersonId = 4
                        },
                        new
                        {
                            OrderId = 4,
                            Description = "...",
                            PersonId = 5
                        },
                        new
                        {
                            OrderId = 5,
                            Description = "...",
                            PersonId = 1
                        },
                        new
                        {
                            OrderId = 6,
                            Description = "...",
                            PersonId = 6
                        },
                        new
                        {
                            OrderId = 7,
                            Description = "...",
                            PersonId = 7
                        },
                        new
                        {
                            OrderId = 8,
                            Description = "...",
                            PersonId = 1
                        },
                        new
                        {
                            OrderId = 9,
                            Description = "...",
                            PersonId = 8
                        },
                        new
                        {
                            OrderId = 10,
                            Description = "...",
                            PersonId = 9
                        },
                        new
                        {
                            OrderId = 11,
                            Description = "...",
                            PersonId = 1
                        },
                        new
                        {
                            OrderId = 12,
                            Description = "...",
                            PersonId = 2
                        },
                        new
                        {
                            OrderId = 13,
                            Description = "...",
                            PersonId = 2
                        },
                        new
                        {
                            OrderId = 14,
                            Description = "...",
                            PersonId = 3
                        },
                        new
                        {
                            OrderId = 15,
                            Description = "...",
                            PersonId = 1
                        },
                        new
                        {
                            OrderId = 16,
                            Description = "...",
                            PersonId = 4
                        },
                        new
                        {
                            OrderId = 17,
                            Description = "...",
                            PersonId = 1
                        },
                        new
                        {
                            OrderId = 18,
                            Description = "...",
                            PersonId = 1
                        },
                        new
                        {
                            OrderId = 19,
                            Description = "...",
                            PersonId = 5
                        },
                        new
                        {
                            OrderId = 20,
                            Description = "...",
                            PersonId = 6
                        },
                        new
                        {
                            OrderId = 21,
                            Description = "...",
                            PersonId = 1
                        },
                        new
                        {
                            OrderId = 22,
                            Description = "...",
                            PersonId = 7
                        },
                        new
                        {
                            OrderId = 23,
                            Description = "...",
                            PersonId = 7
                        },
                        new
                        {
                            OrderId = 24,
                            Description = "...",
                            PersonId = 8
                        },
                        new
                        {
                            OrderId = 25,
                            Description = "...",
                            PersonId = 1
                        },
                        new
                        {
                            OrderId = 26,
                            Description = "...",
                            PersonId = 1
                        },
                        new
                        {
                            OrderId = 27,
                            Description = "...",
                            PersonId = 9
                        },
                        new
                        {
                            OrderId = 28,
                            Description = "...",
                            PersonId = 9
                        },
                        new
                        {
                            OrderId = 29,
                            Description = "...",
                            PersonId = 9
                        },
                        new
                        {
                            OrderId = 30,
                            Description = "...",
                            PersonId = 2
                        },
                        new
                        {
                            OrderId = 31,
                            Description = "...",
                            PersonId = 3
                        },
                        new
                        {
                            OrderId = 32,
                            Description = "...",
                            PersonId = 1
                        },
                        new
                        {
                            OrderId = 33,
                            Description = "...",
                            PersonId = 1
                        },
                        new
                        {
                            OrderId = 34,
                            Description = "...",
                            PersonId = 1
                        },
                        new
                        {
                            OrderId = 35,
                            Description = "...",
                            PersonId = 5
                        },
                        new
                        {
                            OrderId = 36,
                            Description = "...",
                            PersonId = 1
                        },
                        new
                        {
                            OrderId = 37,
                            Description = "...",
                            PersonId = 5
                        },
                        new
                        {
                            OrderId = 38,
                            Description = "...",
                            PersonId = 1
                        },
                        new
                        {
                            OrderId = 39,
                            Description = "...",
                            PersonId = 1
                        },
                        new
                        {
                            OrderId = 40,
                            Description = "...",
                            PersonId = 1
                        });
                });

            modelBuilder.Entity("Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"), 1L, 1);

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");

                    b.HasData(
                        new
                        {
                            PersonId = 1,
                            Gender = 1,
                            Name = "Ayşe"
                        },
                        new
                        {
                            PersonId = 2,
                            Gender = 0,
                            Name = "Hilmi"
                        },
                        new
                        {
                            PersonId = 3,
                            Gender = 1,
                            Name = "Raziye"
                        },
                        new
                        {
                            PersonId = 4,
                            Gender = 0,
                            Name = "Süleyman"
                        },
                        new
                        {
                            PersonId = 5,
                            Gender = 1,
                            Name = "Fadime"
                        },
                        new
                        {
                            PersonId = 6,
                            Gender = 0,
                            Name = "Şuayip"
                        },
                        new
                        {
                            PersonId = 7,
                            Gender = 1,
                            Name = "Lale"
                        },
                        new
                        {
                            PersonId = 8,
                            Gender = 1,
                            Name = "Jale"
                        },
                        new
                        {
                            PersonId = 9,
                            Gender = 0,
                            Name = "Rıfkı"
                        },
                        new
                        {
                            PersonId = 10,
                            Gender = 1,
                            Name = "Muaviye"
                        });
                });

            modelBuilder.Entity("Photo", b =>
                {
                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonId");

                    b.ToTable("Photos");

                    b.HasData(
                        new
                        {
                            PersonId = 1,
                            Url = "https://randomuser.me/api/portraits/women/1.jpg"
                        },
                        new
                        {
                            PersonId = 2,
                            Url = "https://randomuser.me/api/portraits/men/2.jpg"
                        },
                        new
                        {
                            PersonId = 3,
                            Url = "https://randomuser.me/api/portraits/women/3.jpg"
                        },
                        new
                        {
                            PersonId = 4,
                            Url = "https://randomuser.me/api/portraits/men/4.jpg"
                        },
                        new
                        {
                            PersonId = 5,
                            Url = "https://randomuser.me/api/portraits/women/5.jpg"
                        },
                        new
                        {
                            PersonId = 6,
                            Url = "https://randomuser.me/api/portraits/men/6.jpg"
                        },
                        new
                        {
                            PersonId = 7,
                            Url = "https://randomuser.me/api/portraits/women/7.jpg"
                        },
                        new
                        {
                            PersonId = 8,
                            Url = "https://randomuser.me/api/portraits/women/8.jpg"
                        },
                        new
                        {
                            PersonId = 9,
                            Url = "https://randomuser.me/api/portraits/men/9.jpg"
                        },
                        new
                        {
                            PersonId = 10,
                            Url = "https://randomuser.me/api/portraits/women/10.jpg"
                        });
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.HasOne("Person", "Person")
                        .WithMany("Orders")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Photo", b =>
                {
                    b.HasOne("Person", "Person")
                        .WithOne("Photo")
                        .HasForeignKey("Photo", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Person", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Photo")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
