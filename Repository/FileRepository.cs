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

namespace Repository
{
    /// <summary>
    /// FileRepository : 파일 관련 DataAccess 담당
    /// </summary>
    public class FileRepository
    {
        /// <summary>
        /// CreateFile : 파일 저장
        /// </summary>
        /// <param name="entity">파일 엔티티</param>
        /// <returns></returns>
        public bool CreateFile(FileEntity entity)
        {
            bool result = false;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_FILE_NAME", entity.FILE_NAME);
	        keyValuePairs.Add("I_FILE_SERVER_NAME", entity.FILE_SERVER_NAME);
            keyValuePairs.Add("I_FILE_PATH", entity.FILE_PATH);
            keyValuePairs.Add("I_FILE_SIZE", entity.FILE_SIZE);
            keyValuePairs.Add("I_FILE_CATEGORY", entity.FILE_CATEGORY);
            keyValuePairs.Add("I_FILE_DIV", entity.FILE_DIV);
            keyValuePairs.Add("I_FILE_CATEGORY_SEQ", entity.FILE_CATEGORY_SEQ);
            keyValuePairs.Add("I_USE_YN", entity.USE_YN);
            keyValuePairs.Add("I_DEL_YN", entity.DEL_YN);
            keyValuePairs.Add("I_CREATE_ID", entity.CREATE_ID);
            keyValuePairs.Add("I_UPDATE_ID", entity.UPDATE_ID);
            

            int cResult = SqlHelper.GetNonQuery("SP_CMM_FILE_C", keyValuePairs);

            if (cResult > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// DeleteFile : 파일 삭제처리
        /// </summary>
        /// <param name="entity">파일 엔티티</param>
        /// <returns></returns>
        public bool DeleteFile(FileEntity entity) 
        {
            bool result = false;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_FILE_SEQ", entity.FILE_SEQ);
            keyValuePairs.Add("I_FILE_CATEGORY", entity.FILE_CATEGORY);
            keyValuePairs.Add("I_FILE_DIV", entity.FILE_DIV);
            keyValuePairs.Add("I_FILE_CATEGORY_SEQ", entity.FILE_CATEGORY_SEQ);
            keyValuePairs.Add("I_UPDATE_ID", entity.UPDATE_ID);

            int dResult = SqlHelper.GetNonQuery("SP_CMM_FILE_D", keyValuePairs);

            if (dResult > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// SelectFileList : 파일 조회
        /// </summary>
        /// <param name="I_FILE_CATEGORY">파일 카테고리</param>
        /// <param name="I_FILE_DIV">파일 분류</param>
        /// <param name="I_FILE_CATEGORY_SEQ">파일 카테고리 SEQ</param>
        /// <returns></returns>
        public List<FileEntity> SelectFileList(string I_FILE_CATEGORY, string I_FILE_DIV, string I_FILE_CATEGORY_SEQ)
        {
            List<FileEntity> result = new List<FileEntity>();

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();


            keyValuePairs.Add("I_FILE_CATEGORY", I_FILE_CATEGORY);
            keyValuePairs.Add("I_FILE_DIV", I_FILE_DIV);
            keyValuePairs.Add("I_FILE_CATEGORY_SEQ", I_FILE_CATEGORY_SEQ);

            DataSet ds = SqlHelper.GetDataSet("SP_CMM_FILE_L", keyValuePairs);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                result = ds.Tables[0].ConverToEntityList<FileEntity>();
            }

            return result;
        }
    }
}
