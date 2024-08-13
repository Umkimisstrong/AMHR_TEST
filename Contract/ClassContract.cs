using Entity;
using System.Collections.Generic;

namespace Contract
{
    /// <summary>
    /// ClassContract : 클래스 Contract
    /// </summary>
    public class ClassContract : GlobalContract
    {
        public string CLASS_NO {  get; set; }
        public string PRD_CODE { get; set; }
        public string CLASS_USER_ID { get; set; }
        public string CLASS_YMD { get; set; }
        public ClassEntity ClassEntity { get; set; }
        public List<ClassEntity> ClassList { get;}

    }
}
