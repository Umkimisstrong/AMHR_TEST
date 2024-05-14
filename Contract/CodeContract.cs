using Entity;
using System.Collections.Generic;

namespace Contract
{
    public class CodeContract : GlobalContract
    {

        public string SYS_CODE_ID {  get; set; }
        public string DIV_CODE_ID { get; set; }
        public string CODE_ID { get; set; }
        public string CODE_NM { get; set; }
        public CodeEntity CodeEntity { get; set; }
        public List<CodeEntity> CodeList { get; set; }


    }
}
