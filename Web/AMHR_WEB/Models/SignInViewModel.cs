using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMHR_WEB.Models
{
    /// <summary>
    /// SignInViewModel : 로그인에 필요한 정보
    /// </summary>
    public class SignInViewModel
    {
        /// <summary>
        /// User_ID : 사용자 ID
        /// </summary>
        public string User_ID { get; set; }
        /// <summary>
        /// Password : 패스워드
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// ErrorMessage : 에러메시지
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}