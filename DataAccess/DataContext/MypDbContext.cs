using System;
using System.Collections.Generic;
using Domain.Location;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace DataAccess.DataContext;

public partial class MypDbContext : DbContext
{
    public MypDbContext() { }

    public MypDbContext(DbContextOptions<MypDbContext> options)
        : base(options) { }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Meeting> Meetings { get; set; }

    public virtual DbSet<MeetingArrangement> MeetingArrangements { get; set; }

    public virtual DbSet<MeetingPhoto> MeetingPhotos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseLazyLoadingProxies();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("addresses");

            entity.Property(e => e.AddressText).HasMaxLength(255);
        });

        modelBuilder.Entity<Meeting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("meetings");

            entity.HasIndex(e => e.AddressId, "IX_Meetings_AddressID");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.DateTime).HasMaxLength(6);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.DetailedDescription).HasColumnType("text");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Address).WithMany(p => p.Meetings)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Meetings_Addresses_AddressID");
        });

        modelBuilder.Entity<MeetingArrangement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("meetingarrangements");

            entity.HasIndex(e => e.MeetingId, "IX_MeetingArrangements_MeetingID");

            entity.HasIndex(e => e.UserId, "IX_MeetingArrangements_UserID");

            entity.Property(e => e.MeetingId).HasColumnName("MeetingID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Meeting).WithMany(p => p.MeetingArrangements)
                .HasForeignKey(d => d.MeetingId)
                .HasConstraintName("FK_MeetingArrangements_Meetings_MeetingID");

            entity.HasOne(d => d.User).WithMany(p => p.MeetingArrangements)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_MeetingArrangements_Users_UserID");
        });

        modelBuilder.Entity<MeetingPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("meetingphotos");

            entity.HasIndex(e => e.MeetingId, "IX_MeetingPhotos_MeetingID");

            entity.Property(e => e.MeetingId).HasColumnName("MeetingID");
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(255)
                .HasColumnName("PhotoURL");
            entity.Property(e => e.UploadDateTime).HasMaxLength(6);

            entity.HasOne(d => d.Meeting).WithMany(p => p.MeetingPhotos)
                .HasForeignKey(d => d.MeetingId)
                .HasConstraintName("FK_MeetingPhotos_Meetings_MeetingID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.AddressId, "IX_Users_AddressID");

            entity.HasIndex(e => e.Email, "IX_Users_Email").IsUnique();

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.DateOfBirth).HasMaxLength(6);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.Address).WithMany(p => p.Users)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Addresses_AddressID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
