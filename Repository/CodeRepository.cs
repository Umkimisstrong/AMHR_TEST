using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Contract;
using Contract.ENUM;
using Entity;

namespace Repository
{
    
    public class CodeRepository
    {
        public CodeEntity SelectCodeEntity(string SYS_CODE_ID, string DIV_CODE_ID, string CODE_ID)
        { 

            CodeEntity entity = new CodeEntity();

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_SYS_CODE_ID", SYS_CODE_ID);
            keyValuePairs.Add("I_DIV_CODE_ID", DIV_CODE_ID);
            keyValuePairs.Add("I_CODE_ID", CODE_ID);

            DataSet ds = SqlHelper.GetDataSet("SP_CMM_CODE_S", keyValuePairs);


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                entity = UtilRepository.ConvertToEntity<CodeEntity>(ds.Tables[0].Rows[0]);
            }

            return entity;
        }

        public List<CodeEntity> SelectCodeEntityList(string SYS_CODE_ID, string DIV_CODE_ID)
        {

            List<CodeEntity> entityList = new List<CodeEntity>();

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_SYS_CODE_ID", SYS_CODE_ID);
            keyValuePairs.Add("I_DIV_CODE_ID", DIV_CODE_ID);

            DataSet ds = SqlHelper.GetDataSet("SP_CMM_CODE_L", keyValuePairs);


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                entityList = UtilRepository.ConverToEntityList<CodeEntity>(ds.Tables[0]);
            }

            return entityList;
        }

        public string SaveCodeEntity(CodeEntity entity, EnumProperties.GeneralFlag generalFlag)
        { 
            string result = "N";

            if (generalFlag.Equals(EnumProperties.GeneralFlag.CREATE))
            {
                // Insert
                result = "IY";
            }
            else if(generalFlag.Equals (EnumProperties.GeneralFlag.UPDATE)) 
            {
                // Update
                result = "UY";
            }
            else if(generalFlag.Equals(EnumProperties.GeneralFlag.DELETE))
            {
                // Delete
                result = "DY";
            }
            else
            {
                // 
                result = "NOTHING";
            }

            return result;
        }

    }
}
