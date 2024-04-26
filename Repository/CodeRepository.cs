using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Contract;
using Entity;

namespace Repository
{
    
    public class CodeRepository
    {
        public CodeContract SelectCode(string SYS_CD, string DIV_CD)
        { 
            CodeContract Contract = new CodeContract();

            List<CodeEntity> CodeList = new List<CodeEntity>();

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("I_SYS_CD", SYS_CD);
            keyValuePairs.Add("I_DIV_CD", DIV_CD);

            DataSet ds = SqlHelper.GetDataSet("PR_CMM_SELECT_CODE", keyValuePairs);


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            { 
                foreach(DataRow item in ds.Tables[0].Rows )
                {
                    CodeEntity Entity = new CodeEntity();
                    Entity.CODE_ID = item["CODE_ID"].ToString();
                    Entity.CODE_NM = item["CODE_NM"].ToString();
                    Entity.SYS_CD = item["SYS_CD"].ToString();
                    Entity.DIV_CD = item["DIV_CD"].ToString();
                    Entity.CREATE_DT = DateTime.Parse(item["CREATE_DT"].ToString());

                    CodeList.Add(Entity);
                }

                
            }

            Contract.CodeList = CodeList;

            return Contract;
        }

    }
}
