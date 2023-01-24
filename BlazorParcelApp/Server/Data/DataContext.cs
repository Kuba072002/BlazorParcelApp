using Microsoft.EntityFrameworkCore;

namespace BlazorParcelApp.Server.Data {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Locker> Lockers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Parcel>()
                .HasOne(x => x.Sender)
                .WithMany(x => x.SentParcels)
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Parcel>()
                .HasOne(x => x.Receiver)
                .WithMany(x => x.ReceivedParcels)
                .HasForeignKey(x => x.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        
            modelBuilder.Entity<Parcel>()
                .HasOne(x => x.DestLocker)
                .WithMany(x => x.ParcelsDest)
                .HasForeignKey(x => x.DestId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Parcel>()
                .HasOne(x => x.SrcLocker)
                .WithMany(x => x.ParcelsSrc)
                .HasForeignKey(x => x.SrcId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
