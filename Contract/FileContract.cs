using Entity;
using System.Collections.Generic;

namespace Contract
{
    /// <summary>
    /// FileContract : 파일 Contract
    /// </summary>
    public class FileContract : GlobalContract
    {
        public long FILE_SEQ { get; set; }

        public string FILE_NAME { get; set; }

        public string FILE_SERVER_NAME { get; set; }

        public string FILE_PATH { get; set; }

        public string FILE_CATEGORY { get; set; }

        public string FILE_DIV { get; set; }

        public string FILE_CATEGORY_SEQ { get; set; }

        public FileEntity FileEntity { get; set; }

        public List<FileEntity> FileList { get; set;}
        
    }
}
