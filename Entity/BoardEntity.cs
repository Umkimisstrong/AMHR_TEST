namespace Entity
{
  
    /// <summary>
    /// BoardEntity : 게시판 엔티티
    /// </summary>
    public class BoardEntity : GlobalEntity
    {
        /// <summary>
        /// BRD_SEQ : 게시판 SEQ
        /// </summary>
        public string BRD_SEQ {  get; set; }
        /// <summary>
        /// BRD_CATEGORY : 게시판 카테고리
        /// </summary>
        public string BRD_CATEGORY { get; set; }
        /// <summary>
        /// BRD_DIV : 게시판 분류
        /// </summary>
        public string BRD_DIV { get; set; }
        /// <summary>
        /// BRD_TITLE : 게시판 제목
        /// </summary>
        public string BRD_TITLE { get; set; }
        /// <summary>
        /// BRD_CONTENTS : 게시판 내용
        /// </summary>
        public string BRD_CONTENTS { get; set; }
        /// <summary>
        /// BRD_WRITE_ID : 게시판 작성자 ID
        /// </summary>
        public string BRD_WRITE_ID { get; set; }
        /// <summary>
        /// BRD_WRITE_NM : 게시판 작성자 명
        /// </summary>
        public string BRD_WRITE_NM { get; set; }
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

