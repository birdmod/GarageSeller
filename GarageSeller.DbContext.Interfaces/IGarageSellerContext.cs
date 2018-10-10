using System.Threading.Tasks;
using GarageSeller.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GarageSeller.Context.Interfaces
{
    public interface IGarageSellerContext
    {
        DbSet<Seller> Sellers { get; set; }
        DbSet<Garage> Garages { get; set; }
        DbSet<Car> Cars { get; set; }
        DbSet<TransmissionType> TransmissionTypes { get; set; }
        DbSet<MotorType> MotorTypes { get; set; }

        Task<int> SaveChangesAsync();
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
