using Contract;
using Contract.ENUM;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMHR_WEB.Controllers
{
    public class ReplyController : BaseController
    {
        /// <summary>
        /// SaveReply : 댓글 저장
        /// </summary>
        /// <param name="contract">댓글 Contract</param>
        /// <param name="generalFlag">일반 플래그</param>
        /// <returns></returns>
        public JsonResult SaveReply(ReplyContract contract, EnumProperties.GeneralFlag generalFlag)
        {
            ReplyRepository repository = new ReplyRepository();
            bool result = repository.SaveReply(contract.ReplyEntity, generalFlag);
            return Json(new { RESULT = result }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// SelectReply : 댓글 조회
        /// </summary>
        /// <param name="contract">댓글 Contract</param>
        /// <returns></returns>
        public JsonResult SelectReply(ReplyContract contract)
        {
            ReplyRepository repository = new ReplyRepository();
            ReplyContract response = new ReplyContract();
            response.ReplyList = repository.SelectReplyList(contract.BRD_SEQ, contract.BRD_CATEGORY, contract.BRD_DIV);
            return Json(new { RESULT = response }, JsonRequestBehavior.AllowGet);
        }
    }
}