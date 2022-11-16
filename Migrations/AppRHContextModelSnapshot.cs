﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppRH.Migrations
{
    [DbContext(typeof(AppRHContext))]
    partial class AppRHContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AppRH.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerID"), 1L, 1);

                    b.Property<DateTime>("CustomerBirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerDNI")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("CustomerSurname")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("AppRH.Models.House", b =>
                {
                    b.Property<int>("HouseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HouseID"), 1L, 1);

                    b.Property<bool>("EstaAlquilada")
                        .HasColumnType("bit");

                    b.Property<string>("HouseAddress")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("HouseName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("OwnerHouse")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<byte[]>("PhotoHouse")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("HouseID");

                    b.ToTable("House");
                });

            modelBuilder.Entity("AppRH.Models.Rental", b =>
                {
                    b.Property<int>("RentalID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RentalID"), 1L, 1);

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("RentalDate")
                        .HasColumnType("datetime2");

                    b.HasKey("RentalID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Rental");
                });

            modelBuilder.Entity("AppRH.Models.RentalDetail", b =>
                {
                    b.Property<int>("RentalDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RentalDetailID"), 1L, 1);

                    b.Property<int>("HouseID")
                        .HasColumnType("int");

                    b.Property<string>("HouseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RentalID")
                        .HasColumnType("int");

                    b.HasKey("RentalDetailID");

                    b.HasIndex("HouseID");

                    b.HasIndex("RentalID");

                    b.ToTable("RentalDetail");
                });

            modelBuilder.Entity("AppRH.Models.RentalDetailTemp", b =>
                {
                    b.Property<int>("RentalDetailTempID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RentalDetailTempID"), 1L, 1);

                    b.Property<int>("HouseID")
                        .HasColumnType("int");

                    b.Property<string>("HouseName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RentalDetailTempID");

                    b.ToTable("RentalDetailTemp");
                });

            modelBuilder.Entity("AppRH.Models.Return", b =>
                {
                    b.Property<int>("ReturnID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReturnID"), 1L, 1);

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ReturnID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Return");
                });

            modelBuilder.Entity("AppRH.Models.ReturnDetail", b =>
                {
                    b.Property<int>("ReturnDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReturnDetailID"), 1L, 1);

                    b.Property<int>("HouseID")
                        .HasColumnType("int");

                    b.Property<string>("HouseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReturnID")
                        .HasColumnType("int");

                    b.HasKey("ReturnDetailID");

                    b.HasIndex("HouseID");

                    b.HasIndex("ReturnID");

                    b.ToTable("ReturnDetail");
                });

            modelBuilder.Entity("AppRH.Models.ReturnDetailTemp", b =>
                {
                    b.Property<int>("ReturnDetailTempID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReturnDetailTempID"), 1L, 1);

                    b.Property<int>("HouseID")
                        .HasColumnType("int");

                    b.Property<string>("HouseName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReturnDetailTempID");

                    b.ToTable("ReturnDetailTemp");
                });

            modelBuilder.Entity("AppRH.Models.Rental", b =>
                {
                    b.HasOne("AppRH.Models.Customer", "Customer")
                        .WithMany("Rentals")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("AppRH.Models.RentalDetail", b =>
                {
                    b.HasOne("AppRH.Models.House", "House")
                        .WithMany("RentalDetails")
                        .HasForeignKey("HouseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppRH.Models.Rental", "Rental")
                        .WithMany("RentalDetails")
                        .HasForeignKey("RentalID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("House");

                    b.Navigation("Rental");
                });

            modelBuilder.Entity("AppRH.Models.Return", b =>
                {
                    b.HasOne("AppRH.Models.Customer", "Customer")
                        .WithMany("Returns")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("AppRH.Models.ReturnDetail", b =>
                {
                    b.HasOne("AppRH.Models.House", "House")
                        .WithMany("ReturnDetails")
                        .HasForeignKey("HouseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppRH.Models.Return", "Return")
                        .WithMany("ReturnDetails")
                        .HasForeignKey("ReturnID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("House");

                    b.Navigation("Return");
                });

            modelBuilder.Entity("AppRH.Models.Customer", b =>
                {
                    b.Navigation("Rentals");

                    b.Navigation("Returns");
                });

            modelBuilder.Entity("AppRH.Models.House", b =>
                {
                    b.Navigation("RentalDetails");

                    b.Navigation("ReturnDetails");
                });

            modelBuilder.Entity("AppRH.Models.Rental", b =>
                {
                    b.Navigation("RentalDetails");
                });

            modelBuilder.Entity("AppRH.Models.Return", b =>
                {
                    b.Navigation("ReturnDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
