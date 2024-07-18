using System.Linq;

namespace DatLycan.Packages.Utils {
    public static class StringExtensions {
        public static int ComputeFNV1aHash(this string @string) {
            uint hash = @string.Aggregate(2166136261, (current, c) => (current ^ c) * 16777619);
            return unchecked((int)hash);
        }
    }
}
