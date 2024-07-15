using Contract;
using Contract.ENUM;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using AMHR_WEB.Models;
using Newtonsoft.Json;

namespace AMHR_WEB.Controllers
{
    /// <summary>
    /// BoardController : 게시판 관련 Buisness Logic 담당
    /// </summary>
    public class BoardController : BaseController
    {
        /// <summary>
        /// START_NUMBER : 페이징 관련 시작 번호
        /// </summary>
        public const int START_NUMBER = 1;
        /// <summary>
        /// ROW_COUNT : 페이징 관련 조회 행 수
        /// </summary>
        public const int ROW_COUNT = 10;

        /// <summary>
        /// CONTROLLER_NAME : 전역변수 Controller Name
        /// </summary>
        public const string CONTROLLER_NAME = "Board";

        /// <summary>
        /// BoardList : 게시판 목록 뷰 담당
        /// </summary>
        /// <param name="contract">게시판 Contract</param>
        /// <returns></returns>
        public ActionResult BoardList(BoardContract contract)
        {
            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            ViewBag.SECOND_BREADCRUMB_NAME = "BoardList";
            int REQUEST_PAGE_NUMBER = (contract.PAGE_NUMBER == 0 ? START_NUMBER - 1 : (contract.PAGE_NUMBER - 1) * ROW_COUNT);
            int NOW_PAGE_NUMBER = contract.PAGE_NUMBER == 0 ? START_NUMBER : contract.PAGE_NUMBER;

            BoardContract response = new BoardContract();
            BoardRepository repository = new BoardRepository();
            response = repository.SelectBoardEntityList(contract.BRD_CATEGORY, contract.BRD_DIV, contract.BRD_TITLE, contract.BRD_CONTENTS, contract.BRD_WRITE_NM, contract.BRD_WRITE_START_DT, contract.BRD_WRITE_END_DT, contract.BRD_PICK_DT
                                                     ,  REQUEST_PAGE_NUMBER, ROW_COUNT);
            response.BRD_CATEGORY = contract.BRD_CATEGORY;
            response.BRD_DIV = contract.BRD_DIV;

            // 5       = 55 / 10
            int PAGE_COUNT = response.TOTAL_COUNT / ROW_COUNT;
            //         = 10 * 5 < 55 ? PAGE_COUNT+1
            PAGE_COUNT = (ROW_COUNT * PAGE_COUNT < response.TOTAL_COUNT) ? PAGE_COUNT + 1 : PAGE_COUNT;
            ViewBag.PAGE_COUNT = PAGE_COUNT;

            // START = 0, ELSE = 1, 2, 3, ...
            ViewBag.NOW_PAGE_NUMBER = NOW_PAGE_NUMBER;
            ViewBag.TOTAL_COUNT = response.TOTAL_COUNT;

            return View(response);
        }

        /// <summary>
        /// BoardSave : 게시판 저장 뷰 담당
        /// </summary>
        /// <param name="contract">게시판 Contract</param>
        /// <returns></returns>
        public ActionResult BoardSave(BoardContract contract)
        {
            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            ViewBag.SECOND_BREADCRUMB_NAME = "BoardSave";
            BoardContract response = new BoardContract();

            // 파일정보를 가져갈 FileContract 선언
            FileContract fileContract = new FileContract();

            if (contract != null && !string.IsNullOrEmpty(contract.BRD_SEQ))
            {
                // 수정
                ViewBag.GENERAL_FLAG = EnumProperties.GeneralFlag.UPDATE;

                // 게시물 조회
                BoardRepository repository = new BoardRepository();
                response.BoardEntity = repository.SelectBoardEntity(contract.BRD_SEQ, contract.BRD_CATEGORY, contract.BRD_DIV);

                // 파일 조회
                FileRepository fileRepository = new FileRepository();
                fileContract.FileList = fileRepository.SelectFileList(contract.BRD_CATEGORY, contract.BRD_DIV, contract.BRD_SEQ);

            }
            else
            {
                ViewBag.GENERAL_FLAG = EnumProperties.GeneralFlag.CREATE;
                response.BoardEntity = new BoardEntity();
                response.BoardEntity.BRD_CATEGORY = contract.BRD_CATEGORY;
                response.BoardEntity.BRD_DIV = contract.BRD_DIV;

                
            }

            // View 에 가져갈 모델에 FileContract 를 담아서 return 되게 한다.
            response.FileContract = fileContract;

            return View(response);
        }

        /// <summary>
        /// BoardDetail : 게시판 상세
        /// </summary>
        /// <param name="contract">게시판 Contract</param>
        /// <returns></returns>
        public ActionResult BoardDetail(BoardContract contract)
        {
            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            ViewBag.SECOND_BREADCRUMB_NAME = "BoardDetail";
            BoardContract response = new BoardContract();
            BoardRepository repository = new BoardRepository();

            // 파일정보를 가져갈 FileContract 선언
            FileContract fileContract = new FileContract();
            ReplyRepository replyRepository = new ReplyRepository();
            response.ReplyList = replyRepository.SelectReplyList(contract.BRD_SEQ, contract.BRD_CATEGORY, contract.BRD_DIV);
            response.BoardEntity = repository.SelectBoardEntity(contract.BRD_SEQ, contract.BRD_CATEGORY, contract.BRD_DIV);

            // 파일정보 조회
            FileRepository fileRepository = new FileRepository();
            fileContract.FileList = fileRepository.SelectFileList(contract.BRD_CATEGORY, contract.BRD_DIV, contract.BRD_SEQ);

            // View 에 가져갈 모델에 FileContract 를 담아서 return 되게 한다.
            response.FileContract = fileContract;


            return View(response);
        }

        /// <summary>
        /// SaveBoard : 게시판 저장
        /// </summary>
        /// <param name="contract">게시판 Contract</param>
        /// <param name="generalFlag">일반 플래그</param>
        /// <returns></returns>
        public ActionResult SaveBoard(BoardContract contract, EnumProperties.GeneralFlag generalFlag)
        {
            BoardRepository repository = new BoardRepository();
            contract.BoardEntity.CREATE_ID = UserSessionModel.USER_ID;
            contract.BoardEntity.UPDATE_ID = UserSessionModel.USER_ID;
            contract.BoardEntity.BRD_WRITE_ID = UserSessionModel.USER_ID;
            contract.BoardEntity.USE_YN = "Y";
            contract.BoardEntity.DEL_YN = "N";

            // 저장버튼 클릭 시 FileContract 정보가 있다면
            if (contract.FileContract != null)
            {
                // Upload 된 파일이 있다면
                if (!string.IsNullOrEmpty(contract.FileContract.UploadFileString))
                {
                    contract.FileContract.UploadFileList = JsonConvert.DeserializeObject<List<FileEntity>>(contract.FileContract.UploadFileString);
                }
                // Delete 된 파일이 있다면
                if (!string.IsNullOrEmpty(contract.FileContract.DeleteFileString))
                {
                    contract.FileContract.DeleteFileList = JsonConvert.DeserializeObject<List<FileEntity>>(contract.FileContract.DeleteFileString);
                }
                // 수정이라면
                if (generalFlag.Equals(EnumProperties.GeneralFlag.UPDATE))
                {
                    // 기존파일 조회
                    FileRepository fileRepository = new FileRepository();
                    contract.FileContract.ExistFileList = fileRepository.SelectFileList(contract.BoardEntity.BRD_CATEGORY, contract.BoardEntity.BRD_DIV, contract.BoardEntity.BRD_SEQ);
                }

            }


            repository.SaveBoard(contract, generalFlag);

            return RedirectToAction("BoardDetail", new { 
                    contract.BoardEntity.BRD_SEQ,
                    contract.BoardEntity.BRD_CATEGORY,
                    contract.BoardEntity.BRD_DIV
            });
        }

        /// <summary>
        /// DeleteBoard : 게시판 삭제
        /// </summary>
        /// <param name="contract">게시판 Contract</param>
        /// <returns></returns>
        public JsonResult DeleteBoard(BoardContract contract)
        {
            BoardRepository repository = new BoardRepository();
            contract.BoardEntity.BRD_WRITE_ID = UserSessionModel.USER_ID;
            
            bool result = repository.SaveBoard(contract, EnumProperties.GeneralFlag.DELETE);

            return Json(new { RESULT = result }, JsonRequestBehavior.AllowGet);
        }
    }
}