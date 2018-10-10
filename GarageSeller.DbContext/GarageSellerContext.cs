using System.Threading.Tasks;
using GarageSeller.Context.Interfaces;
using GarageSeller.Models;
using Microsoft.EntityFrameworkCore;

namespace GarageSeller.Context
{
    public class GarageSellerContext : DbContext, IGarageSellerContext
    {
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Garage> Garages { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }
        public DbSet<MotorType> MotorTypes { get; set; }

        public GarageSellerContext(DbContextOptions<GarageSellerContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seller>().ToTable(nameof(Seller));
            modelBuilder.Entity<Garage>().ToTable(nameof(Garage));
            modelBuilder.Entity<Car>().ToTable(nameof(Car));
            modelBuilder.Entity<TransmissionType>().ToTable(nameof(TransmissionType));
            modelBuilder.Entity<MotorType>().ToTable(nameof(MotorType));
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
