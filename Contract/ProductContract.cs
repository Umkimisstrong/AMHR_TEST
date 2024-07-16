using Entity;
using System.Collections.Generic;

namespace Contract
{
    /// <summary>
    /// ProductContract : 상품 Contract
    /// </summary>
    public class ProductContract : GlobalContract
    {
        public string PRD_CODE { get; set; }
        public string PRD_CODE_NM { get; set; }
        public string PRD_TYPE_CODE { get; set; }
        public string PRD_TYPE_NM { get; set; }
        public ProductEntity ProductEntity { get; set; }
        public List<ProductEntity> ProductList { get; set; }
        


    }
}
