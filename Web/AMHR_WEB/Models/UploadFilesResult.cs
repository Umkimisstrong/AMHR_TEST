using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMHR_WEB.Models
{
    public class UploadFilesResult
    {
        public string name { get; set; }
        public long size { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string deleteUrl { get; set; }
        public string thumbnailUrl { get; set; }
        public string deleteType { get; set; }
    }
}