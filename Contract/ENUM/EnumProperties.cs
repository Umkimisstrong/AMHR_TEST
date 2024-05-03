using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.ENUM
{
    public class EnumProperties
    {
        public enum GeneralFlag
        {
              CREATE
            , UPDATE
            , DELETE
        }

        public enum UserCreateTypeFlag
        { 
              HOME
            , KAKAO
            , NAVER
            , GOOGLE
            , META
        }

        public enum UserTypeFlag
        { 
              ADM
            , GNL
            , PDS
            , PDM
            , CLS
            , CLM
        }

    }
}
