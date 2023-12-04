﻿// <auto-generated />
using System;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(SharedContext))]
    partial class SharedContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("longblob");

                    b.Property<string>("Token")
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

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Ean")
                        .HasColumnType("longtext");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("longtext");

                    b.Property<int>("ItemType")
                        .HasColumnType("int");

                    b.Property<float>("Mass")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ReservedQuantity")
                        .HasColumnType("int");

                    b.Property<int?>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("Id");

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

            modelBuilder.Entity("API.Models.Orders.OrderLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int?>("PurchaseOrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("PurchaseOrderId");

                    b.ToTable("OrderLine");
                });

            modelBuilder.Entity("API.Models.Suppliers.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("API.Models.Authentication.Employee", b =>
                {
                    b.HasBaseType("API.Models.Authentication.User");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("API.Models.Authentication.Guest", b =>
                {
                    b.HasBaseType("API.Models.Authentication.User");

                    b.HasDiscriminator().HasValue("Guest");
                });

            modelBuilder.Entity("API.Models.Items.DefaultItem", b =>
                {
                    b.HasBaseType("API.Models.Items.Item");

                    b.HasDiscriminator().HasValue("DefaultItem");
                });

            modelBuilder.Entity("API.Models.Items.Liquor", b =>
                {
                    b.HasBaseType("API.Models.Items.Item");

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

                    b.Property<int?>("WineType")
                        .HasColumnType("int");

                    b.Property<string>("Winery")
                        .HasColumnType("longtext");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Wine");
                });

            modelBuilder.Entity("API.Models.Orders.PurchaseOrder", b =>
                {
                    b.HasBaseType("API.Models.Orders.Order");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("GuestId")
                        .HasColumnType("int");

                    b.Property<int>("PurchaseOrderState")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("double");

                    b.HasIndex("CustomerId");

                    b.HasIndex("GuestId");

                    b.HasDiscriminator().HasValue("PurchaseOrder");
                });

            modelBuilder.Entity("API.Models.Authentication.Admin", b =>
                {
                    b.HasBaseType("API.Models.Authentication.Employee");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("API.Models.Authentication.Customer", b =>
                {
                    b.HasBaseType("API.Models.Authentication.Guest");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("API.Models.Items.Item", b =>
                {
                    b.HasOne("API.Models.Suppliers.Supplier", null)
                        .WithMany("Items")
                        .HasForeignKey("SupplierId");
                });

            modelBuilder.Entity("API.Models.Orders.OrderLine", b =>
                {
                    b.HasOne("API.Models.Items.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId");

                    b.HasOne("API.Models.Orders.PurchaseOrder", "PurchaseOrder")
                        .WithMany("OrderLines")
                        .HasForeignKey("PurchaseOrderId");

                    b.Navigation("Item");

                    b.Navigation("PurchaseOrder");
                });

            modelBuilder.Entity("API.Models.Orders.PurchaseOrder", b =>
                {
                    b.HasOne("API.Models.Authentication.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("API.Models.Authentication.Guest", "Guest")
                        .WithMany("PurchaseOrders")
                        .HasForeignKey("GuestId");

                    b.Navigation("Customer");

                    b.Navigation("Guest");
                });

            modelBuilder.Entity("API.Models.Suppliers.Supplier", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("API.Models.Authentication.Guest", b =>
                {
                    b.Navigation("PurchaseOrders");
                });

            modelBuilder.Entity("API.Models.Orders.PurchaseOrder", b =>
                {
                    b.Navigation("OrderLines");
                });
#pragma warning restore 612, 618
        }
    }
}
