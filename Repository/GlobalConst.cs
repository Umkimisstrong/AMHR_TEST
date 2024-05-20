using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Repository
{
    /// <summary>
    /// GlobalConst : 전역 리포지토리 상수 관리
    /// </summary>
    public class GlobalConst
    {
        /// <summary>
        /// AMHR : AMHR String
        /// </summary>
        public static string AMHR = string.Empty;

        /// <summary>
        /// Global_DB_Connect : AMHR 을 Local Xml 에서의 값으로 ConnectionString 반환
        /// </summary>
        public static void Global_DB_Connect()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("D:\\AMHR\\Settings\\applicationSettings.xml");
            XmlNode xmlKeyNode = xmlDoc.SelectSingleNode("AppSettings/DBConnection/ConnectionStrings");
            AMHR = xmlKeyNode.Attributes["id"].Value;
        }

        /// <summary>
        /// GlobalConst : 생성자
        /// </summary>
        GlobalConst() 
        {
            
        }

    }
}
