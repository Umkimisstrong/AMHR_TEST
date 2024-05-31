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
    public class BoardRepository
    {
        public BoardContract SelectBoardEntityList(string I_BRD_CATEGORY, string I_BRD_DIV, string I_BRD_TITLE, string I_BRD_CONTENTS, string I_BRD_WRITE_NM, string I_BRD_WRITE_START_DT, string I_BRD_WRITE_END_DT, string I_BRD_PICK_DT
                                            , int I_START_NUMBER, int I_ROW_COUNT)
        { 
            BoardContract boardContract = new BoardContract();
            boardContract.BoardList = new List<BoardEntity>();
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_BRD_CATEGORY       ", I_BRD_CATEGORY);
            keyValuePairs.Add("I_BRD_DIV			", I_BRD_DIV);
            keyValuePairs.Add("I_BRD_TITLE			", I_BRD_TITLE);
            keyValuePairs.Add("I_BRD_CONTENTS		", I_BRD_CONTENTS);
            keyValuePairs.Add("I_BRD_WRITE_NM		", I_BRD_WRITE_NM);
            keyValuePairs.Add("I_BRD_WRITE_START_DT ", I_BRD_WRITE_START_DT);
            keyValuePairs.Add("I_BRD_WRITE_END_DT	", I_BRD_WRITE_END_DT);
            keyValuePairs.Add("I_BRD_PICK_DT	    ", I_BRD_PICK_DT);
            keyValuePairs.Add("I_START_NUMBER		", I_START_NUMBER);
            keyValuePairs.Add("I_ROW_COUNT			", I_ROW_COUNT);

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

        public BoardEntity SelectBoardEntity()
        {
            BoardEntity boardEntity = new BoardEntity();
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
