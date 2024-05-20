namespace Entity
{
    /// <summary>
    /// CodeEntity : 코드 엔티티
    /// </summary>
    public class CodeEntity : GlobalEntity
    {
        /// <summary>
        /// CODE_NM : 코드명
        /// </summary>
        public string CODE_NM { get; set; }
        /// <summary>
        /// SYS_CODE_ID : 시스템 코드 ID
        /// </summary>
        public string SYS_CODE_ID { get; set; }
        /// <summary>
        /// DIV_CODE_ID : 분류 코드 ID
        /// </summary>
        public string DIV_CODE_ID { get; set; }
        /// <summary>
        /// CODE_ID : 코드 ID
        /// </summary>
        public string CODE_ID { get; set; }
        /// <summary>
        /// CODE_DESCRIPTION : 코드 설명
        /// </summary>
        public string CODE_DESCRIPTION { get; set; }
        /// <summary>
        /// USE_YN : 사용여부
        /// </summary>
        public string USE_YN { get; set; }
        /// <summary>
        /// DEL_YN : 삭제여부
        /// </summary>
        public string DEL_YN { get; set; }
        /// <summary>
        /// SORT_ORDER : 정렬순서
        /// </summary>
        public int SORT_ORDER { get; set; }
        /// <summary>
        /// CREATE_ID : 생성자
        /// </summary>
        public string CREATE_ID { get; set; }
        /// <summary>
        /// UPDATE_ID : 수정자
        /// </summary>
        public string UPDATE_ID { get; set; }

        
    }
}
