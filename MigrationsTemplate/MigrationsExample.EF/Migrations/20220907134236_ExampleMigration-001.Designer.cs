﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MigrationsExample.EF.Models;

#nullable disable

namespace MigrationsExample.EF.Migrations
{
    [DbContext(typeof(MigrationsExampleContext))]
    [Migration("20220907134236_ExampleMigration-001")]
    partial class ExampleMigration001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MigrationsExample.EF.Models.Account", b =>
                {
                    b.Property<string>("Email")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("email");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("username");

                    b.ToTable("accounts", (string)null);
                });

            modelBuilder.Entity("MigrationsExample.EF.Models.File", b =>
                {
                    b.Property<string>("Filename")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("filename");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Location")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("location");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("userId");

                    b.ToTable("files", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
