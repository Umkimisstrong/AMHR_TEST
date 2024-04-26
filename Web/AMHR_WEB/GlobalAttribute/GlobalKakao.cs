using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace AMHR_WEB.GlobalAttribute
{
    public class GlobalKakao
    {
        public static string CLIENT_ID { get; set; }
        public static string CLIENT_SECRET { get; set; }
        public static string LOGIN_CALL_BACK_URI { get; set; }
        public static string CODE_URI { get; set; }
        public static string TOKEN_URI { get; set; }

        private static XmlDocument xmlDoc = null;

        public static void SetGlobalKakao()
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load("D:\\AMHR\\Settings\\applicationSettings.xml");
            XmlNode xmlUriNode = xmlDoc.SelectSingleNode("AppSettings/KakaoConfig/Uri");
            TOKEN_URI = xmlUriNode.SelectSingleNode("TokenUri").Attributes["id"].Value;
            LOGIN_CALL_BACK_URI = xmlUriNode.SelectSingleNode("LoginCallBack").Attributes["id"].Value;

            XmlNode xmlKeyNode = xmlDoc.SelectSingleNode("AppSettings/KakaoConfig/Registry_KEY");
            CLIENT_ID = xmlKeyNode.SelectSingleNode("Client_ID").Attributes["id"].Value;
            CLIENT_SECRET = xmlKeyNode.SelectSingleNode("Client_Secret").Attributes["id"].Value;
        }

    }
}