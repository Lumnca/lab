﻿// <auto-generated />
using System;
using LabExam.DataSource;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LabExam.Migrations
{
    [DbContext(typeof(LabContext))]
    [Migration("20190120130027_CreateDB")]
    partial class CreateDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LabExam.Models.Module", b =>
                {
                    b.Property<int>("ModuleID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddTime");

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.HasKey("ModuleID");

                    b.ToTable("Modules");
                });
#pragma warning restore 612, 618
        }
    }
}
