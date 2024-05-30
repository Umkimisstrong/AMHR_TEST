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
        public BoardContract SelectBoardEntityList()
        { 
            BoardContract boardContract = new BoardContract();
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
