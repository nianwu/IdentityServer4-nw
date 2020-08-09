using System.Text.RegularExpressions;

namespace utils
{
    public class RegexDic
    {
        /// <summary>
        /// 中文字符
        /// </summary>
        public Regex ChineseChar => new Regex("[\u4e00-\u9fa5]", RegexOptions.Compiled);

        /// <summary>
        /// 全中文字符串
        /// </summary>
        public Regex FullChinese => new Regex($"^{ChineseChar.ToString()}$", RegexOptions.Compiled);
    }
}