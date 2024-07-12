namespace Entity
{
    /// <summary>
    /// FileEntity : 파일 엔티티
    /// </summary>
    public class FileEntity : GlobalEntity
    {
        /// <summary>
        /// FILE_SEQ : 파일 SEQ
        /// </summary>
        public long FILE_SEQ { get; set; }

        /// <summary>
        /// FILE_NAME : 파일명
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
        /// FILE_SIZE : 파일 크기
        /// </summary>
        public long FILE_SIZE { get; set; }

        /// <summary>
        /// FILE_CATEGORY : 파일 카테고리
        /// </summary>
        public string FILE_CATEGORY { get; set; }

        /// <summary>
        /// FILE_DIV : 파일 분류
        /// </summary>
        public string FILE_DIV { get; set;}

        /// <summary>
        /// FILE_CATEGORY_SEQ : 파일 카테고리 SEQ
        /// </summary>
        public string FILE_CATEGORY_SEQ { get; set; }

        /// <summary>
        /// USE_YN : 사용여부
        /// </summary>
        public string USE_YN {  get; set; }

        /// <summary>
        /// DEL_YN : 삭제여부
        /// </summary>
        public string DEL_YN { get; set; }

        /// <summary>
        /// CREATE_ID : 생성자 ID
        /// </summary>
        public string CREATE_ID { get; set; }

        /// <summary>
        /// UPDATE_ID : 수정자 ID
        /// </summary>
        public string UPDATE_ID { get; set; }

    }
}
