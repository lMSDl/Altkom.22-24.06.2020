using Models.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [TypeConverter(typeof(EnumTypeConverter))]
    public enum Specialization
    {
        Math,
        Physics,
        Chemistry,
        Biology,
        Sociology,
        Psychology
    }
}
