using System;
using System.Collections.Generic;

namespace ComradeVanti.EnumDict
{

    internal static class EnumUtil
    {

        public static IEnumerable<TEnum> GetEnumValues<TEnum>() => 
            (TEnum[])Enum.GetValues(typeof(TEnum));

    }

}