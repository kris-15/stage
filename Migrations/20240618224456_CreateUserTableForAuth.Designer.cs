﻿// <auto-generated />
using System;
using AtmEquityProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AtmEquityProject.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240618224456_CreateUserTableForAuth")]
    partial class CreateUserTableForAuth
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AtmEquityProject.Models.Atm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Devise")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumCompte")
                        .HasColumnType("int");

                    b.Property<int>("Seuil")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Atms");
                });

            modelBuilder.Entity("AtmEquityProject.Models.Balance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("IdAtmId")
                        .HasColumnType("int");

                    b.Property<int>("Solde")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdAtmId");

                    b.ToTable("Balances");
                });

            modelBuilder.Entity("AtmEquityProject.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "System",
                            LastName = "",
                            Password = "System",
                            Username = "System",
                            isActive = false
                        });
                });

            modelBuilder.Entity("AtmEquityProject.Models.Balance", b =>
                {
                    b.HasOne("AtmEquityProject.Models.Atm", "IdAtm")
                        .WithMany("Balances")
                        .HasForeignKey("IdAtmId");

                    b.Navigation("IdAtm");
                });

            modelBuilder.Entity("AtmEquityProject.Models.Atm", b =>
                {
                    b.Navigation("Balances");
                });
#pragma warning restore 612, 618
        }
    }
}
