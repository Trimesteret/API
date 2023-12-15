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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EnumType = 0,
                            Key = "Poultry",
                            Value = "Fjerkræ"
                        },
                        new
                        {
                            Id = 2,
                            EnumType = 0,
                            Key = "Seafood",
                            Value = "Skaldyr"
                        },
                        new
                        {
                            Id = 3,
                            EnumType = 0,
                            Key = "RedMeat",
                            Value = "Oksekød"
                        },
                        new
                        {
                            Id = 4,
                            EnumType = 0,
                            Key = "Pork",
                            Value = "Svinekød"
                        },
                        new
                        {
                            Id = 5,
                            EnumType = 0,
                            Key = "SpicyFood",
                            Value = "Stærk mad"
                        },
                        new
                        {
                            Id = 6,
                            EnumType = 0,
                            Key = "Cheese",
                            Value = "Ost"
                        },
                        new
                        {
                            Id = 7,
                            EnumType = 0,
                            Key = "Pasta",
                            Value = "Pasta"
                        },
                        new
                        {
                            Id = 8,
                            EnumType = 0,
                            Key = "Pizza",
                            Value = "Pizza"
                        },
                        new
                        {
                            Id = 9,
                            EnumType = 0,
                            Key = "Vegetarian",
                            Value = "Vegetar"
                        },
                        new
                        {
                            Id = 10,
                            EnumType = 0,
                            Key = "Salad",
                            Value = "Salat"
                        },
                        new
                        {
                            Id = 11,
                            EnumType = 0,
                            Key = "Dessert",
                            Value = "Dessert"
                        });
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
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("longblob");

                    b.Property<bool>("SignedUp")
                        .HasColumnType("tinyint(1)");

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

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Item");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("API.Models.Orders.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AddressLine")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Door")
                        .HasColumnType("longtext");

                    b.Property<string>("Floor")
                        .HasColumnType("longtext");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("API.Models.Orders.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("double");

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

                    b.Property<double?>("ItemPrice")
                        .HasColumnType("double");

                    b.Property<double>("LinePrice")
                        .HasColumnType("double");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderLines");
                });

            modelBuilder.Entity("API.Models.Suppliers.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("API.Models.Suppliers.SupplierItemRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.ToTable("SupplierItemRelations");
                });

            modelBuilder.Entity("API.Models.Authentication.Customer", b =>
                {
                    b.HasBaseType("API.Models.Authentication.User");

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

            modelBuilder.Entity("API.Models.Orders.InboundOrder", b =>
                {
                    b.HasBaseType("API.Models.Orders.Order");

                    b.Property<int>("InboundOrderState")
                        .HasColumnType("int");

                    b.Property<int?>("SupplierId")
                        .HasColumnType("int");

                    b.HasIndex("SupplierId");

                    b.HasDiscriminator().HasValue("InboundOrder");
                });

            modelBuilder.Entity("API.Models.Orders.PurchaseOrder", b =>
                {
                    b.HasBaseType("API.Models.Orders.Order");

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("PurchaseOrderState")
                        .HasColumnType("int");

                    b.HasIndex("AddressId");

                    b.HasIndex("CustomerId");

                    b.HasDiscriminator().HasValue("PurchaseOrder");
                });

            modelBuilder.Entity("API.Models.Authentication.Admin", b =>
                {
                    b.HasBaseType("API.Models.Authentication.Employee");

                    b.HasDiscriminator().HasValue("Admin");
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

            modelBuilder.Entity("API.Models.Orders.OrderLine", b =>
                {
                    b.HasOne("API.Models.Items.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Orders.Order", null)
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("API.Models.Suppliers.SupplierItemRelation", b =>
                {
                    b.HasOne("API.Models.Suppliers.Supplier", null)
                        .WithMany("SupplierItemRelations")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API.Models.Orders.InboundOrder", b =>
                {
                    b.HasOne("API.Models.Suppliers.Supplier", "Supplier")
                        .WithMany("InboundOrders")
                        .HasForeignKey("SupplierId");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("API.Models.Orders.PurchaseOrder", b =>
                {
                    b.HasOne("API.Models.Orders.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("API.Models.Authentication.Customer", "Customer")
                        .WithMany("PurchaseOrders")
                        .HasForeignKey("CustomerId");

                    b.Navigation("Address");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("API.Models.Orders.Order", b =>
                {
                    b.Navigation("OrderLines");
                });

            modelBuilder.Entity("API.Models.Suppliers.Supplier", b =>
                {
                    b.Navigation("InboundOrders");

                    b.Navigation("SupplierItemRelations");
                });

            modelBuilder.Entity("API.Models.Authentication.Customer", b =>
                {
                    b.Navigation("PurchaseOrders");
                });

            modelBuilder.Entity("API.Models.Items.Wine", b =>
                {
                    b.Navigation("SuitableFor");
                });
#pragma warning restore 612, 618
        }
    }
}
