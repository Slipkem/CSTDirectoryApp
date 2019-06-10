using System;
using System.Data.Entity;
using slipkeCST.Models;
using System.Threading.Tasks;

namespace slipkeCST.Test
{
    class TestPersonContext : IPersonContext
    {
        public TestPersonContext()
        {
            this.People = new TestPersonDbSet();
        }

        public DbSet<Person> People { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(0);
        }

        public void MarkAsModified(Person person) { }
        public void Dispose() { }
    }
}
