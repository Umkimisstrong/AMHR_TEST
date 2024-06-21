using AMHR_WEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMHR_WEB.Controllers
{
    public class AttachFilesController : BaseController
    {
        public JsonResult Upload(HttpPostedFileBase files)
        {
            var resultList = new List<UploadFilesResult>();

            string path = Server.MapPath("~/Content/uploads");
            files.SaveAs(path + files.FileName);

            UploadFilesResult uploadfiles = new UploadFilesResult();
            uploadfiles.name = files.FileName;
            uploadfiles.size = files.ContentLength;
            uploadfiles.type = "image/jpeg";
            uploadfiles.url = "/Content/uploads/" + files.FileName;
            uploadfiles.thumbnailUrl = "/Content/uploads" + files.FileName;
            uploadfiles.deleteType = "GET";

            resultList.Add(uploadfiles);

            JsonFiles jFiles = new JsonFiles(resultList);

            return Json(jFiles);
            
        }

        public JsonResult Delete(string file)
        {
            System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/uploads/"), file));
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}