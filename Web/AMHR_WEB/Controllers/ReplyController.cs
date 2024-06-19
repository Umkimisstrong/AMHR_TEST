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
            contract.ReplyEntity.REPLY_WRITE_ID = UserSessionModel.USER_ID;
            contract.ReplyEntity.CREATE_ID = UserSessionModel.USER_ID;
            contract.ReplyEntity.UPDATE_ID = UserSessionModel.USER_ID;

            // CREATE / UPDATE / DELETE 모두 수행
            bool result = repository.SaveReply(contract.ReplyEntity, generalFlag);
            return Json(new { RESULT = result }, JsonRequestBehavior.AllowGet);
        }

    }
}