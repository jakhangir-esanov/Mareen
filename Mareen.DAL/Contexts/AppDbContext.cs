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
    public DbSet<HotelAttachment> HotelAttachments { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentHistory> PaymentHistories { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomAttachment> RoomAttachments { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceAttachment> ServiceAttachments { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region FluentAPI

        //One-to-many realition for Booking and BookingItem
        modelBuilder.Entity<Booking>()
            .HasMany(b => b.BookingItems)
            .WithOne(b => b.Booking)
            .OnDelete(DeleteBehavior.NoAction);

        //One-to-one realition for Guest and Payment
        modelBuilder.Entity<Payment>()
            .HasOne(b => b.Guest)
            .WithOne()
            .HasForeignKey<Payment>(b => b.GuestId)
            .OnDelete(DeleteBehavior.NoAction);

        //One-to-many realition for Guest and Booking
        modelBuilder.Entity<Guest>()
            .HasMany(b => b.Bookings)
            .WithOne(b => b.Guest)
            .OnDelete(DeleteBehavior.NoAction);

        //One-to-many realition for Hotel and Room
        modelBuilder.Entity<Hotel>()
            .HasMany(b => b.Rooms)
            .WithOne(b => b.Hotel)
            .HasForeignKey(b => b.HotelId)
            .OnDelete(DeleteBehavior.NoAction);

        //One-to-many realition for Hotel and Employee
        modelBuilder.Entity<Hotel>()
            .HasMany(b => b.Employees)
            .WithOne(b => b.Hotel)
            .OnDelete(DeleteBehavior.NoAction);

        //One-to-many realition for Guest and PaymentHistory
        modelBuilder.Entity<PaymentHistory>()
            .HasOne(b => b.Guest)
            .WithMany(b => b.Transactions)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Guest>()
            .HasMany(b => b.Transactions)
            .WithOne(b => b.Guest)
            .OnDelete(DeleteBehavior.Cascade);

        //One-to-one realition for Booking and PaymentHistory
        modelBuilder.Entity<Booking>()
            .HasOne<PaymentHistory>()
            .WithOne(b => b.Booking)
            .HasForeignKey<Booking>(b => b.Id)
            .OnDelete(DeleteBehavior.NoAction);

        //One-to-one realition for Payment and PaymentHistory
        modelBuilder.Entity<Payment>()
            .HasOne<PaymentHistory>()
            .WithOne(b => b.Payment)
            .HasForeignKey<PaymentHistory>(b => b.Id)
            .OnDelete(DeleteBehavior.NoAction);

        //Many-to-many realition for Hotel and Attachment
        modelBuilder.Entity<HotelAttachment>()
            .HasKey(h => h.Id);

        modelBuilder.Entity<HotelAttachment>()
            .HasOne(h => h.Hotel)
            .WithMany(h => h.HotelAttachments)
            .HasForeignKey(h => h.HotelId);

        modelBuilder.Entity<HotelAttachment>()
            .HasOne(h => h.Attachment)
            .WithMany(h => h.HotelAttachments)
            .HasForeignKey(h => h.AttachmentId);

        //Many-to-many realition for Room and Attachment
        modelBuilder.Entity<RoomAttachment>()
            .HasKey(h => h.Id);

        modelBuilder.Entity<RoomAttachment>()
            .HasOne(h => h.Room)
            .WithMany(h => h.RoomAttachments)
            .HasForeignKey(h => h.RoomId);

        modelBuilder.Entity<RoomAttachment>()
            .HasOne(h => h.Attachment)
            .WithMany(h => h.RoomAttachments)
            .HasForeignKey(h => h.AttachmentId);

        //Many-to-many realition for Service and Attachment
        modelBuilder.Entity<ServiceAttachment>()
            .HasKey(h => h.Id);

        modelBuilder.Entity<ServiceAttachment>()
            .HasOne(h => h.Service)
            .WithMany(h => h.ServiceAttachments)
            .HasForeignKey(h => h.ServiceId);

        modelBuilder.Entity<ServiceAttachment>()
            .HasOne(h => h.Attachment)
            .WithMany(h => h.ServiceAttachments)
            .HasForeignKey(h => h.AttachmentId);

        #endregion
    }

}
