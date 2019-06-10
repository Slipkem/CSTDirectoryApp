using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace slipkeCST.Models
{
    public interface IPersonContext : IDisposable
    {
        DbSet<Person> People { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        
        void MarkAsModified(Person person);
        
    }
}
