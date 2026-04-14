using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
