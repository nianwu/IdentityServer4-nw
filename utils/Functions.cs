using System;
namespace utils
{
    public class Functions
    {
        private RegexDic _regexDic;

        public Functions(RegexDic regexDic)
        {
            _regexDic = regexDic;
        }

        public double ProportionOfChinese(string content)
        {
            var chineseCharLength = _regexDic.ChineseChar.Matches(content).Count;
            var chineseLength = chineseCharLength * 2;
            var length = content.Length + chineseCharLength;
            return Math.Round(chineseLength / (double)length, 4);
        }
    }
}