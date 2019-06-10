using System;
using System.Data.Entity;

namespace slipkeCST.Models
{
    public class PersonContext : DbContext, IPersonContext
    {
        public PersonContext() : base("name=PersonContext")
        {
            Database.SetInitializer<PersonContext>(new DropCreateDatabaseAlways<PersonContext>()); 
        }

        public DbSet<slipkeCST.Models.Person> People { get; set; }

        public void MarkAsModified(Person person)
        {
            Entry(person).State = EntityState.Modified;
        }
    }
}
