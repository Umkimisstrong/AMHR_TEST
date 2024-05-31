using Entity;
using System.Collections.Generic;

namespace Contract
{
    
    public class BoardContract : GlobalContract
    {
        public string BRD_SEQ { get; set; }
        public string BRD_CATEGORY { get; set; }
        public string BRD_DIV { get; set; }

        public string BRD_TITLE { get; set; }
        public string BRD_CONTENTS {  get; set; }
        public string BRD_WRITE_NM { get; set; }
        public string BRD_WRITE_START_DT { get; set; }
        public string BRD_WRITE_END_DT { get; set; }
        public string BRD_PICK_DT { get; set; }
        
        public BoardEntity BoardEntity { get; set; }
        
        public List<BoardEntity> BoardList { get; set; }


    }
}
