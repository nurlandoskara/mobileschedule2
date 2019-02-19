using System;
using System.ComponentModel;
using System.Linq;

namespace MobileSchedule2.Enums
{
    public static class EnumHelper
    {
        public static string Description(Enum eValue)
        {
            if (eValue == null)
                return string.Empty;
            var nAttributes = eValue.GetType().GetField(eValue.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return nAttributes.Any() ? (nAttributes.First() as DescriptionAttribute)?.Description : string.Empty;
        }
    }
}
