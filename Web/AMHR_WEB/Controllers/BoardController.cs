using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMHR_WEB.Controllers
{
    public class BoardController : BaseController
    {
        public ActionResult BoardList(BoardContract contract)
        { 
            
            return View();
        }
    }
}