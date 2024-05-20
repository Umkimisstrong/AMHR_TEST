using Entity;
using System.Collections.Generic;

namespace Contract
{
    /// <summary>
    /// CodeContract : 코드 Contract 
    /// </summary>
    public class CodeContract : GlobalContract
    {
        /// <summary>
        /// SYS_CODE_ID : 시스템 코드 ID
        /// </summary>
        public string SYS_CODE_ID {  get; set; }
        /// <summary>
        /// DIV_CODE_ID : 분류 코드 ID
        /// </summary>
        public string DIV_CODE_ID { get; set; }
        /// <summary>
        /// CODE_ID : 코드 ID
        /// </summary>
        public string CODE_ID { get; set; }
        /// <summary>
        /// CODE_NM : 코드 명칭
        /// </summary>
        public string CODE_NM { get; set; }
        /// <summary>
        /// CodeEntity : 코드엔티티(1개의 코드 테이블)
        /// </summary>
        public CodeEntity CodeEntity { get; set; }
        /// <summary>
        /// CodeList : 코드엔티티의 집합
        /// </summary>
        public List<CodeEntity> CodeList { get; set; }


    }
}
