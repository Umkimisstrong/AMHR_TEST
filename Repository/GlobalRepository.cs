using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using MySql.Data;
//using MySql.Data.MySqlClient;
using MySqlConnector;
using System.Reflection;

namespace Repository
{
    /// <summary>
    /// Sql 작업을 위한 Global 저장소
    /// </summary>
    public class GlobalRepository
    {
        /// <summary>
        /// Connection 문자열
        /// </summary>
        private static string AMHR = string.Empty;

        /// <summary>
        /// 생성자
        /// </summary>
        public GlobalRepository()
        {
            GlobalConst.Global_DB_Connect();
            AMHR = GlobalConst.AMHR; // 로컬 경로
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="ConnectionStrings">Connection 문자열</param>
        public GlobalRepository(string ConnectionStrings)
        {
            AMHR = ConnectionStrings;
        }

        /// <summary>
        /// SqlConnection 인스턴스 반환
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection getGlobalDBConnection()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = AMHR;
            return conn;
        }

        

    }
}
