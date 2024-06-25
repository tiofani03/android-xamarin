using System;
using SQLite;

namespace productDemo.Data.Chucker.api.model
{
    public class Traffic
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public string BaseUrl { get; set; }
        public string FullUrl { get; set; }
        public int StatusCode { get; set; }
        public string Response { get; set; }
        public long TookMs { get; set; }
        public string ResponseDate { get; set; }
        public DateTime RequestDate { get; set; }
    }
}