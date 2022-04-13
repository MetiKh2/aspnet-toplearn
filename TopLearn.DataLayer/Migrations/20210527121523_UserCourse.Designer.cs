﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopLearn.DataLayer.Context;

namespace TopLearn.DataLayer.Migrations
{
    [DbContext(typeof(TopLearnContext))]
    [Migration("20210527121523_UserCourse")]
    partial class UserCourse
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.Course", b =>
                {
                    b.Property<int>("CourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CourseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CourseDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CourseLevelLevelID")
                        .HasColumnType("int");

                    b.Property<int?>("CourseStatusStatusID")
                        .HasColumnType("int");

                    b.Property<string>("CourseTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DemoFile")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int>("GropeID")
                        .HasColumnType("int");

                    b.Property<string>("ImageName")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<int>("LevelID")
                        .HasColumnType("int");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<int>("StatusID")
                        .HasColumnType("int");

                    b.Property<int?>("SubGroupe")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<long>("TeacherID")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CourseID");

                    b.HasIndex("CourseLevelLevelID");

                    b.HasIndex("CourseStatusStatusID");

                    b.HasIndex("GropeID");

                    b.HasIndex("SubGroupe");

                    b.HasIndex("TeacherID");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.CourseEpisode", b =>
                {
                    b.Property<long>("EpisodeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<string>("EpisodeFile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("EpisodeTime")
                        .HasColumnType("time");

                    b.Property<string>("EpisodeTitle")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<bool>("IsFree")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.HasKey("EpisodeID");

                    b.HasIndex("CourseID");

                    b.ToTable("CourseEpisodes");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.CourseGroupe", b =>
                {
                    b.Property<int>("GroupeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupeTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<int?>("ParentID")
                        .HasColumnType("int");

                    b.HasKey("GroupeID");

                    b.HasIndex("ParentID");

                    b.ToTable("CourseGroupes");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.CourseLevel", b =>
                {
                    b.Property<int>("LevelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LevelTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("LevelID");

                    b.ToTable("CourseLevels");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.CourseStatus", b =>
                {
                    b.Property<int>("StatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StatusTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("StatusID");

                    b.ToTable("CourseStatuses");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.UserCourse", b =>
                {
                    b.Property<int>("UC_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("UC_ID");

                    b.HasIndex("CourseID");

                    b.HasIndex("UserID");

                    b.ToTable("UserCourses");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Order.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsFinaly")
                        .HasColumnType("bit");

                    b.Property<int>("SumAmount")
                        .HasColumnType("int");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("OrderID");

                    b.HasIndex("UserID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Order.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailID");

                    b.HasIndex("CourseID");

                    b.HasIndex("OrderID");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Permission.Permission", b =>
                {
                    b.Property<int>("PermissionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ParentID")
                        .HasColumnType("int");

                    b.Property<string>("PermissionTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("PermissionID");

                    b.HasIndex("ParentID");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Permission.RolePermission", b =>
                {
                    b.Property<int>("RP_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PermissionID")
                        .HasColumnType("int");

                    b.Property<long>("RoleID")
                        .HasColumnType("bigint");

                    b.HasKey("RP_ID");

                    b.HasIndex("PermissionID");

                    b.HasIndex("RoleID");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.User.Role", b =>
                {
                    b.Property<long>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("RoleTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.User.User", b =>
                {
                    b.Property<long>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActiveCode")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("RegisterTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RemovedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserAvatar")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.User.UserInRole", b =>
                {
                    b.Property<long>("UR_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("RoleID")
                        .HasColumnType("bigint");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("UR_ID");

                    b.HasIndex("RoleID");

                    b.HasIndex("UserID");

                    b.ToTable("UserInRoles");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Wallet.Wallet", b =>
                {
                    b.Property<long>("WalletID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsPay")
                        .HasColumnType("bit");

                    b.Property<int>("TypeID")
                        .HasColumnType("int");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.Property<int?>("WalletTypeTypeID")
                        .HasColumnType("int");

                    b.HasKey("WalletID");

                    b.HasIndex("UserID");

                    b.HasIndex("WalletTypeTypeID");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Wallet.WalletType", b =>
                {
                    b.Property<int>("TypeID")
                        .HasColumnType("int");

                    b.Property<string>("TypeTitle")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("TypeID");

                    b.ToTable("WalletTypes");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.Course", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.Course.CourseLevel", "CourseLevel")
                        .WithMany("Courses")
                        .HasForeignKey("CourseLevelLevelID");

                    b.HasOne("TopLearn.DataLayer.Entities.Course.CourseStatus", "CourseStatus")
                        .WithMany("Courses")
                        .HasForeignKey("CourseStatusStatusID");

                    b.HasOne("TopLearn.DataLayer.Entities.Course.CourseGroupe", "CourseGroupe")
                        .WithMany("Courses")
                        .HasForeignKey("GropeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.DataLayer.Entities.Course.CourseGroupe", "CourseSubGroupe")
                        .WithMany("SubGroupes")
                        .HasForeignKey("SubGroupe");

                    b.HasOne("TopLearn.DataLayer.Entities.User.User", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CourseGroupe");

                    b.Navigation("CourseLevel");

                    b.Navigation("CourseStatus");

                    b.Navigation("CourseSubGroupe");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.CourseEpisode", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.Course.Course", "Course")
                        .WithMany("CourseEpisodes")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.CourseGroupe", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.Course.CourseGroupe", null)
                        .WithMany("CourseGroups")
                        .HasForeignKey("ParentID");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.UserCourse", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.Course.Course", "Course")
                        .WithMany("UserCourses")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.DataLayer.Entities.User.User", "User")
                        .WithMany("UserCourses")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Order.Order", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.User.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Order.OrderDetail", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.Course.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.DataLayer.Entities.Order.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Permission.Permission", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.Permission.Permission", null)
                        .WithMany("Permissions")
                        .HasForeignKey("ParentID");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Permission.RolePermission", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.Permission.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.DataLayer.Entities.User.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.User.UserInRole", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.User.Role", "Role")
                        .WithMany("UserInRoles")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.DataLayer.Entities.User.User", "User")
                        .WithMany("UserInRoles")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Wallet.Wallet", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.User.User", "User")
                        .WithMany("Wallets")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.DataLayer.Entities.Wallet.WalletType", "WalletType")
                        .WithMany("Wallets")
                        .HasForeignKey("WalletTypeTypeID");

                    b.Navigation("User");

                    b.Navigation("WalletType");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.Course", b =>
                {
                    b.Navigation("CourseEpisodes");

                    b.Navigation("UserCourses");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.CourseGroupe", b =>
                {
                    b.Navigation("CourseGroups");

                    b.Navigation("Courses");

                    b.Navigation("SubGroupes");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.CourseLevel", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.CourseStatus", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Order.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Permission.Permission", b =>
                {
                    b.Navigation("Permissions");

                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.User.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("UserInRoles");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.User.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("UserCourses");

                    b.Navigation("UserInRoles");

                    b.Navigation("Wallets");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Wallet.WalletType", b =>
                {
                    b.Navigation("Wallets");
                });
#pragma warning restore 612, 618
        }
    }
}
