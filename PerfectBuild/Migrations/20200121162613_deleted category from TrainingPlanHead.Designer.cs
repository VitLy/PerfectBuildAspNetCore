﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PerfectBuild.Data;

namespace PerfectBuild.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20200121162613_deleted category from TrainingPlanHead")]
    partial class deletedcategoryfromTrainingPlanHead
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PerfectBuild.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("PerfectBuild.Models.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(250);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.HasKey("Id");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("PerfectBuild.Models.Param", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Breast");

                    b.Property<int>("Buttock");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Thigh");

                    b.Property<string>("UserId");

                    b.Property<int>("Waist");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserParam");
                });

            modelBuilder.Entity("PerfectBuild.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DayBirth");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<byte>("Height");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<int>("Sex");

                    b.Property<string>("UserId");

                    b.Property<float>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("PerfectBuild.Models.TrainingHead", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int>("TrainigPlanHeadId");

                    b.Property<int?>("TrainingPlanHeadId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TrainingPlanHeadId");

                    b.HasIndex("UserId");

                    b.ToTable("TrainingHeads");
                });

            modelBuilder.Entity("PerfectBuild.Models.TrainingPlanHead", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description")
                        .HasMaxLength(250);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<byte>("TrainingDays");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TrainingPlanHeads");
                });

            modelBuilder.Entity("PerfectBuild.Models.TrainingPlanSpec", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Amount");

                    b.Property<int>("ExId");

                    b.Property<int>("Order");

                    b.Property<int>("ProgramHeadId");

                    b.Property<byte>("Set");

                    b.Property<int?>("TrainingPlanHeadId");

                    b.Property<float>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("ExId");

                    b.HasIndex("TrainingPlanHeadId");

                    b.ToTable("TrainingPlanSpecs");
                });

            modelBuilder.Entity("PerfectBuild.Models.TrainingProgramHead", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description")
                        .HasMaxLength(250);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("TrainingProgramHeads");
                });

            modelBuilder.Entity("PerfectBuild.Models.TrainingProgramSpec", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Amount");

                    b.Property<int>("ExId");

                    b.Property<int>("ProgramHeadId");

                    b.Property<byte>("Set");

                    b.Property<float>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("ExId");

                    b.HasIndex("ProgramHeadId");

                    b.ToTable("TrainingProgramSpecs");
                });

            modelBuilder.Entity("PerfectBuild.Models.TrainingSpec", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Amount");

                    b.Property<int>("ExId");

                    b.Property<int>("HeadId");

                    b.Property<byte>("Set");

                    b.Property<int>("TrainigPlanId");

                    b.Property<float>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("ExId");

                    b.HasIndex("HeadId");

                    b.HasIndex("TrainigPlanId");

                    b.ToTable("TrainingSpecs");
                });

            modelBuilder.Entity("PerfectBuild.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PerfectBuild.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PerfectBuild.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PerfectBuild.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PerfectBuild.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PerfectBuild.Models.Param", b =>
                {
                    b.HasOne("PerfectBuild.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("PerfectBuild.Models.Profile", b =>
                {
                    b.HasOne("PerfectBuild.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("PerfectBuild.Models.TrainingHead", b =>
                {
                    b.HasOne("PerfectBuild.Models.TrainingPlanHead", "TrainingPlanHead")
                        .WithMany()
                        .HasForeignKey("TrainingPlanHeadId");

                    b.HasOne("PerfectBuild.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("PerfectBuild.Models.TrainingPlanHead", b =>
                {
                    b.HasOne("PerfectBuild.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("PerfectBuild.Models.TrainingPlanSpec", b =>
                {
                    b.HasOne("PerfectBuild.Models.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PerfectBuild.Models.TrainingPlanHead")
                        .WithMany("TrainingPlanSpec")
                        .HasForeignKey("TrainingPlanHeadId");
                });

            modelBuilder.Entity("PerfectBuild.Models.TrainingProgramHead", b =>
                {
                    b.HasOne("PerfectBuild.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PerfectBuild.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("PerfectBuild.Models.TrainingProgramSpec", b =>
                {
                    b.HasOne("PerfectBuild.Models.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PerfectBuild.Models.TrainingProgramHead", "TrainingProgramHead")
                        .WithMany("TrainingProgramSpec")
                        .HasForeignKey("ProgramHeadId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PerfectBuild.Models.TrainingSpec", b =>
                {
                    b.HasOne("PerfectBuild.Models.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PerfectBuild.Models.TrainingHead", "TrainingHead")
                        .WithMany("TrainingSpec")
                        .HasForeignKey("HeadId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PerfectBuild.Models.TrainingPlanHead", "TrainingPlanHead")
                        .WithMany()
                        .HasForeignKey("TrainigPlanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}