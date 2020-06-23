using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Entity : DataErrorInfo, ICloneable
    {
        public Entity()
        {

        }

        public int Id { get; set; }

        public abstract object Clone();
    }
}
