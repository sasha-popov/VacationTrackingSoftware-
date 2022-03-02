﻿// <auto-generated />
using System;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(ProjectContext))]
    [Migration("20181227125455_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BLL.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("TeamIdId");

                    b.HasKey("Id");

                    b.HasIndex("TeamIdId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("BLL.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BLL.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ManagerIdId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ManagerIdId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("BLL.Models.TeamUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("TeamIdId");

                    b.Property<int?>("UserIdId");

                    b.HasKey("Id");

                    b.HasIndex("TeamIdId");

                    b.HasIndex("UserIdId");

                    b.ToTable("TeamUsers");
                });

            modelBuilder.Entity("BLL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BLL.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("RoleIdId");

                    b.Property<int?>("UserIdId");

                    b.HasKey("Id");

                    b.HasIndex("RoleIdId");

                    b.HasIndex("UserIdId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("BLL.Models.UserVacantionRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("Status");

                    b.Property<int?>("UserIdId");

                    b.Property<int?>("VacantionTypeIdId");

                    b.HasKey("Id");

                    b.HasIndex("UserIdId");

                    b.HasIndex("VacantionTypeIdId");

                    b.ToTable("UserVacantionRequests");
                });

            modelBuilder.Entity("BLL.Models.VacationPolicy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<int>("Payments");

                    b.Property<int?>("VacationTypeIdId");

                    b.Property<int>("WorkingYear");

                    b.HasKey("Id");

                    b.HasIndex("VacationTypeIdId");

                    b.ToTable("VacationPolicies");
                });

            modelBuilder.Entity("BLL.Models.VacationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("VacationTypes");
                });

            modelBuilder.Entity("BLL.Models.Project", b =>
                {
                    b.HasOne("BLL.Models.Team", "TeamId")
                        .WithMany("Projects")
                        .HasForeignKey("TeamIdId");
                });

            modelBuilder.Entity("BLL.Models.Team", b =>
                {
                    b.HasOne("BLL.Models.User", "ManagerId")
                        .WithMany()
                        .HasForeignKey("ManagerIdId");
                });

            modelBuilder.Entity("BLL.Models.TeamUser", b =>
                {
                    b.HasOne("BLL.Models.Team", "TeamId")
                        .WithMany("TeamUsers")
                        .HasForeignKey("TeamIdId");

                    b.HasOne("BLL.Models.User", "UserId")
                        .WithMany()
                        .HasForeignKey("UserIdId");
                });

            modelBuilder.Entity("BLL.Models.UserRole", b =>
                {
                    b.HasOne("BLL.Models.Role", "RoleId")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleIdId");

                    b.HasOne("BLL.Models.User", "UserId")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserIdId");
                });

            modelBuilder.Entity("BLL.Models.UserVacantionRequest", b =>
                {
                    b.HasOne("BLL.Models.User", "UserId")
                        .WithMany()
                        .HasForeignKey("UserIdId");

                    b.HasOne("BLL.Models.VacationType", "VacantionTypeId")
                        .WithMany()
                        .HasForeignKey("VacantionTypeIdId");
                });

            modelBuilder.Entity("BLL.Models.VacationPolicy", b =>
                {
                    b.HasOne("BLL.Models.VacationType", "VacationTypeId")
                        .WithMany()
                        .HasForeignKey("VacationTypeIdId");
                });
#pragma warning restore 612, 618
        }
    }
}