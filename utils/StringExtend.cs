using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
namespace utils
{
    public static class StringExtend
    {
        private static RegexDic _regex = new RegexDic();

        public static int LooksLength(this string e)
        {
            // return Encoding.Default.GetBytes(e).Length;
            return e.Length + _regex.ChineseChar.Matches(e).Count;
        }
    }
}