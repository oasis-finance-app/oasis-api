﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oasis.Context;

#nullable disable

namespace Oasis.Migrations
{
    [DbContext(typeof(EntityContext))]
    partial class EntityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("Oasis.Models.BankAccount", b =>
                {
                    b.Property<int>("BankAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Bank")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OtherBankName")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("BankAccountId");

                    b.HasIndex("CustomerId");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("Oasis.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Oasis.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<int>("BankAccountId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("TransactionId");

                    b.HasIndex("BankAccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Oasis.Models.BankAccount", b =>
                {
                    b.HasOne("Oasis.Models.Customer", "Customer")
                        .WithMany("BankAccounts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Oasis.Models.Transaction", b =>
                {
                    b.HasOne("Oasis.Models.BankAccount", "BankAccount")
                        .WithMany("Transactions")
                        .HasForeignKey("BankAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankAccount");
                });

            modelBuilder.Entity("Oasis.Models.BankAccount", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Oasis.Models.Customer", b =>
                {
                    b.Navigation("BankAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}
