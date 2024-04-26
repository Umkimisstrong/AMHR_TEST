using Entity;
using System.Collections.Generic;

namespace Contract
{
    public class CodeContract : GlobalContract
    {


        public string CODE_ID { get; set; }

        public string CODE_NM { get; set; }

        public string SYS_CD { get; set; }

        public string DIV_CD { get; set; }


        public CodeEntity CodeEntity { get; set; }

        public List<CodeEntity> CodeList { get; set; }


    }
}
