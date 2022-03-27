﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Warsztat.Models;

#nullable disable

namespace Warsztat.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220327122917_correctedClientsTable")]
    partial class correctedClientsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.1.22076.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Warsztat.Models.Activity", b =>
                {
                    b.Property<int>("activityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("activityId"), 1L, 1);

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("result")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("activityId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("Warsztat.Models.ActivityDictionary", b =>
                {
                    b.Property<string>("activityType")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("activityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("activityType");

                    b.ToTable("ActivityDictionaries");
                });

            modelBuilder.Entity("Warsztat.Models.Car", b =>
                {
                    b.Property<int>("carId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("carId"), 1L, 1);

                    b.Property<long>("registrationNumber")
                        .HasColumnType("bigint");

                    b.HasKey("carId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("Warsztat.Models.CarType", b =>
                {
                    b.Property<string>("mark")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("mark");

                    b.ToTable("CarTypes");
                });

            modelBuilder.Entity("Warsztat.Models.Client", b =>
                {
                    b.Property<int>("clientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("clientId"), 1L, 1);

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("number")
                        .HasColumnType("bigint");

                    b.Property<string>("surrname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("clientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Warsztat.Models.Personel", b =>
                {
                    b.Property<int>("personelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("personelId"), 1L, 1);

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("number")
                        .HasColumnType("bigint");

                    b.Property<string>("surrname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("personelId");

                    b.ToTable("Personels");
                });

            modelBuilder.Entity("Warsztat.Models.Request", b =>
                {
                    b.Property<int>("carId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("carId"), 1L, 1);

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("result")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("carId");

                    b.ToTable("Requests");
                });
#pragma warning restore 612, 618
        }
    }
}
