﻿// <auto-generated />
using System;
using DemoProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DemoProject.Migrations
{
    [DbContext(typeof(DemoProjectContext))]
    partial class DemoProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DemoProject.Models.Candidate", b =>
                {
                    b.Property<int>("CandidateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CandidateId"));

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GameCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Passport")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PassportExtension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.HasKey("CandidateId");

                    b.HasIndex("SchoolId");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("DemoProject.Models.School", b =>
                {
                    b.Property<int>("SchoolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SchoolId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SchoolId");

                    b.ToTable("Schools", (string)null);

                    b.HasData(
                        new
                        {
                            SchoolId = 1,
                            Name = "Today's Solutions",
                            SchoolCode = "123TSN"
                        },
                        new
                        {
                            SchoolId = 2,
                            Name = "Alphaone School",
                            SchoolCode = "456APS"
                        },
                        new
                        {
                            SchoolId = 3,
                            Name = "Alphaone Technologies",
                            SchoolCode = "456APT"
                        },
                        new
                        {
                            SchoolId = 4,
                            Name = "Amaka and Mary School",
                            SchoolCode = "456AMS"
                        },
                        new
                        {
                            SchoolId = 5,
                            Name = "JT Mentoring School",
                            SchoolCode = "456JTS"
                        });
                });

            modelBuilder.Entity("DemoProject.Models.Candidate", b =>
                {
                    b.HasOne("DemoProject.Models.School", "School")
                        .WithMany("Candidates")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("School");
                });

            modelBuilder.Entity("DemoProject.Models.School", b =>
                {
                    b.Navigation("Candidates");
                });
#pragma warning restore 612, 618
        }
    }
}
