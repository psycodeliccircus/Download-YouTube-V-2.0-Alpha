using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace YouTube
{
    public class DownloadUrlResolver
    {
        public string Title { get; set; }
        public string VideoLength { get; set; }

        private const int CorrectSignatureLength = 81;
        private const string SignatureQuery = "signature";

        public void PopulateVideoInfo(string value,out IEnumerable<VideoInfo> infocollection)
        {
            bool result = false;
            infocollection = null;

            string videoURL = value;
            string videoID = string.Empty;

            result = YouTube.Tools.VideoID(videoURL, out videoID);
            if (!result) return;

            if (YouTube.Tools.VideoLink(videoURL))
            {
                var json = LoadJson(videoURL);

                JToken title = json["args"]["title"];
                Title = title == null ? string.Empty : title.ToString();

                JToken length = json["args"]["length_seconds"];
                VideoLength = YouTube.Tools.SetTime(length == null ? "0" : length.ToString());

                infocollection = GetDownloadUrls(json);
            }
            else
                throw new YouTubeFormatException("Invalid YouTube URL.");
        }

        private IEnumerable<VideoInfo> GetDownloadUrls(JObject value, bool decryptSignature = true)
        {
            try
            {
                IEnumerable<ExtractionInfo> downloadUrls = ExtractDownloadUrls(value);
                IEnumerable<VideoInfo> infos = GetVideoInfos(downloadUrls).ToList();

                string htmlPlayerVersion = GetHtml5PlayerVersion(value);

                foreach (VideoInfo info in infos)
                {
                    info.HtmlPlayerVersion = htmlPlayerVersion;

                    if (decryptSignature && info.RequiresDecryption)
                        YouTube.Tools.DecryptDownloadUrl(info);
                }

                return infos;
            }
            catch (Exception ex)
            {
                if (ex is WebException || ex is VideoNotAvailableException)
                    throw;

                throw new YoutubeParseException("Could not parse the Youtube page.\n" +
                                                "This may be due to a change of the YouTube page structure.\n" +
                                                "Please report this bug at => krsolucoesweb@gmail.com", ex);
            }
        }

        private JObject LoadJson(string value)
        {
            string pageSource = YouTube.Tools.DownloadString(value);

            if (YouTube.Tools.IsVideoUnavailable(pageSource))
                throw new VideoNotAvailableException();

            var dataRegex = new Regex(@"ytplayer\.config\s*=\s*(\{.+?\});", RegexOptions.Multiline);

            string extractedJson = dataRegex.Match(pageSource).Result("$1");

            return JObject.Parse(extractedJson);
        }

        private string GetStreamMap(JObject value)
        {
            JToken streamMap = value["args"]["url_encoded_fmt_stream_map"];
            string streamMapString = streamMap == null ? null : streamMap.ToString();

            if (streamMapString == null || streamMapString.Contains("been+removed"))
                throw new VideoNotAvailableException("Video has been removed or has an age restricted.");

            return streamMapString;
        }

        private string GetAdaptiveMap(JObject value)
        {
            JToken streamMap = value["args"]["adaptive_fmts"];
            return streamMap.ToString();
        }

        private IEnumerable<ExtractionInfo> ExtractDownloadUrls(JObject value)
        {
            string[] splitByUrls = GetStreamMap(value).Split(',');
            string[] adaptiveFmtSplitByUrls = GetAdaptiveMap(value).Split(',');
            splitByUrls = splitByUrls.Concat(adaptiveFmtSplitByUrls).ToArray();

            foreach (string s in splitByUrls)
            {
                IDictionary<string, string> queries = YouTube.Tools.ParseQueryString(s);
                string url;

                bool requiresDecryption = false;

                if (queries.ContainsKey("s") || queries.ContainsKey("sig"))
                {
                    requiresDecryption = queries.ContainsKey("s");
                    string signature = queries.ContainsKey("s") ? queries["s"] : queries["sig"];

                    url = string.Format("{0}&{1}={2}", queries["url"], SignatureQuery, signature);

                    string fallbackHost = queries.ContainsKey("fallback_host") ? "&fallback_host=" + queries["fallback_host"] : String.Empty;

                    url += fallbackHost;
                }

                else
                {
                    url = queries["url"];
                }

                url = YouTube.Tools.UrlDecode(url);
                url = YouTube.Tools.UrlDecode(url);

                yield return new ExtractionInfo { RequiresDecryption = requiresDecryption, Uri = new Uri(url) };
            }
        }

        private IEnumerable<VideoInfo> GetVideoInfos(IEnumerable<ExtractionInfo> extractionInfos)
        {
            var downLoadInfos = new List<VideoInfo>();

            foreach (ExtractionInfo extractionInfo in extractionInfos)
            {
                string itag = YouTube.Tools.ParseQueryString(extractionInfo.Uri.Query)["itag"];

                int formatCode = int.Parse(itag);

                VideoInfo info = VideoInfo.Defaults.SingleOrDefault(videoInfo => videoInfo.FormatCode == formatCode);

                if (info != null)
                {
                    info = new VideoInfo(info)
                    {
                        DownloadUrl = extractionInfo.Uri.ToString(),
                        Title = this.Title,
                        RequiresDecryption = extractionInfo.RequiresDecryption
                    };
                }
                else
                {
                    info = new VideoInfo(formatCode)
                    {
                        DownloadUrl = extractionInfo.Uri.ToString()
                    };
                }

                downLoadInfos.Add(info);
            }

            return downLoadInfos;
        }

        private string GetHtml5PlayerVersion(JObject value)
        {
            var regex = new Regex(@"html5player-(.+?)\.js");
            string js = value["assets"]["js"].ToString();

            return regex.Match(js).Result("$1");
        }
    }
}