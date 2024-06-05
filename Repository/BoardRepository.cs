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
    /// BoardRepository : 게시판 리포지토리
    /// </summary>
    public class BoardRepository
    {
        /// <summary>
        /// SelectBoardEntityList : 게시판 리스트 조회
        /// </summary>
        /// <param name="BRD_CATEGORY">게시판 카테고리</param>
        /// <param name="BRD_DIV">게시판 분류</param>
        /// <param name="BRD_TITLE">제목</param>
        /// <param name="BRD_CONTENTS">내용</param>
        /// <param name="BRD_WRITE_NM">작성자 명</param>
        /// <param name="BRD_WRITE_START_DT">시작일</param>
        /// <param name="BRD_WRITE_END_DT">종료일</param>
        /// <param name="BRD_PICK_DT">특정일</param>
        /// <param name="START_NUMBER">조회시작 번호</param>
        /// <param name="ROW_COUNT">조회 행 수</param>
        /// <returns></returns>
        public BoardContract SelectBoardEntityList(
                                                   string BRD_CATEGORY, string BRD_DIV, string BRD_TITLE, string BRD_CONTENTS, string BRD_WRITE_NM
                                                 , string BRD_WRITE_START_DT, string BRD_WRITE_END_DT, string BRD_PICK_DT
                                                 , int START_NUMBER, int ROW_COUNT
                                                   )
        { 
            BoardContract boardContract = new BoardContract();
            boardContract.BoardList = new List<BoardEntity>();
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_BRD_CATEGORY       ", BRD_CATEGORY);
            keyValuePairs.Add("I_BRD_DIV			", BRD_DIV);
            keyValuePairs.Add("I_BRD_TITLE			", BRD_TITLE);
            keyValuePairs.Add("I_BRD_CONTENTS		", BRD_CONTENTS);
            keyValuePairs.Add("I_BRD_WRITE_NM		", BRD_WRITE_NM);
            keyValuePairs.Add("I_BRD_WRITE_START_DT ", BRD_WRITE_START_DT);
            keyValuePairs.Add("I_BRD_WRITE_END_DT	", BRD_WRITE_END_DT);
            keyValuePairs.Add("I_BRD_PICK_DT	    ", BRD_PICK_DT);
            keyValuePairs.Add("I_START_NUMBER		", START_NUMBER);
            keyValuePairs.Add("I_ROW_COUNT			", ROW_COUNT);

            DataSet ds = SqlHelper.GetDataSet("SP_CMM_BOARD_L", keyValuePairs);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            { 
                boardContract.BoardList = ds.Tables[0].ConverToEntityList<BoardEntity>();
            }
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                boardContract.TOTAL_COUNT = int.Parse(ds.Tables[1].Rows[0]["TOTAL_COUNT"].ToString());
            }

            return boardContract;
        }

        /// <summary>
        /// SelectBoardEntity : 단일 게시판 조회
        /// </summary>
        /// <param name="BRD_SEQ">게시판 SEQ</param>
        /// <param name="BRD_CATEGORY">게시판 카테고리</param>
        /// <param name="BRD_DIV">게시판 분류</param>
        /// <returns></returns>
        public BoardEntity SelectBoardEntity(string BRD_SEQ, string BRD_CATEGORY, string BRD_DIV)
        {
            BoardEntity boardEntity = new BoardEntity();

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_BRD_SEQ      ", BRD_SEQ);
            keyValuePairs.Add("I_BRD_CATEGORY ", BRD_CATEGORY);
            keyValuePairs.Add("I_BRD_DIV      ", BRD_DIV);

            DataSet ds = SqlHelper.GetDataSet("SP_CMM_BOARD_S", keyValuePairs);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                boardEntity = ds.Tables[0].Rows[0].ConvertToEntity<BoardEntity>();
            }

            return boardEntity; 
        }


        public bool SaveBoard(BoardEntity entity, EnumProperties.GeneralFlag generalFlag)
        {
            bool result = false;
            switch (generalFlag)
            {
                case EnumProperties.GeneralFlag.CREATE:
                    result = InsertBoard(entity);
                    break;
                case EnumProperties.GeneralFlag.UPDATE:
                    result = UpdateBoard(entity);
                    break;
                case EnumProperties.GeneralFlag.DELETE:
                    result = DeleteBoard(entity);
                    break;
                default:
                    break;
            }

            return result;
        }

        private bool InsertBoard(BoardEntity entity)
        {
            bool result = false;
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_BRD_CATEGORY   ", entity.BRD_CATEGORY);
            keyValuePairs.Add("I_BRD_DIV        ", entity.BRD_DIV);
            keyValuePairs.Add("I_BRD_TITLE      ", entity.BRD_TITLE);
            keyValuePairs.Add("I_BRD_CONTENTS   ", entity.BRD_CONTENTS);
            keyValuePairs.Add("I_BRD_WRITE_ID   ", entity.BRD_WRITE_ID);
            keyValuePairs.Add("I_USE_YN         ", entity.USE_YN);
            keyValuePairs.Add("I_DEL_YN         ", entity.DEL_YN);
            keyValuePairs.Add("I_CREATE_ID      ", entity.CREATE_ID);
            keyValuePairs.Add("I_UPDATE_ID      ", entity.UPDATE_ID);

            DataSet ds = SqlHelper.GetDataSet("SP_CMM_BOARD_C", keyValuePairs);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SEQ_NO"] != null)
                {
                    string sResult = ds.Tables[0].Rows[0]["SEQ_NO"].ToString();
                    if (string.IsNullOrEmpty(sResult))
                    {
                        result = true;
                        entity.BRD_SEQ = sResult;
                    }
                } 
            }

            return result;
        }

        private bool UpdateBoard(BoardEntity entity) 
        {
            bool result = false;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("BRD_SEQ      ", entity.BRD_SEQ);
            keyValuePairs.Add("BRD_CATEGORY ", entity.BRD_CATEGORY);
            keyValuePairs.Add("BRD_DIV      ", entity.BRD_DIV);
            keyValuePairs.Add("BRD_TITLE    ", entity.BRD_TITLE);
            keyValuePairs.Add("BRD_CONTENTS ", entity.BRD_CONTENTS);
            keyValuePairs.Add("BRD_WRITE_ID ", entity.BRD_WRITE_ID);
            keyValuePairs.Add("UPDATE_ID    ", entity.UPDATE_ID);

            int uResult = SqlHelper.GetNonQuery("SP_CMM_BOARD_U", keyValuePairs);
            if (uResult > 0)
            {
                result = true;
            }
            return result;
        }

        private bool DeleteBoard(BoardEntity entity) 
        {
            bool result = false;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();

            keyValuePairs.Add("BRD_SEQ      ", entity.BRD_SEQ);
            keyValuePairs.Add("BRD_CATEGORY ", entity.BRD_CATEGORY);
            keyValuePairs.Add("BRD_DIV      ", entity.BRD_DIV);
            keyValuePairs.Add("BRD_WRITE_ID ", entity.BRD_WRITE_ID);

            int dResult = SqlHelper.GetNonQuery("SP_CMM_BOARD_D", keyValuePairs);
            if (dResult > 0)
            {
                result = true;
            }

            return result;
        }
    }
}
