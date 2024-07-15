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


        /// <summary>
        /// SaveBoard : 게시판 저장
        /// </summary>
        /// <param name="entity">게시판 엔티티</param>
        /// <param name="generalFlag">일반 플래그</param>
        /// <returns></returns>
        //public bool SaveBoard(BoardEntity entity, EnumProperties.GeneralFlag generalFlag)
        public bool SaveBoard(BoardContract contract, EnumProperties.GeneralFlag generalFlag)
        {
            bool result = false;

            // 파일 작업객체 생성
            FileRepository fileRepository = new FileRepository();

            switch (generalFlag)
            {
                // 신규
                case EnumProperties.GeneralFlag.CREATE:             

                    // 게시판 생성 후 BRD_SEQ 를 넘겨받는다.
                    string BRD_SEQ = InsertBoard(contract.BoardEntity);
                    if (!string.IsNullOrEmpty(BRD_SEQ))
                    {
                        result = true; // 결과는 true 이다.

                        // 파일 정보가 있다면 파일을 생성한다.
                        if (contract.FileContract != null && contract.FileContract.UploadFileList != null)
                        {
                            // 파일 정보를 게시판 정보로 담는다.
                            foreach (FileEntity item in contract.FileContract.UploadFileList)
                            {
                                item.FILE_CATEGORY = contract.BoardEntity.BRD_CATEGORY;
                                item.FILE_DIV = contract.BoardEntity.BRD_DIV;
                                item.FILE_CATEGORY_SEQ = BRD_SEQ;
                                item.CREATE_ID = contract.BoardEntity.CREATE_ID;
                                item.UPDATE_ID = contract.BoardEntity.UPDATE_ID;
                                item.USE_YN = "Y";
                                item.DEL_YN = "N";

                                // 파일 정보를 Insert 한다. (신규는 기존파일과 삭제파일 등 비교하는 로직이 없기 때문에 바로 Insert 한다.
                                result = fileRepository.CreateFile(item);
                            }
                        }
                    }
                    else
                    {
                        result = false;
                    }
                    break;

                // 수정
                case EnumProperties.GeneralFlag.UPDATE:
                    
                    result = UpdateBoard(contract.BoardEntity);
                    if (result)
                    {
                        // 게시판 수정 후 파일 정보가 있다면 파일 관련 작업을 진행
                        if (contract.FileContract != null)
                        {
                            // 신규파일 있다면 신규파일 정보를 게시판 정보로 세팅한다.
                            if (contract.FileContract.UploadFileList != null)
                            {
                                foreach (FileEntity item in contract.FileContract.UploadFileList)
                                {
                                    item.FILE_CATEGORY = contract.BoardEntity.BRD_CATEGORY;
                                    item.FILE_DIV = contract.BoardEntity.BRD_DIV;
                                    item.FILE_CATEGORY_SEQ = contract.BoardEntity.BRD_SEQ;
                                    item.CREATE_ID = contract.BoardEntity.CREATE_ID;
                                    item.UPDATE_ID = contract.BoardEntity.UPDATE_ID;
                                    item.USE_YN = "Y";
                                    item.DEL_YN = "N";
                                }
                            }

                            // 삭제파일 정보를 세팅한다.
                            FileEntity deleteFileEntity = new FileEntity();
                            deleteFileEntity.FILE_CATEGORY = contract.BoardEntity.BRD_CATEGORY;
                            deleteFileEntity.FILE_DIV = contract.BoardEntity.BRD_DIV;
                            deleteFileEntity.FILE_CATEGORY_SEQ = contract.BoardEntity.BRD_SEQ;
                            deleteFileEntity.UPDATE_ID = contract.BoardEntity.UPDATE_ID;

                            // 파일저장 진행
                            result = fileRepository.SaveFile(contract.FileContract, deleteFileEntity);
                        }
                    }
                    break;

                // 삭제
                case EnumProperties.GeneralFlag.DELETE:

                    result = DeleteBoard(contract.BoardEntity);
                    if (result)
                    {
                        // 삭제파일 정보를 세팅한다.
                        FileEntity deleteFileEntity = new FileEntity();
                        deleteFileEntity.FILE_CATEGORY = contract.BoardEntity.BRD_CATEGORY;
                        deleteFileEntity.FILE_DIV = contract.BoardEntity.BRD_DIV;
                        deleteFileEntity.FILE_CATEGORY_SEQ = contract.BoardEntity.BRD_SEQ;
                        deleteFileEntity.UPDATE_ID = contract.BoardEntity.BRD_WRITE_ID;

                        // 파일삭제 진행
                        fileRepository.DeleteFile(deleteFileEntity);
                    }
                    break;

                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// InsertBoard : 게시판 입력
        /// </summary>
        /// <param name="entity">게시판 엔티티</param>
        /// <returns></returns>
        private string InsertBoard(BoardEntity entity)
        {
            string result = "";
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

            string sResult = SqlHelper.GetReturnValue("SP_CMM_BOARD_C", keyValuePairs);
            if (!string.IsNullOrEmpty(sResult))
            {
                result = sResult;
                entity.BRD_SEQ = sResult;
            }
            return result;
        }

        /// <summary>
        /// UpdateBoard : 게시판 수정 
        /// </summary>
        /// <param name="entity">게시판 엔티티</param>
        /// <returns></returns>
        private bool UpdateBoard(BoardEntity entity) 
        {
            bool result = false;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_BRD_SEQ      ", entity.BRD_SEQ);
            keyValuePairs.Add("I_BRD_CATEGORY ", entity.BRD_CATEGORY);
            keyValuePairs.Add("I_BRD_DIV      ", entity.BRD_DIV);
            keyValuePairs.Add("I_BRD_TITLE    ", entity.BRD_TITLE);
            keyValuePairs.Add("I_BRD_CONTENTS ", entity.BRD_CONTENTS);
            keyValuePairs.Add("I_BRD_WRITE_ID ", entity.BRD_WRITE_ID);
            keyValuePairs.Add("I_UPDATE_ID    ", entity.UPDATE_ID);

            int uResult = SqlHelper.GetNonQuery("SP_CMM_BOARD_U", keyValuePairs);
            if (uResult > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// DeleteBoard
        /// </summary>
        /// <param name="entity">게시판 엔티티</param>
        /// <returns></returns>
        private bool DeleteBoard(BoardEntity entity) 
        {
            bool result = false;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();

            keyValuePairs.Add("I_BRD_SEQ      ", entity.BRD_SEQ);
            keyValuePairs.Add("I_BRD_CATEGORY ", entity.BRD_CATEGORY);
            keyValuePairs.Add("I_BRD_DIV      ", entity.BRD_DIV);
            keyValuePairs.Add("I_BRD_WRITE_ID ", entity.BRD_WRITE_ID);

            int dResult = SqlHelper.GetNonQuery("SP_CMM_BOARD_D", keyValuePairs);
            if (dResult > 0)
            {
                result = true;
            }

            return result;
        }
    }
}
