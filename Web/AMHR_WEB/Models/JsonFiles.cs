using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMHR_WEB.Models
{
    /// <summary>
    /// JsonFiles : (미사용) Json Return 용으로 만들었으나 미사용
    /// </summary>
    public class JsonFiles
    {
        public UploadFilesResult[] files;
        public string TempFolder { get; set; }
        public JsonFiles(List<UploadFilesResult> filesList)
        {
            files = new UploadFilesResult[filesList.Count];
            for(int i = 0; i < filesList.Count; i++) 
            {
                files[i] = filesList.ElementAt(i);
            }
        }
    }
}