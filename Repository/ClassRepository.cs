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
            keyValuePairs.Add("I_PRD_TYPE_CODE", entity.PRD_TYPE_CODE);
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

        
        public DataSet GetTimeTextValueItem(int dayOfWeek)
        {
            DataSet ds = new DataSet();

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_DAY", dayOfWeek);

            ds = SqlHelper.GetDataSet("SP_CLS_TIME_TEXTVALUE", keyValuePairs);

            return ds;
        }

    }
}
