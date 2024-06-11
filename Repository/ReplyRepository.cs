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
    /// ReplyRepository : 댓글 리포지토리
    /// </summary>
    public class ReplyRepository
    {
        /// <summary>
        /// SaveReply : 댓글 저장
        /// </summary>
        /// <param name="entity">댓글 엔티티</param>
        /// <param name="generalFlag">일반 플래그</param>
        /// <returns></returns>
        public bool SaveReply(ReplyEntity entity, EnumProperties.GeneralFlag generalFlag)
        {
            bool result = false;
            switch (generalFlag)
            {
                case EnumProperties.GeneralFlag.CREATE:
                    int iResult = InsertReply(entity);
                    if (iResult > 0){ result = true; }
                    break;
                case EnumProperties.GeneralFlag.UPDATE:
                    int uResult = UpdateReply(entity);
                    if(uResult> 0) { result = true; }
                    break;
                case EnumProperties.GeneralFlag.DELETE:
                    int dResult = DeleteReply(entity);
                    if (dResult > 0){  result = true; }
                    break;
            }
            return result;
        }

        /// <summary>
        /// InsertReply : 댓글 입력
        /// </summary>
        /// <param name="entity">댓글 엔티티</param>
        /// <returns></returns>
        private int InsertReply(ReplyEntity entity) 
        {
            int result = 0;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_REPLY_PARENT_SEQ", entity.REPLY_PARENT_SEQ);
            keyValuePairs.Add("I_BRD_SEQ", entity.BRD_SEQ);
            keyValuePairs.Add("I_BRD_CATEGORY", entity.BRD_CATEGORY);
            keyValuePairs.Add("I_BRD_DIV", entity.BRD_DIV);
            keyValuePairs.Add("I_REPLY_COMMENTS", entity.REPLY_COMMENTS);
            keyValuePairs.Add("I_REPLY_WRITE_ID", entity.REPLY_WRITE_ID);
            keyValuePairs.Add("I_USE_YN", entity.USE_YN);
            keyValuePairs.Add("I_DEL_YN", entity.DEL_YN);
            keyValuePairs.Add("I_CREATE_ID", entity.CREATE_ID);
            keyValuePairs.Add("I_UPDATE_ID", entity.UPDATE_ID);

            result = SqlHelper.GetNonQuery("SP_BRD_REPLY_C", keyValuePairs);

            return result;
        }

        /// <summary>
        /// UpdateReply : 댓글 수정
        /// </summary>
        /// <param name="entity">댓글 엔티티</param>
        /// <returns></returns>
        private int UpdateReply(ReplyEntity entity) 
        {
            int result = 0;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("REPLY_SEQ", entity.REPLY_SEQ);
            keyValuePairs.Add("REPLY_PARENT_SEQ", entity.REPLY_PARENT_SEQ);
            keyValuePairs.Add("BRD_SEQ", entity.BRD_SEQ);
            keyValuePairs.Add("BRD_CATEGORY", entity.BRD_CATEGORY);
            keyValuePairs.Add("BRD_DIV", entity.BRD_DIV);
            keyValuePairs.Add("REPLY_COMMENTs", entity.REPLY_COMMENTS);
            keyValuePairs.Add("REPLY_WRITE_ID", entity.REPLY_WRITE_ID);
            keyValuePairs.Add("CREATE_ID", entity.CREATE_ID);
            keyValuePairs.Add("UPDATE_ID", entity.UPDATE_ID);

            result = SqlHelper.GetNonQuery("SP_BRD_REPLY_U", keyValuePairs);

            return result;
        }

        /// <summary>
        /// DeleteReply : 댓글 삭제
        /// </summary>
        /// <param name="entity">댓글 엔티티</param>
        /// <returns></returns>
        private int DeleteReply(ReplyEntity entity) 
        {
            int result = 0;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("REPLY_SEQ", entity.REPLY_SEQ);
            keyValuePairs.Add("REPLY_PARENT_SEQ", entity.REPLY_PARENT_SEQ);
            keyValuePairs.Add("UPDATE_ID", entity.UPDATE_ID);

            result = SqlHelper.GetNonQuery("SP_BRD_REPLY_D", keyValuePairs);

            return result;
        }

        /// <summary>
        /// SelectReplyList : 댓글 조회
        /// </summary>
        /// <param name="BRD_SEQ">게시판 SEQ</param>
        /// <param name="BRD_CATEGORY">게시판 카테고리</param>
        /// <param name="BRD_DIV">게시판 분류</param>
        /// <returns></returns>
        public List<ReplyEntity> SelectReplyList(string BRD_SEQ, string BRD_CATEGORY, string BRD_DIV)
        { 
            List<ReplyEntity > replyList = new List<ReplyEntity>();

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("BRD_SEQ", BRD_SEQ);
            keyValuePairs.Add("BRD_CATEGORY", BRD_CATEGORY);
            keyValuePairs.Add("BRD_DIV", BRD_DIV);

            DataSet ds = SqlHelper.GetDataSet("SP_BRD_REPLY_L", keyValuePairs);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                replyList = UtilRepository.ConverToEntityList<ReplyEntity>(ds.Tables[0]);
            }


            return replyList;
        }
    }
}
