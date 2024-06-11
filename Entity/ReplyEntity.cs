namespace Entity
{
  
    /// <summary>
    /// ReplyEntity : 댓글 엔티티
    /// </summary>
    public class ReplyEntity : GlobalEntity
    {
        /// <summary>
        /// REPLY_SEQ : 댓글 SEQ
        /// </summary>
        public int REPLY_SEQ { get; set; }
        /// <summary>
        /// REPLY_PARENT_SEQ : 댓글 상위 SEQ
        /// </summary>
        public int REPLY_PARENT_SEQ { get; set; }
        /// <summary>
        /// BRD_SEQ : 게시판 SEQ
        /// </summary>
        public string BRD_SEQ { get; set; }
        /// <summary>
        /// BRD_CATEGORY : 게시판 카테고리
        /// </summary>
        public string BRD_CATEGORY { get; set; }
        /// <summary>
        /// BRD_DIV : 게시판 분류
        /// </summary>
        public string BRD_DIV { get; set; }
        /// <summary>
        /// REPLY_COMMENTS : 댓글 내용
        /// </summary>
        public string REPLY_COMMENTS { get; set; }
        /// <summary>
        /// REPLY_WRITE_ID : 댓글 작성자 ID
        /// </summary>
        public string REPLY_WRITE_ID { get; set; }
        /// <summary>
        /// REPLY_WRITE_NM : 댓글 작성자 명
        /// </summary>
        public string REPLY_WRITE_NM { get; set; }
        /// <summary>
        /// USE_YN : 사용여부
        /// </summary>
        public string USE_YN { get; set; }
        /// <summary>
        /// DEL_YN : 삭제여부
        /// </summary>
        public string DEL_YN { get; set; }
        /// <summary>
        /// CREATE_ID : 생성자
        /// </summary>
        public string CREATE_ID { get; set; }
        /// <summary>
        /// UPDATE_ID : 수정자
        /// </summary>
        public string UPDATE_ID { get; set;}

        
    }
}

