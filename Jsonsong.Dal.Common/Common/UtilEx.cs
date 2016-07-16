using System.Collections.Generic;
using System.Linq;
using Jsonsong.Dal.Common.MongoDb;

namespace Jsonsong.Dal.Common.Common
{
    public static class UtilEx
    {
        public static bool HasValue<T>(this IEnumerable<T> enumerable)
        {
            return enumerable != null && !enumerable.Any();
        }

        public static bool HasValue(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }
    }
}