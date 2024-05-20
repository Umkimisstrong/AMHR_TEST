using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMHR_WEB.Models
{
    /// <summary>
    /// UserSessionModel : 유저 세션 모델 클래스
    /// </summary>
    public class UserSessionModel
    {
        
        /// <summary>
        /// UserId : 유저 아이디
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// DisplayName : 표시명칭
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// USER_ID : 유저 아이디
        /// </summary>
        public string USER_ID { get; set; }
        /// <summary>
        /// USER_NM : 유저명
        /// </summary>
        public string USER_NM { get; set; }
        /// <summary>
        /// USER_TYPE : 유저타입
        /// </summary>
        public string USER_TYPE { get; set; }
        /// <summary>
        /// USER_EMAIL : 유저이메일
        /// </summary>
        public string USER_EMAIL { get; set; }
        /// <summary>
        /// USER_DESCRIPTION : 유저 설명
        /// </summary>
        public string USER_DESCRIPTION { get; set; }
        /// <summary>
        /// USER_CREATE_TYPE : 유저 생성 타입
        /// </summary>
        public string USER_CREATE_TYPE { get; set; }
        /// <summary>
        /// USE_YN : 사용여부
        /// </summary>
        public string USE_YN { get; set; }
        /// <summary>
        /// DEL_YN : 삭제여부
        /// </summary>
        public string DEL_YN { get; set; }
    }
}