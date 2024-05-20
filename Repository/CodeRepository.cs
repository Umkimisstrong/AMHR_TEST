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
    /// <summary>
    /// CodeRepository : 코드 리포지토리
    /// </summary>
    public class CodeRepository
    {
        /// <summary>
        /// SelectCodeEntity : 단일 코드 조회
        /// </summary>
        /// <param name="SYS_CODE_ID">시스템 코드 ID</param>
        /// <param name="DIV_CODE_ID">분류 코드 ID</param>
        /// <param name="CODE_ID">코드 ID</param>
        /// <returns></returns>
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

        /// <summary>
        /// SelectCodeEntityList : 코드 리스트 조회
        /// </summary>
        /// <param name="SYS_CODE_ID">시스템 코드 ID</param>
        /// <param name="DIV_CODE_ID">분류 코드 ID</param>
        /// <param name="CODE_ID">코드 ID</param>
        /// <param name="CODE_NM">코드 명칭</param>
        /// <param name="START_NUMBER">조회시작 번호</param>
        /// <param name="ROW_COUNT">조회 행 수</param>
        /// <returns></returns>
        public CodeContract SelectCodeEntityList(string SYS_CODE_ID, string DIV_CODE_ID, string CODE_ID, string CODE_NM, int START_NUMBER, int ROW_COUNT)
        {
            CodeContract codeContract = new CodeContract();
            List<CodeEntity> entityList = new List<CodeEntity>();

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_SYS_CODE_ID", SYS_CODE_ID);
            keyValuePairs.Add("I_DIV_CODE_ID", DIV_CODE_ID);
            keyValuePairs.Add("I_CODE_ID", CODE_ID);
            keyValuePairs.Add("I_CODE_NM", CODE_NM);
            keyValuePairs.Add("I_START_NUMBER", START_NUMBER);
            keyValuePairs.Add("I_ROW_COUNT", ROW_COUNT);

            DataSet ds = SqlHelper.GetDataSet("SP_CMM_CODE_L", keyValuePairs);


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                entityList = UtilRepository.ConverToEntityList<CodeEntity>(ds.Tables[0]);
            }

            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                codeContract.TOTAL_COUNT = int.Parse(ds.Tables[1].Rows[0]["TOTAL_COUNT"].ToString());
            }

            codeContract.CodeList = entityList;

            return codeContract;
        }

        /// <summary>
        /// SaveCodeEntity : 코드 저장
        /// </summary>
        /// <param name="entity">코드 엔티티</param>
        /// <param name="generalFlag">일반 플래그</param>
        /// <returns></returns>
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
