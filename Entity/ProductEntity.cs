namespace Entity
{
    /// <summary>
    /// ProductEntity : 상품 엔티티
    /// </summary>
    public class ProductEntity : GlobalEntity
    {
        /// <summary>
        /// PRD_CODE : 상품 코드
        /// </summary>
        public string PRD_CODE { get; set; }
        /// <summary>
        /// PRD_CODE_NM : 상품 코드명
        /// </summary>
        public string PRD_CODE_NM { get; set; }
        /// <summary>
        /// PRD_TYPE_CODE : 상품 타입 코드
        /// </summary>
        public string PRD_TYPE_CODE { get; set; }
        /// <summary>
        /// PRD_TYPE_NM : 상품 타입명
        /// </summary>
        public string PRD_TYPE_NM { get; set; }
        /// <summary>
        /// PRD_PRICE : 상품 가격
        /// </summary>
        public string PRD_PRICE { get; set; }
        /// <summary>
        /// USE_YN : 사용여부
        /// </summary>
        public string USE_YN { get; set; }
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
