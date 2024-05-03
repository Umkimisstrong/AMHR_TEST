using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMHR_WEB.Models
{
    /// <summary>
    /// CustomErrorModel : 사용자 정의 에러 모델
    /// </summary>
    public class CustomErrorModel
    {
        /// <summary>
        /// status : 상태
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// statusText : 상태 문자열
        /// </summary>
        public string statusText { get; set; }
        /// <summary>
        /// responseText : 응답 문자열
        /// </summary>
        public string responseText { get; set; }
    }
}