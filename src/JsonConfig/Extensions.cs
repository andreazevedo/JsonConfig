using System.Threading;

namespace JsonConfig
{
    internal static class Extensions
    {
        internal static string UpperCaseFirstChar(this string str)
        {
            if (str.Length == 0) return str;

            var firstCharUpperCased = str[0].ToUpper();
            if (str.Length > 1)
            {
                return firstCharUpperCased + str.Substring(1);
            }
            else
            {
                return firstCharUpperCased.ToString(Thread.CurrentThread.CurrentCulture);
            }
        }

        internal static char ToUpper(this char chr)
        {
            return chr.ToString(Thread.CurrentThread.CurrentCulture).ToUpper()[0];
        }
    }
}
