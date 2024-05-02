using System;
namespace Doczy.Business.DTOs.VideoDtos
{
    public class ZoomApiResponseDto
    {
        public int code { get; set; }
        public Message message { get; set; }
    }

    public class Message
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public ResponseData response { get; set; }
    }

    public class ResponseData
    {
        public string join_url { get; set; }
    }
}

