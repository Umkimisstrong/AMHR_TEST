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
using Contract.ENUM;

namespace Repository
{
    /// <summary>
    /// ProductRepository : Product 관련 DataAccess 담당
    /// </summary>
    public class ProductRepository
    {
        
        /// <summary>
        /// CheckPrdCode : 상품 코드 중복여부 체크
        /// </summary>
        /// <param name="PRD_CODE">상품 코드</param>
        /// <returns></returns>
        public bool CheckPrdCode(string PRD_CODE)
        {
            bool result = true;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_PRD_CODE", PRD_CODE);

            DataSet ds = SqlHelper.GetDataSet("SP_PRD_CODE_CHECK", keyValuePairs);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                result = string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0][0].ToString().Trim());
            }

            return result;
        }

        /// <summary>
        /// CreateProduct : 상품 마스터 입력
        /// </summary>
        /// <param name="entity">상품 엔티티</param>
        /// <returns></returns>
        public int CreateProduct(ProductEntity entity) 
        {
            int result = 0;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_PRD_CODE", entity.PRD_CODE);
            keyValuePairs.Add("I_PRD_CODE_NM", entity.PRD_CODE_NM);
            keyValuePairs.Add("I_PRD_TYPE_CODE", entity.PRD_TYPE_CODE);
            keyValuePairs.Add("I_PRD_TYPE_NM", entity.PRD_TYPE_NM);
            keyValuePairs.Add("I_PRD_PRICE", entity.PRD_PRICE);
            keyValuePairs.Add("I_USE_YN", entity.USE_YN);
            keyValuePairs.Add("I_DEL_YN", "N");
            keyValuePairs.Add("I_CREATE_ID", entity.CREATE_ID);
            keyValuePairs.Add("I_UPDATE_ID", entity.UPDATE_ID);

            result = SqlHelper.GetNonQuery("SP_PRD_PRODUCT_C", keyValuePairs);
            

            return result;
        }

        /// <summary>
        /// 상품 마스터 수정
        /// </summary>
        /// <param name="entity">상품 엔티티</param>
        /// <returns></returns>
        public int UpdateProduct(ProductEntity entity)
        {
            int result = 0;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_PRD_CODE", entity.PRD_CODE);
            keyValuePairs.Add("I_PRD_CODE_NM", entity.PRD_CODE_NM);
            keyValuePairs.Add("I_PRD_TYPE_CODE", entity.PRD_TYPE_CODE);
            keyValuePairs.Add("I_PRD_TYPE_NM", entity.PRD_TYPE_NM);
            keyValuePairs.Add("I_PRD_PRICE", entity.PRD_PRICE);
            keyValuePairs.Add("I_USE_YN", entity.USE_YN);
            keyValuePairs.Add("I_DEL_YN", "N");
            keyValuePairs.Add("I_UPDATE_ID", entity.UPDATE_ID);

            result = SqlHelper.GetNonQuery("SP_PRD_PRODUCT_U", keyValuePairs);
            

            return result;
        }

        /// <summary>
        /// 상품 마스터 삭제
        /// </summary>
        /// <param name="entity">상품 엔티티</param>
        /// <returns></returns>
        public int DeleteProduct(ProductEntity entity)
        {
            int result = 0;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_PRD_CODE", entity.PRD_CODE);
            keyValuePairs.Add("I_UPDATE_ID", entity.UPDATE_ID);

            result = SqlHelper.GetNonQuery("SP_PRD_PRODUCT_D", keyValuePairs);


            return result;
        }


        /// <summary>
        /// 상품 마스터 조회
        /// </summary>
        /// <param name="PRD_CODE">상품 코드</param>
        /// <returns></returns>
        public ProductEntity SelectProductEntity(string PRD_CODE)
        {
            ProductEntity entity = new ProductEntity();

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_PRD_CODE", PRD_CODE);

            DataSet ds = SqlHelper.GetDataSet("SP_PRD_PRODUCT_S", keyValuePairs);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                entity = ds.Tables[0].Rows[0].ConvertToEntity<ProductEntity>();
            }

            return entity;
        }

        /// <summary>
        /// SelectProductEntityList : 상품 코드 목록 조회
        /// </summary>
        /// <param name="PRD_CODE">상품 코드</param>
        /// <param name="PRD_CODE_NM">상품 코드명</param>
        /// <param name="PRD_TYPE_CODE">상품 타입 코드</param>
        /// <param name="PRD_TYPE_NM">상품 타입 명</param>
        /// <param name="START_NUMBER">조회시작 번호</param>
        /// <param name="ROW_COUNT">조회 행 수</param>
        /// <returns></returns>
        public ProductContract SelectProductEntityList(string PRD_CODE, string PRD_CODE_NM, string PRD_TYPE_CODE, string PRD_TYPE_NM, int START_NUMBER, int ROW_COUNT) 
        {
            ProductContract productContract = new ProductContract();
            List<ProductEntity> entityList = new List<ProductEntity>();

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_PRD_CODE", PRD_CODE);
            keyValuePairs.Add("I_PRD_CODE_NM", PRD_CODE_NM);
            keyValuePairs.Add("I_PRD_TYPE_CODE", PRD_TYPE_CODE);
            keyValuePairs.Add("I_PRD_TYPE_NM", PRD_TYPE_NM);
            keyValuePairs.Add("I_START_NUMBER", START_NUMBER);
            keyValuePairs.Add("I_ROW_COUNT", ROW_COUNT);


            DataSet ds = SqlHelper.GetDataSet("SP_PRD_PRODUCT_L", keyValuePairs);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                entityList = ds.Tables[0].ConverToEntityList<ProductEntity>();
            }

            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                productContract.TOTAL_COUNT = int.Parse(ds.Tables[1].Rows[0]["TOTAL_COUNT"].ToString());
            }

            productContract.ProductList = entityList;
            return productContract;
        }

        /// <summary>
        /// SaveProductEntity : 상품 저장
        /// </summary>
        /// <param name="entity">상품 엔티티</param>
        /// <param name="generalFlag">일반 플래그</param>
        /// <returns></returns>
        public string SaveProductEntity(ProductEntity entity, EnumProperties.GeneralFlag generalFlag)
        {
            string result = "N";

            switch (generalFlag)
            {
                case EnumProperties.GeneralFlag.CREATE:
                    result = CreateProduct(entity) > 0 ? "IY" : "IN";
                    break;
                case EnumProperties.GeneralFlag.UPDATE:
                    result = UpdateProduct(entity) > 0 ? "UY" : "UN";
                    break;
                case EnumProperties.GeneralFlag.DELETE:
                    result = DeleteProduct(entity) > 0 ? "DY" : "DN";
                    break;

                default: break;
            }

            return result;
        }
    }
}
