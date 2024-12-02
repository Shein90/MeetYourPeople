using DataAccess.Options;
using Domain.Event;
using Domain.Location;
using Domain.UserDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataAccess;

public class MypDbContext : DbContext
{
    private readonly DataBaseAccessSettings _options;
    public DbSet<User> Users { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<MeetingArrangement> MeetingArrangements { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<MeetingPhoto> MeetingPhotos { get; set; }

    //public MypDbContext(IOptions<DataBaseAccessSettings> options)
    //{
    //    _options = options.Value;
    //    //Database.EnsureCreated();
    //}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=localhost;port=3306;database=myp_db;user=root;password=!CSer1990;",
        ServerVersion.Parse("8.0.37-mysql"));
        optionsBuilder.LogTo(Console.WriteLine);
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseMySql(_options.ConnectionString,
    //    new MySqlServerVersion(new Version(8, 0, 37)));
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<MeetingArrangement>()
        //      .HasKey(ma => ma.MeetingArrangementID);

        //modelBuilder.Entity<MeetingArrangement>()
        //    .HasOne(ma => ma.User)
        //    .WithMany(u => u.MeetingArrangements)
        //    .HasForeignKey(ma => ma.UserID)
        //    .OnDelete(DeleteBehavior.Cascade);

        //modelBuilder.Entity<MeetingArrangement>()
        //    .HasOne(ma => ma.Meeting)
        //    .WithMany(m => m.MeetingArrangements)
        //    .HasForeignKey(ma => ma.MeetingID)
        //    .OnDelete(DeleteBehavior.Cascade);

        //modelBuilder.Entity<Meeting>()
        //    .HasMany(m => m.MeetingPhotos)
        //    .WithOne(mp => mp.Meeting)
        //    .OnDelete(DeleteBehavior.Cascade);

        //// Настройка связей для Meeting
        //modelBuilder.Entity<Meeting>()
        //    .HasMany(m => m.MeetingPhotos)
        //    .WithOne(mp => mp.Meeting)
        //    .HasForeignKey(mp => mp.MeetingID) // Добавляем явный внешний ключ
        //    .OnDelete(DeleteBehavior.Cascade);

        //modelBuilder.Entity<Meeting>()
        //    .HasOne(m => m.Address)
        //    .WithMany(a => a.Meetings)
        //    .HasForeignKey(m => m.AddressID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<User>()
        //    .HasOne(u => u.Address)
        //    .WithMany(a => a.Users)
        //    .HasForeignKey(u => u.AddressID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //base.OnModelCreating(modelBuilder);

        // Таблица Address
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.AddressText).IsRequired().HasMaxLength(255);
        });

        // Таблица User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.UserName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
            entity.Property(u => u.DateOfBirth).IsRequired();

            entity.HasOne(u => u.Address)
                .WithMany(a => a.Users)
                .HasForeignKey(u => u.AddressID)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Таблица Meeting
        modelBuilder.Entity<Meeting>(entity =>
        {
            entity.HasKey(m => m.Id);

            entity.Property(m => m.DateTime).IsRequired();
            entity.Property(m => m.Title).IsRequired().HasMaxLength(100);
            entity.Property(m => m.Description).HasMaxLength(255);
            entity.Property(m => m.DetailedDescription).HasColumnType("TEXT");
            entity.Property(m => m.MaxParticipants).IsRequired();

            entity.HasOne(m => m.Address)
                .WithMany(a => a.Meetings)
                .HasForeignKey(m => m.AddressID)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Таблица MeetingArrangement
        modelBuilder.Entity<MeetingArrangement>(entity =>
        {
            entity.HasKey(ma => ma.Id);

            entity.HasOne(ma => ma.User)
                .WithMany(u => u.MeetingArrangements)
                .HasForeignKey(ma => ma.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ma => ma.Meeting)
                .WithMany(m => m.MeetingArrangements)
                .HasForeignKey(ma => ma.MeetingID)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(ma => ma.UserRole).HasMaxLength(50);
        });

        // Таблица MeetingPhotos
        modelBuilder.Entity<MeetingPhoto>(entity =>
        {
            entity.HasKey(mp => mp.Id);

            entity.Property(mp => mp.UploadDateTime).IsRequired();
            entity.Property(mp => mp.PhotoURL).IsRequired().HasMaxLength(255);

            entity.HasOne(mp => mp.Meeting)
                .WithMany(m => m.MeetingPhotos)
                .HasForeignKey(mp => mp.MeetingID)
                .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);
    }
}