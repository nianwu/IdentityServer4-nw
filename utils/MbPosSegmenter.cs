using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using JiebaNet.Segmenter;
using JiebaNet.Segmenter.PosSeg;
using Newtonsoft.Json;

namespace utils
{
    public class MbPosSegmenter : PosSegmenter
    {
        private string[] _stopWords;

        private string[] StopWords => _stopWords ?? (_stopWords = File.ReadAllLines(Path.GetFullPath(ConfigManager.StopWordsFile)));

        public MbPosSegmenter()
        {
        }

        public MbPosSegmenter Reload()
        {
            _stopWords = File.ReadAllLines(Path.GetFullPath(ConfigManager.StopWordsFile));
            return this;
        }

        public IEnumerable<Pair> CutAndRemoveStopWords(string content, bool hmm = true)
        {
            if (string.IsNullOrEmpty(content))
            {
                return new Pair[0];
            }

            var result = Cut(content, hmm);

            result = result.Where(x => !StopWords.Contains(x.Word)).ToList();

            return result;
        }

        private string ComputedMd5(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return null;
            }

            return BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(content))).ToLower().Replace("-", string.Empty);
        }

        public string SegmentMd5(string content)
        {
            string result = null;

            if (string.IsNullOrEmpty(content))
            {
                return result;
            }

            if (string.IsNullOrEmpty(content))
            {
                return result;
            }

            var words = CutAndRemoveStopWords(content)
                // m:数词, x:字符串 详情参考 https://gist.github.com/luw2007/6016931
                .Where(x =>
                    !x.Flag.Contains("m")
                    && !x.Flag.Contains("x")
                )
                .Select(x => x.Word)
                .ToList();

            if (words is null || !words.Any())
            {
                return result;
            }

            result = JsonConvert.SerializeObject(words);

            result = ComputedMd5(result);

            return result;
        }
    }
}