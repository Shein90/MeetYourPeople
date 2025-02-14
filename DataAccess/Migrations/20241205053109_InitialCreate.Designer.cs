﻿// <auto-generated />
using System;
using DataAccess.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(MypDbContext))]
    [Migration("20241205053109_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");
            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Domain.Event.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int")
                        .HasColumnName("AddressID");

                    b.Property<DateTime>("DateTime")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("DetailedDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MaxParticipants")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "AddressId" }, "IX_Meetings_AddressID");

                    b.ToTable("meetings", (string)null);
                });

            modelBuilder.Entity("Domain.Event.MeetingArrangement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MeetingId")
                        .HasColumnType("int")
                        .HasColumnName("MeetingID");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "MeetingId" }, "IX_MeetingArrangements_MeetingID");

                    b.HasIndex(new[] { "UserId" }, "IX_MeetingArrangements_UserID");

                    b.ToTable("meetingarrangements", (string)null);
                });

            modelBuilder.Entity("Domain.Event.MeetingPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MeetingId")
                        .HasColumnType("int")
                        .HasColumnName("MeetingID");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("PhotoURL");

                    b.Property<DateTime>("UploadDateTime")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "MeetingId" }, "IX_MeetingPhotos_MeetingID");

                    b.ToTable("meetingphotos", (string)null);
                });

            modelBuilder.Entity("Domain.Location.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AddressText")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("addresses", (string)null);
                });

            modelBuilder.Entity("Domain.UserDomain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int")
                        .HasColumnName("AddressID");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasMaxLength(6)
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "AddressId" }, "IX_Users_AddressID");

                    b.HasIndex(new[] { "Email" }, "IX_Users_Email")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Domain.Event.Meeting", b =>
                {
                    b.HasOne("Domain.Location.Address", "Address")
                        .WithMany("Meetings")
                        .HasForeignKey("AddressId")
                        .IsRequired()
                        .HasConstraintName("FK_Meetings_Addresses_AddressID");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Domain.Event.MeetingArrangement", b =>
                {
                    b.HasOne("Domain.Event.Meeting", "Meeting")
                        .WithMany("MeetingArrangements")
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_MeetingArrangements_Meetings_MeetingID");

                    b.HasOne("Domain.UserDomain.User", "User")
                        .WithMany("MeetingArrangements")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_MeetingArrangements_Users_UserID");

                    b.Navigation("Meeting");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Event.MeetingPhoto", b =>
                {
                    b.HasOne("Domain.Event.Meeting", "Meeting")
                        .WithMany("MeetingPhotos")
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_MeetingPhotos_Meetings_MeetingID");

                    b.Navigation("Meeting");
                });

            modelBuilder.Entity("Domain.UserDomain.User", b =>
                {
                    b.HasOne("Domain.Location.Address", "Address")
                        .WithMany("Users")
                        .HasForeignKey("AddressId")
                        .IsRequired()
                        .HasConstraintName("FK_Users_Addresses_AddressID");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Domain.Event.Meeting", b =>
                {
                    b.Navigation("MeetingArrangements");

                    b.Navigation("MeetingPhotos");
                });

            modelBuilder.Entity("Domain.Location.Address", b =>
                {
                    b.Navigation("Meetings");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.UserDomain.User", b =>
                {
                    b.Navigation("MeetingArrangements");
                });
#pragma warning restore 612, 618
        }
    }
}
