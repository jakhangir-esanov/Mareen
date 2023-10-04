using Mareen.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mareen.DAL.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingItem> BookingsItems { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentHistory> PaymentHistories { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>()
            .HasMany(b => b.BookingItems)
            .WithOne(b => b.Booking)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Guest>()
            .HasMany(b => b.Transactions)
            .WithOne(b => b.Guest)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Payment>()
            .HasOne(b => b.Guest)
            .WithOne()
            .HasForeignKey<Payment>(b => b.GuestId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Guest>()
            .HasMany(b => b.Bookings)
            .WithOne(b => b.Guest)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Hotel>()
            .HasMany(b => b.Rooms)
            .WithOne(b => b.Hotel)
            .HasForeignKey(b => b.HotelId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Hotel>()
            .HasMany(b => b.Employees)
            .WithOne(b => b.Hotel)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<PaymentHistory>()
            .HasOne(b => b.Guest)
            .WithMany(b => b.Transactions)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Attachment>()
            .HasOne<Room>()
            .WithOne(b => b.Attachment)
            .HasForeignKey<Room>(b => b.AttachmentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Booking>()
            .HasOne<PaymentHistory>()
            .WithOne(b => b.Booking)
            .HasForeignKey<Booking>(b => b.Id)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Payment>()
            .HasOne<PaymentHistory>()
            .WithOne(b => b.Payment)
            .HasForeignKey<PaymentHistory>(b => b.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
