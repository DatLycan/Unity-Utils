using System;
using System.Collections.Generic;

namespace DatLycan.Packages.Utils {
    public static class IEnumerableExtensions {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action) {
            foreach (T obj in source)
                action(obj);
            return source;
        }
    }
}
