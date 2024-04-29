using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using System.Data.SqlClient;
//using MySql.Data.MySqlClient;
using MySqlConnector;
using System.Reflection;

namespace Repository
{
    /// <summary>
    /// SqlHelper - sql 을 실행하기 위한 클래스
    /// </summary>
    public class SqlHelper
    {
        /// <summary>
        /// Connection 문자열
        /// </summary>
        private static string strSqlConnection = string.Empty;

        /// <summary>
        /// 생성자
        /// </summary>
        public SqlHelper() { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="ConnectionStrings">Connection 문자열</param>
        public SqlHelper(string ConnectionStrings)
        {
            strSqlConnection = ConnectionStrings;
        }

        /// <summary>
        /// SqlConnection 인스턴스를 반환
        /// </summary>
        /// <param name="ConnectionStrings">Connection 문자열</param>
        /// <returns></returns>
        public static MySqlConnection getConnection(string ConnectionStrings) 
        {

            GlobalRepository repository = string.IsNullOrEmpty(ConnectionStrings)
                                         ? new GlobalRepository()
                                         : new GlobalRepository(ConnectionStrings);

            MySqlConnection mySqlConnection = repository.getGlobalDBConnection();
            return mySqlConnection;
        }

        /// <summary>
        /// Stored Procedure 을 실행하고 DataSet 을 반환
        /// </summary>
        /// <param name="spName">Stored Procedure 명</param>
        /// <param name="paramSource">Dictionary 형식의 파라미터</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string spName, Dictionary<string, object> paramSource)
        {
            DataSet ds = new DataSet();

            using (MySqlConnection mySqlConnection =  getConnection(strSqlConnection))
            {
                mySqlConnection.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(spName, mySqlConnection))
                {
                    mySqlCommand.CommandType = CommandType.StoredProcedure;
                    mySqlCommand.CommandTimeout = 30;
                    mySqlCommand.Parameters.AddRange(ToSqlParams(paramSource));

                    
                    MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);

                    adapter.Fill(ds);
                }
                mySqlConnection.Clone();

            }
            

            return ds;
        }

        /// <summary>
        /// Stored Procedure 을 실행하고 int 결과를 반환
        /// </summary>
        /// <param name="spName">Stored Procedure 명</param>
        /// <param name="paramSource">Dictionary 형식의 파라미터</param>
        /// <returns></returns>
        public static int GetNonQuery(string spName, Dictionary<string, object> paramSource)
        {
            int iResult = -1;

            using(MySqlConnection mySqlConnection = getConnection(strSqlConnection))
            {
                mySqlConnection.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(spName, mySqlConnection)) 
                {

                    mySqlCommand.CommandType = CommandType.StoredProcedure;
                    mySqlCommand.CommandTimeout = 30;
                    mySqlCommand.Parameters.AddRange(ToSqlParams(paramSource));

                    iResult = mySqlCommand.ExecuteNonQuery();
                }
                mySqlConnection.Clone();
            }

            return iResult;
        }


        /// <summary>
        /// Dictionary 형식의 파라미터를 SqlParameter [] 형식으로 변환
        /// </summary>
        /// <param name="paramSource">Dictionary 형식의 파라미터</param>
        /// <returns></returns>
        public static MySqlParameter[] ToSqlParams(Dictionary<string, object> paramSource)
        {
            MySqlParameter[] mySqlParams = new MySqlParameter[paramSource.Count];

            int i = 0;

            foreach (KeyValuePair<string, object> param in paramSource)
            {
                mySqlParams[i] = new MySqlParameter();
                mySqlParams[i].ParameterName = param.Key;
                mySqlParams[i].Value = param.Value;

                i += 1;
            }
            return mySqlParams;                
        }


        
    }
}
