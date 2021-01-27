﻿// <auto-generated />
using EvanApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EvanApi.Migrations
{
    [DbContext(typeof(CoopContext))]
    partial class CoopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("EvanApi.models.Account", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("accountid")
                        .HasColumnType("text");

                    b.Property<string>("address")
                        .HasColumnType("text");

                    b.Property<string>("email")
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<string>("phone")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Account");

                    b.HasData(
                        new
                        {
                            id = "1",
                            accountid = "1",
                            address = "Khusibu",
                            email = "dangolevan@gmail.com",
                            name = "Evan Dangol",
                            phone = "123456789"
                        },
                        new
                        {
                            id = "2",
                            accountid = "2",
                            address = "Kilagal",
                            email = "bso@gmail.com",
                            name = "Bso Amatya",
                            phone = "234567891"
                        });
                });

            modelBuilder.Entity("EvanApi.models.AccountType", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("accounttype")
                        .HasColumnType("text");

                    b.Property<string>("accounttypeid")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("AccountType");

                    b.HasData(
                        new
                        {
                            id = "1",
                            accounttype = "Daily",
                            accounttypeid = "1"
                        },
                        new
                        {
                            id = "2",
                            accounttype = "Month",
                            accounttypeid = "2"
                        });
                });

            modelBuilder.Entity("EvanApi.models.Transaction", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("accountid")
                        .HasColumnType("text");

                    b.Property<string>("accounttypeid")
                        .HasColumnType("text");

                    b.Property<string>("amount")
                        .HasColumnType("text");

                    b.Property<string>("date")
                        .HasColumnType("text");

                    b.Property<string>("entry")
                        .HasColumnType("text");

                    b.Property<string>("type")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Transaction");

                    b.HasData(
                        new
                        {
                            id = "1",
                            accountid = "1",
                            accounttypeid = "1",
                            amount = "100",
                            date = "2021-01-01",
                            entry = "Debit"
                        },
                        new
                        {
                            id = "2",
                            accountid = "2",
                            accounttypeid = "2",
                            amount = "100",
                            date = "2021-01-01",
                            entry = "Debit"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}