using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Contract;
using Entity;
using Repository;
using System.Diagnostics.Contracts;

namespace Repository
{
    /// <summary>
    /// FileRepository : 파일 관련 DataAccess 담당
    /// </summary>
    public class FileRepository
    {
        /// <summary>
        /// SaveFile : 파일 저장 로직
        /// </summary>
        /// <param name="contract">FileContract - List 를 담아서 호출</param>
        /// <param name="deleteFileEntity">삭제할 파일 엔티티</param>
        /// <returns></returns>
        public bool SaveFile(FileContract contract, FileEntity deleteFileEntity)
        {
            // 01. 결과 반환용 bool 변수 선언
            bool result = false;

            // 02. 파일 저장에 필요한 List<FileEntity> 선언
            contract.FileList = new List<FileEntity>();

            // 03. Upload FileList - 신규파일 리스트가 있다면 contract.FileList 에 담는다.
            if (contract.UploadFileList != null && contract.UploadFileList.Count > 0)
            {
                contract.FileList = contract.UploadFileList;
            }

            // 04. Delete FileList - 삭제된 파일 리스트가 있다면 검증한다.
            if (contract.DeleteFileList != null && contract.DeleteFileList.Count > 0)
            { 
                // 05. Exist FileList - 기존에 올라온 파일이 있으면 기존파일과 비교한다.
                if(contract.ExistFileList != null && contract.ExistFileList.Count>0)
                {
                    // 05-1. 삭제된 파일 리스트의 개수와 기존에 올라온 파일 리스트의 개수가 동일하지 않을 경우
                    //       기존 파일 리스트와의 비교를 통해 삭제된 파일을 제거한다.
                    //       (소스코드상 자료구조 내부에서 제거 - List.RemoveAt(인덱스))
                    if (contract.DeleteFileList.Count != contract.ExistFileList.Count)
                    {
                        // 비교 시 인덱스 삭제에 따른 자료구조 변경 시 오류가 날 수 있으므로
                        // 각 리스트를 FILE_SEQ 순으로 정렬시킨다.
                        // 삭제 대상 리스트인 Exist FileList 반복문은 0 ~ 10이 아닌 10 ~ 0 역순으로 돈다.
                        contract.DeleteFileList = contract.DeleteFileList.OrderBy(x => x.FILE_SEQ).ToList();
                        contract.ExistFileList = contract.ExistFileList.OrderBy(x => x.FILE_SEQ).ToList();

                        for (int i = contract.ExistFileList.Count - 1; i >= 0; i--)
                        {
                            for (int j = 0; i < contract.DeleteFileList.Count; j++)
                            {
                                if (contract.ExistFileList[i].FILE_SEQ == contract.DeleteFileList[j].FILE_SEQ)
                                {
                                    contract.ExistFileList.RemoveAt(i); // 자료구조 내부 삭제
                                    break;
                                }
                            }
                        }
                    }
                    // 05-2. 삭제된 파일 리스트의 개수와 기존에 올라온 파일 리스트의 개수가 동일한 경우
                    //       기존 파일 리스트를 모두 비운다.(전부다 삭제되었다는 의미)
                    else
                    {
                        contract.ExistFileList.Clear();
                    }
                }
            }

            // 06. 기존파일리스트가 남아있다면 비교-삭제 이후의 남은 기존파일이나.
            //     혹은 삭제되지 않고 그대로 남아있는 기존파일 정보를 
            //     contract.FileList 에 담는다. 
            if (contract.ExistFileList != null && contract.ExistFileList.Count > 0)
            {
                for (int i = 0; i < contract.ExistFileList.Count; i++)
                {
                    FileEntity entity = new FileEntity();
                    entity = contract.ExistFileList[i];
                    contract.FileList.Add(entity);
                }
            }

            // 07. 파일 저장 전 관련된 파일 정보를 모두 N처리한다.
            DeleteFile(deleteFileEntity);

            // 08. 신규파일과 기존파일을 합친 List의 File 들을 Insert 한다.
            for (int i = 0; i < contract.FileList.Count; i++)
            {
                result = CreateFile(contract.FileList[i]);
            }

            // 09. 결과를 반환한다.
            return result;
        }

        /// <summary>
        /// CreateFile : 파일 저장
        /// </summary>
        /// <param name="entity">파일 엔티티</param>
        /// <returns></returns>
        public bool CreateFile(FileEntity entity)
        {
            bool result = false;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_FILE_NAME", entity.FILE_NAME);
	        keyValuePairs.Add("I_FILE_SERVER_NAME", entity.FILE_SERVER_NAME);
            keyValuePairs.Add("I_FILE_PATH", entity.FILE_PATH);
            keyValuePairs.Add("I_FILE_SIZE", entity.FILE_SIZE);
            keyValuePairs.Add("I_FILE_CATEGORY", entity.FILE_CATEGORY);
            keyValuePairs.Add("I_FILE_DIV", entity.FILE_DIV);
            keyValuePairs.Add("I_FILE_CATEGORY_SEQ", entity.FILE_CATEGORY_SEQ);
            keyValuePairs.Add("I_USE_YN", entity.USE_YN);
            keyValuePairs.Add("I_DEL_YN", entity.DEL_YN);
            keyValuePairs.Add("I_CREATE_ID", entity.CREATE_ID);
            keyValuePairs.Add("I_UPDATE_ID", entity.UPDATE_ID);
            

            int cResult = SqlHelper.GetNonQuery("SP_CMM_FILE_C", keyValuePairs);

            if (cResult > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// DeleteFile : 파일 삭제처리
        /// </summary>
        /// <param name="entity">파일 엔티티</param>
        /// <returns></returns>
        public void DeleteFile(FileEntity entity) 
        {

            if (!string.IsNullOrEmpty(entity.FILE_CATEGORY)
                && !string.IsNullOrEmpty(entity.FILE_DIV)
                && !string.IsNullOrEmpty(entity.FILE_CATEGORY_SEQ)
                )
            {
                Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();

                keyValuePairs.Add("I_FILE_CATEGORY", entity.FILE_CATEGORY);
                keyValuePairs.Add("I_FILE_DIV", entity.FILE_DIV);
                keyValuePairs.Add("I_FILE_CATEGORY_SEQ", entity.FILE_CATEGORY_SEQ);
                keyValuePairs.Add("I_UPDATE_ID", entity.UPDATE_ID);

                int dResult = SqlHelper.GetNonQuery("SP_CMM_FILE_D", keyValuePairs);

                if (dResult > 0)
                {
                    // void 이기 때문에 아무것도 안한다.
                }
            }
            
        }

        /// <summary>
        /// SelectFileList : 파일 조회
        /// </summary>
        /// <param name="I_FILE_CATEGORY">파일 카테고리</param>
        /// <param name="I_FILE_DIV">파일 분류</param>
        /// <param name="I_FILE_CATEGORY_SEQ">파일 카테고리 SEQ</param>
        /// <returns></returns>
        public List<FileEntity> SelectFileList(string I_FILE_CATEGORY, string I_FILE_DIV, string I_FILE_CATEGORY_SEQ)
        {
            List<FileEntity> result = new List<FileEntity>();

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();


            keyValuePairs.Add("I_FILE_CATEGORY", I_FILE_CATEGORY);
            keyValuePairs.Add("I_FILE_DIV", I_FILE_DIV);
            keyValuePairs.Add("I_FILE_CATEGORY_SEQ", I_FILE_CATEGORY_SEQ);

            DataSet ds = SqlHelper.GetDataSet("SP_CMM_FILE_L", keyValuePairs);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                result = ds.Tables[0].ConverToEntityList<FileEntity>();
            }

            return result;
        }
    }
}
