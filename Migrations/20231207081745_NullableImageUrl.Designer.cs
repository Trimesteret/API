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
    [DbContext(typeof(SharedContext))]
    [Migration("20231207081745_NullableImageUrl")]
    partial class NullableImageUrl
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("API.Enums.CustomEnum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("EnumType")
                        .HasColumnType("int");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CustomEnums");
                });

            modelBuilder.Entity("API.Enums.ItemEnumRelation", b =>
                {
                    b.Property<int>("ItemEnumRelationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CustomEnumId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int?>("WineId")
                        .HasColumnType("int");

                    b.HasKey("ItemEnumRelationId");

                    b.HasIndex("CustomEnumId");

                    b.HasIndex("ItemId");

                    b.HasIndex("WineId");

                    b.ToTable("ItemEnumRelations");
                });

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

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("longblob");

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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Ean")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("longtext");

                    b.Property<int>("ItemType")
                        .HasColumnType("int");

                    b.Property<float>("Mass")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
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
                        .IsRequired()
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

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int>("PurchaseOrderId")
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
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("GrapeSort")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TastingNotes")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double?>("Volume")
                        .HasColumnType("double");

                    b.Property<int?>("WineType")
                        .HasColumnType("int");

                    b.Property<string>("Winery")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Wine");
                });

            modelBuilder.Entity("API.Models.Orders.PurchaseOrder", b =>
                {
                    b.HasBaseType("API.Models.Orders.Order");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("GuestId")
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

            modelBuilder.Entity("API.Enums.ItemEnumRelation", b =>
                {
                    b.HasOne("API.Enums.CustomEnum", "CustomEnum")
                        .WithMany()
                        .HasForeignKey("CustomEnumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Items.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Items.Wine", null)
                        .WithMany("SuitableFor")
                        .HasForeignKey("WineId");

                    b.Navigation("CustomEnum");

                    b.Navigation("Item");
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
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Orders.PurchaseOrder", "PurchaseOrder")
                        .WithMany("OrderLines")
                        .HasForeignKey("PurchaseOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("PurchaseOrder");
                });

            modelBuilder.Entity("API.Models.Orders.PurchaseOrder", b =>
                {
                    b.HasOne("API.Models.Authentication.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Authentication.Guest", "Guest")
                        .WithMany("PurchaseOrders")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

            modelBuilder.Entity("API.Models.Items.Wine", b =>
                {
                    b.Navigation("SuitableFor");
                });

            modelBuilder.Entity("API.Models.Orders.PurchaseOrder", b =>
                {
                    b.Navigation("OrderLines");
                });
#pragma warning restore 612, 618
        }
    }
}
