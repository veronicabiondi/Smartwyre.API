using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Smartwyre.DeveloperTest.Data
{
    public interface IDatabaseContext
    {
        DbSet<T> GetDataSet<T>() where T:class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}