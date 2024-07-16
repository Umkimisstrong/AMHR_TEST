namespace Entity
{
    /// <summary>
    /// ProductEntity : 상품 엔티티
    /// </summary>
    public class ProductEntity : GlobalEntity
    {
        public string PRD_CODE { get; set; }
        public string PRD_CODE_NM { get; set; }
        public string PRD_TYPE_CODE { get; set; }
        public string PRD_TYPE_NM { get; set; }
        public string PRD_PRICE { get; set; }
        public string USE_YN { get; set; }
        public string DEL_YN { get; set; }
        public string CREATE_ID { get; set; }
        public string UPDATE_ID { get; set; }

    }
}
