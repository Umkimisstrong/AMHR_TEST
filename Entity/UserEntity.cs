namespace Entity
{
    /// <summary>
    /// UserEntity : 사용자 엔티티
    /// </summary>
    public class UserEntity : GlobalEntity
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
        /// USER_PWD : 사용자 비밀번호
        /// </summary>
        public string USER_PWD { get; set; }
        /// <summary>
        /// USER_TYPE : 사용자 타입
        /// </summary>
        public string USER_TYPE { get; set; }
        /// <summary>
        /// USER_TYPE : 사용자 타입 명
        /// </summary>
        public string USER_TYPE_NM { get; set; }
        /// <summary>
        /// USER_EMAIL : 사용자 이메일
        /// </summary>
        public string USER_EMAIL { get; set; }
        /// <summary>
        /// USER_DESCRIPTION : 사용자 설명
        /// </summary>
        public string USER_DESCRIPTION { get; set; }
        /// <summary>
        /// USER_CREATE_TYPE : 사용자 생성 타입
        /// </summary>
        public string USER_CREATE_TYPE { get; set; }
        /// <summary>
        /// USER_CREATE_TYPE : 사용자 생성 타입 명
        /// </summary>
        public string USER_CREATE_TYPE_NM { get; set; }
        /// <summary>
        /// USE_YN : 사용여부
        /// </summary>
        public string USE_YN { get; set; }
        /// <summary>
        /// DEL_YN : 삭제여부
        /// </summary>
        public string DEL_YN { get; set; }
        /// <summary>
        /// CREATE_ID : 생성자
        /// </summary>
        public string CREATE_ID { get; set; }
        /// <summary>
        /// UPDATE_ID : 수정자
        /// </summary>
        public string UPDATE_ID { get; set; }



    }
}
