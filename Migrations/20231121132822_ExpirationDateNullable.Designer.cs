﻿// <auto-generated />
using System;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20231121132822_ExpirationDateNullable")]
    partial class ExpirationDateNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("API.Models.Authentication.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("TokenExpiration")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("API.Models.Items.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Ean")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Items");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Item");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("API.Models.Orders.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("longtext");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Order");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Order");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("API.Models.Suppliers.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("API.Models.Authentication.Customer", b =>
                {
                    b.HasBaseType("API.Models.Authentication.User");

                    b.Property<int?>("PhoneNumber")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("API.Models.Authentication.Employee", b =>
                {
                    b.HasBaseType("API.Models.Authentication.User");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("API.Models.Items.DefaultItem", b =>
                {
                    b.HasBaseType("API.Models.Items.Item");

                    b.HasDiscriminator().HasValue("DefaultItem");
                });

            modelBuilder.Entity("API.Models.Items.Liquor", b =>
                {
                    b.HasBaseType("API.Models.Items.Item");

                    b.Property<string>("LiquorType")
                        .HasColumnType("longtext");

                    b.HasDiscriminator().HasValue("Liquor");
                });

            modelBuilder.Entity("API.Models.Items.Wine", b =>
                {
                    b.HasBaseType("API.Models.Items.Item");

                    b.Property<double?>("AlcoholPercentage")
                        .HasColumnType("double");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<string>("GrapeSort")
                        .HasColumnType("longtext");

                    b.Property<string>("Region")
                        .HasColumnType("longtext");

                    b.Property<string>("SuitableFor")
                        .HasColumnType("longtext");

                    b.Property<string>("TastingNotes")
                        .HasColumnType("longtext");

                    b.Property<double?>("Volume")
                        .HasColumnType("double");

                    b.Property<string>("WineType")
                        .HasColumnType("longtext");

                    b.Property<string>("Winery")
                        .HasColumnType("longtext");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Wine");
                });

            modelBuilder.Entity("API.Models.Orders.CustomerPurchaseOrder", b =>
                {
                    b.HasBaseType("API.Models.Orders.Order");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.HasIndex("CustomerId");

                    b.HasDiscriminator().HasValue("CustomerPurchaseOrder");
                });

            modelBuilder.Entity("API.Models.Authentication.Admin", b =>
                {
                    b.HasBaseType("API.Models.Authentication.Employee");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("API.Models.Items.Item", b =>
                {
                    b.HasOne("API.Models.Orders.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId");

                    b.HasOne("API.Models.Suppliers.Supplier", null)
                        .WithMany("Items")
                        .HasForeignKey("SupplierId");
                });

            modelBuilder.Entity("API.Models.Orders.CustomerPurchaseOrder", b =>
                {
                    b.HasOne("API.Models.Authentication.Customer", "Customer")
                        .WithMany("CustomerPurchaseOrders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("API.Models.Orders.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("API.Models.Suppliers.Supplier", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("API.Models.Authentication.Customer", b =>
                {
                    b.Navigation("CustomerPurchaseOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
