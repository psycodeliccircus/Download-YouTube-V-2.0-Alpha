using System.Collections.Generic;

namespace YouTube
{
    public class VideoInfo
    {
        internal static IEnumerable<VideoInfo> Defaults = new List<VideoInfo>
        {
            //3GP
            new VideoInfo(13, VideoType.Mobile, 0, false, AudioType.Aac, 0, AdaptiveType.None),
            new VideoInfo(17, VideoType.Mobile, 144, false, AudioType.Aac, 24, AdaptiveType.None),
            new VideoInfo(36, VideoType.Mobile, 240, false, AudioType.Aac, 38, AdaptiveType.None),

            //FLV
            new VideoInfo(5, VideoType.Flash, 240, false, AudioType.Mp3, 64, AdaptiveType.None),
            new VideoInfo(6, VideoType.Flash, 270, false, AudioType.Mp3, 64, AdaptiveType.None),
            new VideoInfo(34, VideoType.Flash, 360, false, AudioType.Aac, 128, AdaptiveType.None),
            new VideoInfo(35, VideoType.Flash, 480, false, AudioType.Aac, 128, AdaptiveType.None),
            new VideoInfo(120, VideoType.Flash, 720, true, AudioType.Aac, 128, AdaptiveType.None),

            //MP4
            new VideoInfo(18, VideoType.Mp4, 360, false, AudioType.Aac, 96, AdaptiveType.None),
            new VideoInfo(22, VideoType.Mp4, 720, false, AudioType.Aac, 192, AdaptiveType.None),
            new VideoInfo(37, VideoType.Mp4, 1080, false, AudioType.Aac, 192, AdaptiveType.None),
            new VideoInfo(38, VideoType.Mp4, 3072, false, AudioType.Aac, 192, AdaptiveType.None),
            new VideoInfo(82, VideoType.Mp4, 360, true, AudioType.Aac, 96, AdaptiveType.None),
            new VideoInfo(83, VideoType.Mp4, 240, true, AudioType.Aac, 96, AdaptiveType.None),
            new VideoInfo(84, VideoType.Mp4, 720, true, AudioType.Aac, 152, AdaptiveType.None),
            new VideoInfo(85, VideoType.Mp4, 520, true, AudioType.Aac, 152, AdaptiveType.None),

            //WebM
            new VideoInfo(43, VideoType.WebM, 360, false, AudioType.Vorbis, 128, AdaptiveType.None),
            new VideoInfo(44, VideoType.WebM, 480, false, AudioType.Vorbis, 128, AdaptiveType.None),
            new VideoInfo(45, VideoType.WebM, 720, false, AudioType.Vorbis, 192, AdaptiveType.None),
            new VideoInfo(46, VideoType.WebM, 1080, false, AudioType.Vorbis, 192, AdaptiveType.None),
            new VideoInfo(100, VideoType.WebM, 360, true, AudioType.Vorbis, 128, AdaptiveType.None),
            new VideoInfo(101, VideoType.WebM, 360, true, AudioType.Vorbis, 192, AdaptiveType.None),
            new VideoInfo(102, VideoType.WebM, 720, true, AudioType.Vorbis, 192, AdaptiveType.None),
        };

        internal VideoInfo(int formatCode)
            : this(formatCode, VideoType.Unknown, 0, false, AudioType.Unknown, 0, AdaptiveType.None)
        { }

        internal VideoInfo(VideoInfo info)
            : this(info.FormatCode, info.VideoType, info.Resolution, info.Is3D, info.AudioType, info.AudioBitrate, info.AdaptiveType)
        { }

        private VideoInfo(int formatCode, VideoType videoType, int resolution, bool is3D, AudioType audioType, int audioBitrate, AdaptiveType adaptiveType)
        {
            this.FormatCode = formatCode;
            this.VideoType = videoType;
            this.Resolution = resolution;
            this.Is3D = is3D;
            this.AudioType = audioType;
            this.AudioBitrate = audioBitrate;
            this.AdaptiveType = adaptiveType;
        }

        public AdaptiveType AdaptiveType { get; private set; }

        public int AudioBitrate { get; private set; }

        public string AudioExtension
        {
            get
            {
                switch (this.AudioType)
                {
                    case AudioType.Aac:
                        return ".aac";

                    case AudioType.Mp3:
                        return ".mp3";

                    case AudioType.Vorbis:
                        return ".ogg";
                }

                return null;
            }
        }

        public AudioType AudioType { get; private set; }

        public bool CanExtractAudio
        {
            get { return this.VideoType == VideoType.Flash; }
        }

        public string DownloadUrl { get; internal set; }

        public int FormatCode { get; private set; }

        public bool Is3D { get; private set; }

        public bool RequiresDecryption { get; internal set; }

        public int Resolution { get; private set; }

        public string Title { get; internal set; }

        public string VideoExtension
        {
            get
            {
                switch (this.VideoType)
                {
                    case VideoType.Mp4:
                        return ".mp4";

                    case VideoType.Mobile:
                        return ".3gp";

                    case VideoType.Flash:
                        return ".flv";

                    case VideoType.WebM:
                        return ".webm";
                }

                return null;
            }
        }

        public VideoType VideoType { get; private set; }

        internal string HtmlPlayerVersion { get; set; }

        public override string ToString()
        {
            string result = string.Empty;

            switch (this.FormatCode)
            {
                //3GP
                case 13:
                    result = "3GP MPEG-4 Visual 0.5 AAC";
                    break;
                case 17:
                    result = "3GP 144p MPEG-4 Visual Simple 0.05 AAC 24k";
                    break;
                case 36:
                    result = "3GP 240p MPEG-4 Visual Simple 0.17 AAC 38k";
                    break;

                //FLV
                case 5:
                    result = "FLV 240p Sorenson H.263 0.25 MP3 64k";
                    break;
                case 6:
                    result = "FLV 270p Sorenson H.263 0.8 MP3 64k";
                    break;
                case 34:
                    result = "FLV 360p H.264 Main 0.5 AAC 128k";
                    break;
                case 35:
                    result = "FLV 480p H.264 Main 0.8-1 AAC 128k";
                    break;
                case 120:
                    result = "FLV 720p AVC Main@L3.1 2 AAC 128k";
                    break;

                //MP4
                case 18:
                    result = "MP4 360p H.264 Baseline 0.5 AAC 96k";
                    break;
                case 22:
                    result = "MP4 720p H.264 High 2-2.9 AAC 192k";
                    break;
                case 37:
                    result = "MP4 1080p H.264 High 3–4.3 AAC 192k";
                    break;
                case 38:
                    result = "MP4 3072p H.264 High 3.5-5 AAC 192k";
                    break;
                case 82:
                    result = "MP4 360p H.264 3D 0.5 AAC 96k";
                    break;
                case 83:
                    result = "MP4 240p H.264 3D 0.5 AAC 96k";
                    break;
                case 84:
                    result = "MP4 720p H.264 3D 2-2.9 AAC 152k";
                    break;
                case 85:
                    result = "MP4 520p H.264 3D 2-2.9 AAC 152k";
                    break;

                //WebM
                case 43:
                    result = "WebM 360p VP8 0.5 Vorbis 128k";
                    break;
                case 44:
                    result = "WebM 480p VP8 1 Vorbis 128k";
                    break;
                case 45:
                    result = "WebM 720p VP8 2 Vorbis 192k";
                    break;
                case 46:
                    result = "WebM 1080p VP8 Vorbis 192k";
                    break;
                case 100:
                    result = "WebM 360p VP8 3D Vorbis 128k";
                    break;
                case 101:
                    result = "WebM 360p VP8 3D Vorbis 192k";
                    break;
                case 102:
                    result = "WebM 720p VP8 3D Vorbis 192k";
                    break;
            }

            return result;
        }
    }
}