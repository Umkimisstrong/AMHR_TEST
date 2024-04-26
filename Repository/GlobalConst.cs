using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Repository
{
    public class GlobalConst
    {
        public static string AMHR = string.Empty;
        public static void Global_DB_Connect()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("D:\\AMHR\\Settings\\applicationSettings.xml");
            XmlNode xmlKeyNode = xmlDoc.SelectSingleNode("AppSettings/DBConnection/ConnectionStrings");
            AMHR = xmlKeyNode.Attributes["id"].Value;
        }

        GlobalConst() 
        {
            
        }

    }
}
