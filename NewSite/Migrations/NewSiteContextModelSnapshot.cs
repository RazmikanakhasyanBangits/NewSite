﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewSite;

#nullable disable

namespace NewSite.Migrations
{
    [DbContext(typeof(NewSiteContext))]
    partial class NewSiteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NewSite.Entity.Role", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = (short)1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = (short)2,
                            Name = "User"
                        },
                        new
                        {
                            Id = (short)3,
                            Name = "Developer"
                        });
                });

            modelBuilder.Entity("NewSite.Entity.User", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("RoleId")
                        .HasColumnType("smallint");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NewSite.Entity.UserDetails", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<short>("Age")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("BirthData")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserDetails");
                });

            modelBuilder.Entity("NewSite.Entity.User", b =>
                {
                    b.HasOne("NewSite.Entity.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("NewSite.Entity.UserDetails", b =>
                {
                    b.HasOne("NewSite.Entity.User", "User")
                        .WithOne("Detail")
                        .HasForeignKey("NewSite.Entity.UserDetails", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("NewSite.Entity.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("NewSite.Entity.User", b =>
                {
                    b.Navigation("Detail");
                });
#pragma warning restore 612, 618
        }
    }
}
