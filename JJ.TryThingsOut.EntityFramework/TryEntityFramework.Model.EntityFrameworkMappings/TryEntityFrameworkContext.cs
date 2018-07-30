using System.Data.Entity;

namespace TryEntityFramework.Model.EntityFrameworkMappings
{
    public class TryEntityFrameworkContext : DbContext
    {
        public TryEntityFrameworkContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }
    }
}
