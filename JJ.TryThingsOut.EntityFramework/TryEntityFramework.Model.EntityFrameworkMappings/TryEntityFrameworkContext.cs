using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryEntityFramework.Model.EntityFrameworkMappings
{
    public class TryEntityFrameworkContext : DbContext
    {
        public TryEntityFrameworkContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }
    }
}
