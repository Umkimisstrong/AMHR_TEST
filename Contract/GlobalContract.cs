using System;

namespace Contract
{
    /// <summary>
    /// GlobalContract : 전역 Contract
    /// </summary>
    public class GlobalContract
    {
        /// <summary>
        /// PAGE_NUMBER : 페이지 번호
        /// </summary>
        public int PAGE_NUMBER { get; set; }
        /// <summary>
        /// TOTAL_COUNT : 전체 수
        /// </summary>
        public int TOTAL_COUNT { get; set; }
        /// <summary>
        /// CREATE_DT : 생성일
        /// </summary>
        public DateTime CREATE_DT { get; set; }
        /// <summary>
        /// UPDATE_DT : 수정일
        /// </summary>
        public DateTime UPDATE_DT { get; set;  }



    }
}
