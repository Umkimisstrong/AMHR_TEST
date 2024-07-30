using Entity;
using System.Collections.Generic;

namespace Contract
{
    /// <summary>
    /// ProductContract : 상품 Contract
    /// </summary>
    public class ProductContract : GlobalContract
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
        /// PRD_TYPE_NM : 상품 타입 명
        /// </summary>
        public string PRD_TYPE_NM { get; set; }
        /// <summary>
        /// ProductEntity : 상품 엔티티
        /// </summary>
        public ProductEntity ProductEntity { get; set; }
        /// <summary>
        /// ProductList : 상품 엔티티의 집합
        /// </summary>
        public List<ProductEntity> ProductList { get; set; }
        


    }
}
