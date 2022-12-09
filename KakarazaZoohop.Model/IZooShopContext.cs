using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace KakarazaZoohop.Model
{
    public interface IZooShopContext : IDisposable
    {
        DbSet<Patient> Patients { get; set; }

        DbSet<Owner> Owners { get; set; }

        DbSet<VisitRecord> VisitRecords { get; set; }

        DatabaseFacade Database { get; }

        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}