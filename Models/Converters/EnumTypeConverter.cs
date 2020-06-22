using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Properties;

namespace Models.Converters
{
    public class EnumTypeConverter : EnumConverter
    {
        public EnumTypeConverter(Type type) : base(type)
        {
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if(destinationType == typeof(string))
            {
                if(value != null)
                {
                    return Resources.ResourceManager.GetString(value.ToString());
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
