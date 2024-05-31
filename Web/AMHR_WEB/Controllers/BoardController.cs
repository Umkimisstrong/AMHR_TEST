using Contract;
using Contract.ENUM;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;

namespace AMHR_WEB.Controllers
{
    public class BoardController : BaseController
    {

        public const int START_NUMBER = 1;
        public const int ROW_COUNT = 10;

        /// <summary>
        /// CONTROLLER_NAME : 전역변수 Controller Name
        /// </summary>
        public const string CONTROLLER_NAME = "Board";
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

        public ActionResult BoardSave(BoardContract contract)
        {
            BoardContract response = new BoardContract();

            if (contract != null && !string.IsNullOrEmpty(contract.BRD_SEQ))
            {
                // 수정
                ViewBag.GENERAL_FLAG = EnumProperties.GeneralFlag.UPDATE;

                // 게시물 조회
                BoardRepository repository = new BoardRepository();
                response.BoardEntity = repository.SelectBoardEntity();




            }
            else
            {
                ViewBag.GENERAL_FLAG = EnumProperties.GeneralFlag.CREATE;
                response.BoardEntity = new BoardEntity();
            }

            return View(response);
        }
    }
}