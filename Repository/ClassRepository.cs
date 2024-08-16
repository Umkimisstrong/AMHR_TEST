using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Contract;
using Entity;
using Repository;
using System.Diagnostics.Contracts;
using Contract.ENUM;
using Org.BouncyCastle.Asn1.Mozilla;
using MySqlX.XDevAPI.Common;

namespace Repository
{
    /// <summary>
    /// ClassRepository : 클래스 관련 Data Access 담당
    /// </summary>
    public class ClassRepository
    {
        public bool CreateClassReservation(ClassEntity entity)
        {
            bool result = false;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_PRD_CODE", entity.PRD_CODE);
            keyValuePairs.Add("I_CLASS_USER_ID", entity.CLASS_USER_ID);
            keyValuePairs.Add("I_CLASS_YMD", entity.CLASS_YMD);
            keyValuePairs.Add("I_CLASS_TIME", entity.CLASS_TIME);
            keyValuePairs.Add("I_USE_YN", entity.USE_YN);
            keyValuePairs.Add("I_DEL_YN", entity.DEL_YN);
            keyValuePairs.Add("I_CREATE_ID", entity.CREATE_ID);
            keyValuePairs.Add("I_UPDATE_ID", entity.UPDATE_ID);

            string sResult = SqlHelper.GetReturnValue("SP_PRD_CLASS_C", keyValuePairs);
            if (!string.IsNullOrEmpty(sResult))
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// SelectClassRsvList : 클래스 예약현황 조회
        /// </summary>
        /// <param name="I_CLASS_YM">연,월(202408)</param>
        /// <returns></returns>
        public DataTable SelectClassRsvList(string I_CLASS_YM)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_CLASS_YM", I_CLASS_YM);
            

            DataSet ds = SqlHelper.GetDataSet("SP_CLS_RSV_LIST", keyValuePairs);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        /// <summary>
        /// GetTimeTextValueItem : 시간코드를 TextValueItem 형식으로 가져온다.
        /// </summary>
        /// <param name="dayOfWeek">요일을 숫자로 나타낸 값(현재미사용)</param>
        /// <returns></returns>
        public DataSet GetTimeTextValueItem(int dayOfWeek)
        {
            DataSet ds = new DataSet();

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_DAY", dayOfWeek);

            ds = SqlHelper.GetDataSet("SP_CLS_TIME_TEXTVALUE", keyValuePairs);

            return ds;
        }

        public string SelectClassTimeNm(string I_TIME_CODE)
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_TIME_CODE", I_TIME_CODE);

            string result = SqlHelper.GetReturnValue("SP_CLS_TIME_S", keyValuePairs);

            return result;
        }

    }
}
