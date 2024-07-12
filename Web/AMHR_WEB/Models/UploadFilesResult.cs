﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMHR_WEB.Models
{
    /// <summary>
    /// UploadFilesResult : 업로드파일형식 클래스(FileEntity 와 동일형식)
    /// </summary>
    public class UploadFilesResult
    {
        public long FILE_SEQ { get; set; }

        public string FILE_NAME { get; set; }

        public string FILE_SERVER_NAME { get; set; }

        public string FILE_PATH { get; set; }

        public long FILE_SIZE { get; set; }

        public string FILE_CATEGORY { get; set; }

        public string FILE_DIV { get; set; }

        public string FILE_CATEGORY_SEQ { get; set; }

        public string USE_YN { get; set; }

        public string DEL_YN { get; set; }

        public string CREATE_ID { get; set; }

        public string UPDATE_ID { get; set; }
    }
}