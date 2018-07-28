using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryEntityFramework.Model
{
    public class Thing
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
    }
}
