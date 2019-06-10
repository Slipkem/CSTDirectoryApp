using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using slipkeCST.Models;

namespace slipkeCST.Test
{
    class TestPersonDbSet : TestDbSet<Person>
    {
        public override Person Find(params object[] keyValues)
        {
            return this.SingleOrDefault(person => person.Id == (int)keyValues.Single());
        }

        public override Task<Person> FindAsync(params object[] keyValues)
        {
            return this.SingleOrDefaultAsync(person => person.Id == (int)keyValues.Single());
        }
    }
}
