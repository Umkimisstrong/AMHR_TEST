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

namespace Repository
{
    /// <summary>
    /// UserRepository : User 관련 DataAccess 담당
    /// </summary>
    public class UserRepository
    {
        /// <summary>
        /// CreateUser : User 생성
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public bool CreateUser(UserContract contract)
        {
            bool result = false;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_USER_ID         ", contract.UserEntity.USER_ID);    
        	keyValuePairs.Add("I_USER_NM         ", contract.UserEntity.USER_NM);
        	keyValuePairs.Add("I_USER_PWD        ", contract.UserEntity.USER_PWD);
        	keyValuePairs.Add("I_USER_TYPE       ", contract.UserEntity.USER_TYPE);
        	keyValuePairs.Add("I_USER_EMAIL      ", contract.UserEntity.USER_EMAIL);
        	keyValuePairs.Add("I_USER_DESCRIPTION", contract.UserEntity.USER_DESCRIPTION);
        	keyValuePairs.Add("I_USE_YN          ", contract.UserEntity.USE_YN);
        	keyValuePairs.Add("I_DEL_YN          ", contract.UserEntity.DEL_YN);
            keyValuePairs.Add("I_USER_CREATE_TYPE", contract.UserEntity.USER_CREATE_TYPE);
            keyValuePairs.Add("I_CREATE_ID       ", contract.UserEntity.CREATE_ID);
            keyValuePairs.Add("I_UPDATE_ID       ", contract.UserEntity.UPDATE_ID);

            int iResult = SqlHelper.GetNonQuery("SP_CMM_USER_C", keyValuePairs);

            if (iResult > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// CheckUserID : USER ID 중복체크
        /// </summary>
        /// <param name="userID">사용자 ID</param>
        /// <returns></returns>
        public bool CheckUserID(string userID)
        {
            bool result = true;
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_USER_ID", userID);

            DataSet ds = SqlHelper.GetDataSet("SP_CMM_USER_ID_CHECK", keyValuePairs);
            if(ds != null && ds.Tables[0].Rows.Count > 0)
            { 
                
                result = string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0][0].ToString().Trim()); 
            }

            return result;
        }

        /// <summary>
        /// CheckUserCreateType : 이메일과 사용자 생성 타입으로 확인
        /// </summary>
        /// <param name="userEmail">사용자 이메일</param>
        /// <param name="userCreateType">사용자 생성 타입</param>
        /// <returns></returns>
        public string CheckUserCreateType(string userEmail, string userCreateType) 
        {
            string result = "";

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_USER_EMAIL", userEmail);
            keyValuePairs.Add("I_USER_CREATE_TYPE", userCreateType);

            DataSet ds = SqlHelper.GetDataSet("SP_CMM_USER_CREATE_TYPE_CHECK", keyValuePairs);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                result = ds.Tables[0].Rows[0][0].ToString().Trim();
            }
            return result;
        }

        /// <summary>
        /// LoginCheckUser : ID, PWD 로 Login Check
        /// </summary>
        /// <param name="userID">사용자 ID</param>
        /// <param name="userPWD">사용자 PWD</param>
        /// <returns></returns>
        public UserEntity LoginCheckUser(string userID, string userPWD)
        {
            UserEntity entity = null;
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_USER_ID", userID);
            keyValuePairs.Add("I_USER_PWD", userPWD);

            DataSet ds = SqlHelper.GetDataSet("SP_CMM_USER_LOGIN_S", keyValuePairs);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                entity = UtilRepository.ConvertToEntity<UserEntity>(ds.Tables[0].Rows[0]);
            }

            return entity;
        }

        /// <summary>
        /// SelectUserEntityList : 사용자 조회
        /// </summary>
        /// <param name="USER_ID">사용자 ID</param>
        /// <param name="USER_NM">사용자 이름</param>
        /// <param name="USER_TYPE">사용자 타입</param>
        /// <param name="USER_CREATE_TYPE">사용자 생성 타입</param>
        /// <param name="START_NUMBER">조회시작 번호</param>
        /// <param name="ROW_COUNT">조회 행 수</param>
        /// <returns></returns>
        public UserContract SelectUserEntityList(string userID, string userNM, string userType, string userCreateType, int START_NUMBER, int ROW_COUNT)
        {
            UserContract userContract = new UserContract();
            List<UserEntity> entityList = new List<UserEntity>();

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_USER_ID            ", userID);
	    	keyValuePairs.Add("I_USER_NM            ", userNM);
        	keyValuePairs.Add("I_USER_TYPE          ", userType);
            keyValuePairs.Add("I_USER_CREATE_TYPE   ", userCreateType);
            keyValuePairs.Add("I_START_NUMBER       ", START_NUMBER);
            keyValuePairs.Add("I_ROW_COUNT          ", ROW_COUNT);

            DataSet ds = SqlHelper.GetDataSet("SP_CMM_USER_L", keyValuePairs);


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                entityList = UtilRepository.ConverToEntityList<UserEntity>(ds.Tables[0]);
            }

            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                userContract.TOTAL_COUNT = int.Parse(ds.Tables[1].Rows[0]["TOTAL_COUNT"].ToString());
            }

            userContract.UserList = entityList;

            return userContract;
        }

        public UserEntity SelectUserEntity(string userID, string userEmail, string userCreateType)
        { 
            UserEntity userEntity = new UserEntity();

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_USER_ID", userID);
            keyValuePairs.Add("I_USER_EMAIL", userEmail);
            keyValuePairs.Add("I_USER_CREATE_TYPE", userCreateType);

            DataSet ds = SqlHelper.GetDataSet("SP_CMM_USER_S", keyValuePairs);

            if(ds != null && ds.Tables[0].Rows.Count>0)
            {
                userEntity = UtilRepository.ConvertToEntity<UserEntity>(ds.Tables[0].Rows[0]);
            }

            return userEntity;
        }

        public string UserChangePassword(string userID, string userEmail, string userCreateType, string userPwd, string loginID)
        {
            string result = "";
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_USER_ID            ", userID);
            keyValuePairs.Add("I_USER_EMAIL         ", userEmail);
            keyValuePairs.Add("I_USER_CREATE_TYPE   ", userCreateType);
            keyValuePairs.Add("I_USER_CHANGE_PWD    ", userPwd);
            keyValuePairs.Add("I_LOGIN_ID           ", loginID);

            DataSet ds = SqlHelper.GetDataSet("SP_CMM_USER_PWD_CHANGE", keyValuePairs);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                result = ds.Tables[0].Rows[0]["RETURN_RESULT"].ToString();
            }

            return result;
        }

    }
}
