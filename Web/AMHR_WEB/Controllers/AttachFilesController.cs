using AMHR_WEB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMHR_WEB.Controllers
{
    /// <summary>
    /// AttachFilesController : 첨부파일 관련 컨트롤러
    /// </summary>
    public class AttachFilesController : BaseController
    {
        /// <summary>
        /// storageRoot : 기본 경로 베이스
        /// </summary>
        private string storageRoot = "D:\\AMHR_TEST\\AppFiles\\Uploads\\";
        /// <summary>
        /// MonthlyPath : 연 / 월 경로
        /// </summary>
        private string MonthlyPath = DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() +"\\";


        /// <summary>
        /// Upload : 물리파일 업로드
        /// </summary>
        /// <returns></returns>
        public JsonResult Upload()
        {
            var resultList = new List<UploadFilesResult>();
            string path = storageRoot + MonthlyPath;

            foreach (string strFileInfo in Request.Files)
            {
                HttpPostedFileBase files = Request.Files[strFileInfo];

                DirectoryInfo di = new DirectoryInfo(path);
                if (di.Exists == false)
                {
                    di.Create();
                }

                string serverFileNM = Guid.NewGuid().ToString();

                files.SaveAs(path + serverFileNM);

                UploadFilesResult uploadfiles = new UploadFilesResult();

                uploadfiles.FILE_NAME = files.FileName;
                uploadfiles.FILE_SERVER_NAME = serverFileNM;
                uploadfiles.FILE_SIZE = files.ContentLength;
                uploadfiles.FILE_PATH = MonthlyPath;
                uploadfiles.USE_YN = "Y";
                uploadfiles.DEL_YN = "N";
                uploadfiles.CREATE_ID = UserSessionModel.USER_ID;
                uploadfiles.UPDATE_ID = UserSessionModel.USER_ID;

                resultList.Add(uploadfiles);
            }

            return Json(resultList);
        }

        /// <summary>
        /// Find : 개별 파일 다운로드
        /// </summary>
        /// <param name="fileFolderPath">루트 경로를 제외한 폴더경로</param>
        /// <param name="fileName">파일명</param>
        /// <param name="serverFileName">서버파일명</param>
        /// <returns></returns>
        [Route("AttachFiles/Find/{fileFolderPath}/{fileName}/{serverFileName}")]
        public ActionResult Find(string fileFolderPath, string fileName, string serverFileName)
        {
            if (string.IsNullOrEmpty(fileFolderPath) || string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(serverFileName))
            {
                return HttpNotFound();
            }


            var filePath = Path.Combine(storageRoot, fileFolderPath, serverFileName);
            if (System.IO.File.Exists(filePath))
            {
                string contentType = fileName.Substring(fileName.LastIndexOf('.'));
                FileStreamResult result = new FileStreamResult(new System.IO.FileStream(filePath, System.IO.FileMode.Open, FileAccess.Read, FileShare.ReadWrite), contentType);
                result.FileDownloadName = fileName;
                return result;
            }
            else
            {
                return HttpNotFound();
            }
            
        }

        /// <summary>
        /// Delete : 물리파일삭제
        /// </summary>
        /// <param name="path">경로</param>
        /// <param name="serverFileNm">서버파일명</param>
        /// <returns></returns>
        public JsonResult Delete(string path, string serverFileNm)
        {
            System.IO.File.Delete(Path.Combine(path, serverFileNm));
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// SaveFile : 파일저장용 테스트 
        /// </summary>
        /// <param name="UploadFileString">Upload File 문자열</param>
        /// /// <param name="DeleteFileString">Delete File 문자열</param>
        public void SaveFile(string UploadFileString, string DeleteFileString) 
        {
            List<UploadFilesResult> obj =  JsonConvert.DeserializeObject<List<UploadFilesResult>>(UploadFileString);
            List<UploadFilesResult> obj2 = JsonConvert.DeserializeObject<List<UploadFilesResult>>(DeleteFileString);
            // 하위에 반복문을 통해 Insert 할 수 있다.
        }
    }
}