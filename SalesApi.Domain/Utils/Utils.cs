using System.Collections.Generic;
using System.Linq;

namespace SalesApi.Domain.Utils
{
    public static class Utils
    {
        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }
    }
}
