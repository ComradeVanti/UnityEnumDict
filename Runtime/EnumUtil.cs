using System;
using System.Collections.Generic;

namespace ComradeVanti.EnumDict
{

    public static class EnumUtil
    {

        public static IEnumerable<TEnum> GetEnumValues<TEnum>() => 
            (TEnum[])Enum.GetValues(typeof(TEnum));

    }

}