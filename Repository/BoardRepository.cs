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
        public BoardContract SelectBoardEntityList(string BRD_CATEGORY, string BRD_DIV, string BRD_TITLE, string BRD_CONTENTS, string BRD_WRITE_NM, string BRD_WRITE_START_DT, string BRD_WRITE_END_DT, string BRD_PICK_DT
                                            , int START_NUMBER, int ROW_COUNT)
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
            keyValuePairs.Add("BRD_SEQ      ", BRD_SEQ);
            keyValuePairs.Add("BRD_CATEGORY ", BRD_CATEGORY);
            keyValuePairs.Add("BRD_DIV      ", BRD_DIV);

            DataSet ds = SqlHelper.GetDataSet("SP_CMM_BOARD_S", keyValuePairs);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                boardEntity = ds.Tables[0].Rows[0].ConvertToEntity<BoardEntity>();
            }

            return boardEntity; 
        }


        public bool SaveBoard()
        {
            bool result = false;
            return result;
        }

        private bool InsertBoard()
        {
            bool result = false;
            return result;
        }

        private bool UpdateBoard() 
        {
            bool result = false;
            return result;
        }
    }
}
