using Entity;
using System.Collections.Generic;

namespace Contract
{
    /// <summary>
    /// FileContract : 파일 Contract
    /// </summary>
    public class FileContract : GlobalContract
    {
        /// <summary>
        /// FILE_SEQ : 파일 SEQ
        /// </summary>
        public long FILE_SEQ { get; set; }
        /// <summary>
        /// FILE_NAME : 파일 명
        /// </summary>
        public string FILE_NAME { get; set; }
        /// <summary>
        /// FILE_SERVER_NAME : 파일 서버 명
        /// </summary>
        public string FILE_SERVER_NAME { get; set; }
        /// <summary>
        /// FILE_PATH : 파일 경로
        /// </summary>
        public string FILE_PATH { get; set; }
        /// <summary>
        /// FILE_CATEGORY : 파일 카테고리
        /// </summary>
        public string FILE_CATEGORY { get; set; }
        /// <summary>
        /// FILE_DIV : 파일 분류
        /// </summary>
        public string FILE_DIV { get; set; }
        /// <summary>
        /// FILE_CATEGORY_SEQ
        /// </summary>
        public string FILE_CATEGORY_SEQ { get; set; }
        /// <summary>
        /// FileEntity : 파일 엔티티
        /// </summary>
        public FileEntity FileEntity { get; set; }
        /// <summary>
        /// FileList : 파일 엔티티의 집합
        /// </summary>
        public List<FileEntity> FileList { get; set;}
        /// <summary>
        /// UploadFileList : 신규로 업로드할 파일 집합
        /// </summary>
        public List<FileEntity> UploadFileList { get; set; }
        /// <summary>
        /// DeleteFileList : 삭제할 파일 집합
        /// </summary>
        public List<FileEntity> DeleteFileList { get; set; }
        /// <summary>
        /// ExistFileList : 기존에 올라온 파일 집합
        /// </summary>
        public List<FileEntity> ExistFileList { get; set; }
        /// <summary>
        /// UploadFileString : 신규로 업로드할 파일 문자열
        /// </summary>
        public string UploadFileString { get; set; }
        /// <summary>
        /// DeleteFileString : 삭제할 파일 문자열
        /// </summary>
        public string DeleteFileString { get; set; }


        
    }
}
