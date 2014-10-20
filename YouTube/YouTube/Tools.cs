using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;


namespace YouTube
{
    public class Tools
    {
        private const int CorrectSignatureLength = 81;
        private const string SignatureQuery = "signature";

        public static bool VideoID(string value, out string videoid)
        {
            videoid = Regex.Match(value, @"^(?:https?\:\/\/)?(?:www\.)?(?:youtu\.be\/|youtube\.com\/(?:embed\/|v\/|watch\?v\=))([\w-]{10,12})(?:$|\&|\?\#).*").Groups[1].Value;
            return string.IsNullOrEmpty(videoid) ? false : true;
        }

        public static bool VideoLink(string value)
        {
            if (Regex.IsMatch(value, @"^(?:https?\:\/\/)?(?:www\.)?(?:youtu\.be\/|youtube\.com\/(?:embed\/|v\/|watch\?v\=))([\w-]{10,12})(?:[\&\?\#].*?)*?(?:[\&\?\#]t=([\dhm]+s))?$"))
                return true;
            else
                return false;
        }

        public static string SetTime(string value)
        {
            int hours = TimeSpan.FromSeconds(double.Parse(value)).Hours;
            int minutes = TimeSpan.FromSeconds(double.Parse(value)).Minutes;
            int seconds = TimeSpan.FromSeconds(double.Parse(value)).Seconds;

            string timevalue = string.Empty;

            if (hours > 0) timevalue = string.Format("{0}:{1}:{2}", hours, minutes, seconds);
            if (hours == 0 && minutes > 0) timevalue = string.Format("{0}:{1}", minutes.ToString("00"), seconds.ToString("00"));
            if (hours == 0 && minutes == 0 & seconds > 0) timevalue = string.Format("00:{0}", seconds.ToString("00"));

            return timevalue;
        }

        public static string DownloadString(string value)
        {
            using (var client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                return client.DownloadString(value);
            }
        }

        public static bool IsVideoUnavailable(string pageSource)
        {
            const string unavailableContainer = "<div id=\"watch-player-unavailable\">";
            return pageSource.Contains(unavailableContainer);
        }

        public static string UrlDecode(string value)
        {
            return HttpUtility.UrlDecode(value);
        }

        public static IDictionary<string, string> ParseQueryString(string value)
        {
            if (value.Contains("?"))
            {
                value = value.Substring(value.IndexOf('?') + 1);
            }

            var dictionary = new Dictionary<string, string>();

            foreach (string vp in Regex.Split(value, "&"))
            {
                string[] strings = Regex.Split(vp, "=");
                dictionary.Add(strings[0], strings.Length == 2 ? UrlDecode(strings[1]) : string.Empty);
            }

            return dictionary;
        }

        public static void DecryptDownloadUrl(VideoInfo videoInfo)
        {
            IDictionary<string, string> queries = ParseQueryString(videoInfo.DownloadUrl);

            if (queries.ContainsKey(SignatureQuery))
            {
                string encryptedSignature = queries[SignatureQuery];
                string decrypted;

                decrypted = GetDecipheredSignature(videoInfo.HtmlPlayerVersion, encryptedSignature);

                videoInfo.DownloadUrl = ReplaceQueryStringParameter(videoInfo.DownloadUrl, SignatureQuery, decrypted);
                videoInfo.RequiresDecryption = false;
            }
        }

        public static string GetDecipheredSignature(string htmlPlayerVersion, string signature)
        {
            if (signature.Length == CorrectSignatureLength)
            {
                return signature;
            }

            return DecipherWithVersion(signature, htmlPlayerVersion);
        }

        private static string ReplaceQueryStringParameter(string currentPageUrl, string paramToReplace, string newValue)
        {
            var query = ParseQueryString(currentPageUrl);

            query[paramToReplace] = newValue;

            var resultQuery = new StringBuilder();
            bool isFirst = true;

            foreach (KeyValuePair<string, string> pair in query)
            {
                if (!isFirst)
                {
                    resultQuery.Append("&");
                }

                resultQuery.Append(pair.Key);
                resultQuery.Append("=");
                resultQuery.Append(pair.Value);

                isFirst = false;
            }

            var uriBuilder = new UriBuilder(currentPageUrl)
            {
                Query = resultQuery.ToString()
            };

            return uriBuilder.ToString();
        }

        public static string DecipherWithVersion(string cipher, string cipherVersion)
        {
            string jsUrl = string.Format("http://s.ytimg.com/yts/jsbin/html5player-{0}.js", cipherVersion);
            string js = DownloadString(jsUrl);

            string functNamePattern = @"\.sig\s*\|\|(\w+)\(";
            var funcName = Regex.Match(js, functNamePattern).Groups[1].Value;

            string funcBodyPattern = @"(?<brace>{([^{}]| ?(brace))*})";
            string funcPattern = string.Format(@"{0}\(\w+\){1}", funcName, funcBodyPattern);
            var funcBody = Regex.Match(js, funcPattern).Groups["brace"].Value;

            var lines = funcBody.Split(';');
            string operations = "";
            foreach (var line in lines.Skip(1).Take(lines.Length - 2))
            {
                Match m;
                if ((m = Regex.Match(line, @"\(\w+,(?<index>\d+)\)")).Success)
                    operations += "w" + m.Groups["index"].Value + " ";
                else if ((m = Regex.Match(line, @"slice\((?<index>\d+)\)")).Success)
                    operations += "s" + m.Groups["index"].Value + " ";
                else if ((m = Regex.Match(line, @"reverse\(\)")).Success)
                    operations += "r ";
            }
            operations = operations.Trim();

            return DecipherWithOperations(cipher, operations);
        }

        private static string DecipherWithOperations(string cipher, string operations)
        {
            return operations.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(cipher, ApplyOperation);
        }

        private static string ApplyOperation(string cipher, string op)
        {
            switch (op[0])
            {
                case 'r':
                    return new string(cipher.ToCharArray().Reverse().ToArray());

                case 'w':
                    {
                        int index = GetOpIndex(op);
                        return SwapFirstChar(cipher, index);
                    }

                case 's':
                    {
                        int index = GetOpIndex(op);
                        return cipher.Substring(index);
                    }

                default:
                    throw new NotImplementedException("Couldn't find cipher operation.");
            }
        }

        private static int GetOpIndex(string op)
        {
            string parsed = new Regex(@".(\d+)").Match(op).Result("$1");
            int index = Int32.Parse(parsed);

            return index;
        }

        private static string SwapFirstChar(string cipher, int index)
        {
            var builder = new StringBuilder(cipher);
            builder[0] = cipher[index];
            builder[index] = cipher[0];

            return builder.ToString();
        }
    }
}