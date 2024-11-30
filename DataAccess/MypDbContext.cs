using Domain.Meeting;
using Domain.UserDomain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class MypDbContext : DbContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<Meeting>? Meetings { get; set; }
    public DbSet<MeetingArrangement>? MeetingArrangements { get; set; }
    public DbSet<Address>? Addresses { get; set; }
    public DbSet<MeetingPhoto>? MeetingPhotos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=myp_db;User=root;Password=!CSer1990;",
        new MySqlServerVersion(new Version(8, 0, 37)),
        options => options.EnableRetryOnFailure());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MeetingArrangement>()
              .HasKey(ma => ma.MeetingArrangementID);
        
        modelBuilder.Entity<MeetingArrangement>()
            .HasOne(ma => ma.User)
            .WithMany(u => u.MeetingArrangements)
            .HasForeignKey(ma => ma.UserID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MeetingArrangement>()
            .HasOne(ma => ma.Meeting)
            .WithMany(m => m.MeetingArrangements)
            .HasForeignKey(ma => ma.MeetingID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Meeting>()
            .HasMany(m => m.MeetingPhotos)
            .WithOne(mp => mp.Meeting)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Meeting>()
            .HasOne(m => m.Address)
            .WithMany(a => a.Meetings)
            .HasForeignKey(m => m.AddressID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Address)
            .WithMany(a => a.Users)
            .HasForeignKey(u => u.AddressID)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}

