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
using MySqlX.XDevAPI.Common;

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
            keyValuePairs.Add("I_CODE_ID    ", CODE_ID);

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
            keyValuePairs.Add("I_SYS_CODE_ID    ", SYS_CODE_ID);
            keyValuePairs.Add("I_DIV_CODE_ID    ", DIV_CODE_ID);
            keyValuePairs.Add("I_CODE_ID        ", CODE_ID);
            keyValuePairs.Add("I_CODE_NM        ", CODE_NM);
            keyValuePairs.Add("I_START_NUMBER   ", START_NUMBER);
            keyValuePairs.Add("I_ROW_COUNT      ", ROW_COUNT);

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

            switch (generalFlag)
            {
                case EnumProperties.GeneralFlag.CREATE:
                    result = InsertCodeEntity(entity) > 0 ? "IY" : "IN";
                    break;
                case EnumProperties.GeneralFlag.UPDATE:
                    result = UpdateCodeEntity(entity) > 0 ? "UY" : "UN";
                    break;
                case EnumProperties.GeneralFlag.DELETE:
                    result = DeleteCodeEntity(entity) > 0 ? "DY" : "DN";
                    break;

                default:break;
            }

            return result;
        }

        /// <summary>
        /// InsertCodeEntity : 코드 입력
        /// </summary>
        /// <param name="entity">Code 엔티티</param>
        /// <returns></returns>
        private int InsertCodeEntity(CodeEntity entity)
        {
            int result = 0;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_USER_ID            ", entity.CREATE_ID);
            keyValuePairs.Add("I_CODE_NM            ", entity.CODE_NM);
            keyValuePairs.Add("I_SYS_CODE_ID        ", entity.SYS_CODE_ID);
            keyValuePairs.Add("I_DIV_CODE_ID        ", entity.DIV_CODE_ID);
            keyValuePairs.Add("I_CODE_ID            ", entity.CODE_ID);
            keyValuePairs.Add("I_CODE_DESCRIPTION   ", entity.CODE_DESCRIPTION);
            keyValuePairs.Add("I_USE_YN             ", entity.USE_YN);
            keyValuePairs.Add("I_DEL_YN             ", "N");
            keyValuePairs.Add("I_SORT_ORDER         ", entity.SORT_ORDER);

            result = SqlHelper.GetNonQuery("SP_CMM_CODE_C", keyValuePairs);

            return result;
        }

        /// <summary>
        /// UpdateCodeEntity : 코드 수정
        /// </summary>
        /// <param name="entity">Code 엔티티</param>
        /// <returns></returns>
        private int UpdateCodeEntity(CodeEntity entity) 
        {
            int result = 0;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_USER_ID            ", entity.CREATE_ID);
            keyValuePairs.Add("I_CODE_NM            ", entity.CODE_NM);
            keyValuePairs.Add("I_SYS_CODE_ID        ", entity.SYS_CODE_ID);
            keyValuePairs.Add("I_DIV_CODE_ID        ", entity.DIV_CODE_ID);
            keyValuePairs.Add("I_CODE_ID            ", entity.CODE_ID);
            keyValuePairs.Add("I_CODE_DESCRIPTION   ", entity.CODE_DESCRIPTION);
            keyValuePairs.Add("I_USE_YN             ", entity.USE_YN);
            keyValuePairs.Add("I_DEL_YN             ", "N");
            keyValuePairs.Add("I_SORT_ORDER         ", entity.SORT_ORDER);

            result = SqlHelper.GetNonQuery("SP_CMM_CODE_U", keyValuePairs);

            return result;
        }

        /// <summary>
        /// DeleteCodeEntity : 코드 삭제
        /// </summary>
        /// <param name="entity">Code 엔티티</param>
        /// <returns></returns>
        private int DeleteCodeEntity(CodeEntity entity)
        {
            int result = 0;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();

            keyValuePairs.Add("I_SYS_CODE_ID    ", entity.SYS_CODE_ID);
            keyValuePairs.Add("I_DIV_CODE_ID    ", entity.DIV_CODE_ID);
            keyValuePairs.Add("I_CODE_ID        ", entity.CODE_ID);
            keyValuePairs.Add("I_DEL_YN         ", "Y");
            keyValuePairs.Add("I_USER_ID        ", entity.CREATE_ID);

            result = SqlHelper.GetNonQuery("SP_CMM_CODE_D", keyValuePairs);
            return result;
        }

        /// <summary>
        /// CheckCodeID : 코드 중복여부 체크
        /// </summary>
        /// <param name="SYS_CODE_ID">시스템 코드 ID</param>
        /// <param name="DIV_CODE_ID">분류 코드 ID</param>
        /// <param name="CODE_ID">코드 ID</param>
        /// <returns></returns>
        public bool CheckCodeID(string SYS_CODE_ID, string DIV_CODE_ID, string CODE_ID)
        {
            bool result = true;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_SYS_CODE_ID    ", SYS_CODE_ID);
            keyValuePairs.Add("I_DIV_CODE_ID    ", DIV_CODE_ID);
            keyValuePairs.Add("I_CODE_ID        ", CODE_ID);

            DataSet ds = SqlHelper.GetDataSet("SP_CMM_CODE_ID_CHECK", keyValuePairs);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                result = string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0][0].ToString().Trim());
            }

            return result;
        }

        /// <summary>
        /// GetCodeTextValueItem : 코드를 Text 와 Value 로 조회
        /// </summary>
        /// <param name="SYS_CODE_ID">시스템 코드 ID</param>
        /// <param name="DIV_CODE_ID">분류 코드 ID</param>
        /// <returns></returns>
        public DataSet GetCodeTextValueItem(string SYS_CODE_ID, string DIV_CODE_ID)
        {
            DataSet ds = new DataSet();

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_SYS_CODE_ID", SYS_CODE_ID);
            keyValuePairs.Add("I_DIV_CODE_ID", DIV_CODE_ID);

            ds = SqlHelper.GetDataSet("SP_CMM_CODE_TEXTVALUE", keyValuePairs);

            return ds;
        }
    }
}
