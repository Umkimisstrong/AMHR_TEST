using Entity;
using System.Collections.Generic;

namespace Contract
{
    /// <summary>
    /// UserContract : 사용자 Contract
    /// </summary>
    public class UserContract : GlobalContract
    {

        /// <summary>
        /// USER_ID : 사용자 ID
        /// </summary>
        public string USER_ID { get; set; }
        /// <summary>
        /// USER_NM : 사용자 명
        /// </summary>
        public string USER_NM { get; set; }
        /// <summary>
        /// USER_TYPE : 사용자 생성 타입
        /// </summary>
        public string USER_CREATE_TYPE { get; set; }
        /// <summary>
        /// USER_TYPE : 사용자 타입
        /// </summary>
        public string USER_TYPE { get; set; }

        /// <summary>
        /// UserEntity : 사용자 엔티티(1개의 사용자 테이블)
        /// </summary>
        public UserEntity UserEntity { get; set; }
        /// <summary>
        /// UserList : 사용자 집합
        /// </summary>
        public List<UserEntity> UserList { get; set; }


    }
}
