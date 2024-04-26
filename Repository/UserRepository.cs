﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Contract;
using Entity;

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
        /// LoginCheckUser : ID, PWD 로 Login Check
        /// </summary>
        /// <param name="userID">사용자 ID</param>
        /// <param name="userPWD">사용자 PWD</param>
        /// <returns></returns>
        public bool LoginCheckUser(string userID, string userPWD)
        {
            bool result = false;
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_USER_ID", userID);
            keyValuePairs.Add("I_USER_PWD", userPWD);

            DataSet ds = SqlHelper.GetDataSet("SP_CMM_USER_LOGIN_S", keyValuePairs);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }
    }
}
