using Entity;
using System.Collections.Generic;

namespace Contract
{
    /// <summary>
    /// ClassContract : 클래스 Contract
    /// </summary>
    public class ClassContract : GlobalContract
    {
        /// <summary>
        /// CLASS_NO : 클래스 번호
        /// </summary>
        public string CLASS_NO {  get; set; }
        /// <summary>
        /// PRD_CODE : 상품 코드
        /// </summary>
        public string PRD_CODE { get; set; }
        public string PRD_TYPE_NM { get; set; }

        public string PRD_PRICE { get; set; }
        /// <summary>
        /// CLASS_USER_ID : 클래스 사용자 ID
        /// </summary>
        public string CLASS_USER_ID { get; set; }
        /// <summary>
        /// CLASS_YMD : 클래스 수강 연월일
        /// </summary>
        public string CLASS_YMD { get; set; }
        public string CLASS_TIME { get; set; }
        /// <summary>
        /// ClassEntity : 클래스 엔티티
        /// </summary>
        public ClassEntity ClassEntity { get; set; }
        /// <summary>
        /// ClassList : 클래스 엔티티 집합
        /// </summary>
        public List<ClassEntity> ClassList { get;}

    }
}
