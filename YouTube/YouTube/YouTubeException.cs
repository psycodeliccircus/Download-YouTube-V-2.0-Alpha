using System;


namespace YouTube
{
    public class YouTubeFormatException : Exception
    {
        public YouTubeFormatException()
        { }

        public YouTubeFormatException(string message)
            : base(message)
        { }
    }

    public class VideoNotAvailableException : Exception
    {
        public VideoNotAvailableException()
        { }

        public VideoNotAvailableException(string message)
            : base(message)
        { }
    }

    public class YoutubeParseException : Exception
    {
        public YoutubeParseException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    public class AudioExtractionException : Exception
    {
        public AudioExtractionException(string message)
            : base(message)
        { }
    }
}