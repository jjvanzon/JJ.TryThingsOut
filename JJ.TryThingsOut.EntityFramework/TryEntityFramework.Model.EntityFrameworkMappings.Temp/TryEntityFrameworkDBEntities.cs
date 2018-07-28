using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryEntityFramework.Model.EntityFrameworkMappings.Temp
{
    public class TryEntityFrameworkDBEntities : DbContext
    {
        public TryEntityFrameworkDBEntities(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public DbSet<Thing> Things { get; set; }
    }
}
