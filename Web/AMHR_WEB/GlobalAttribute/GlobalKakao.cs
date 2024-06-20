using System.Xml;

namespace AMHR_WEB.GlobalAttribute
{
    /// <summary>
    /// GlobalKakao : 글로벌 카카오 클래스
    /// </summary>
    public class GlobalKakao
    {
        /// <summary>
        /// CLIENT_ID : 카카오 로그인에 필요한 Application 정보 - ID
        /// </summary>
        public static string CLIENT_ID { get; set; }
        /// <summary>
        /// CLIENT_SECRET : 카카오 로그인에 필요한 Application 정보 - SECRET
        /// </summary>
        public static string CLIENT_SECRET { get; set; }
        /// <summary>
        /// LOGIN_CALL_BACK_URI : 로그인 이후 CALL_BACK 받을 URI
        /// </summary>
        public static string LOGIN_CALL_BACK_URI { get; set; }
        /// <summary>
        /// CODE_URI : 로그인 후 코드를 넘겨받기 위한 URI
        /// </summary>
        public static string CODE_URI { get; set; }
        /// <summary>
        /// TOKEN_URI : 코드를 넘겨받고 카카오 유저 정보를 얻기 위한 URI
        /// </summary>
        public static string TOKEN_URI { get; set; }
        /// <summary>
        /// CONST_PWD : AMHR DataBase 에 저장시킬 상수 PWD
        /// </summary>
        public static string CONST_PWD { get; set; }

        /// <summary>
        /// XmlDocument 인스턴스
        /// </summary>
        private static XmlDocument xmlDoc = null;

        /// <summary>
        /// SetGlobalKakao : 로컬 디렉토리의 xml 파일로 해당 정보를 관리한다.
        /// </summary>
        public static void SetGlobalKakao()
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load("D:\\AMHR_TEST\\Settings\\applicationSettings.xml");
            XmlNode xmlUriNode = xmlDoc.SelectSingleNode("AppSettings/KakaoConfig/Uri");
            CODE_URI = xmlUriNode.SelectSingleNode("CodeUri").Attributes["id"].Value;
            TOKEN_URI = xmlUriNode.SelectSingleNode("TokenUri").Attributes["id"].Value;
            LOGIN_CALL_BACK_URI = xmlUriNode.SelectSingleNode("LoginCallBack").Attributes["id"].Value;

            XmlNode xmlKeyNode = xmlDoc.SelectSingleNode("AppSettings/KakaoConfig/Registry_KEY");
            CLIENT_ID = xmlKeyNode.SelectSingleNode("Client_ID").Attributes["id"].Value;
            CLIENT_SECRET = xmlKeyNode.SelectSingleNode("Client_Secret").Attributes["id"].Value;

            XmlNode xmlPwdNode = xmlDoc.SelectSingleNode("AppSettings/KakaoConfig/CONST_PWD");
            CONST_PWD = xmlPwdNode.SelectSingleNode("Client_PWD").Attributes["id"].Value;
            
        }

    }
}