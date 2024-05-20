using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.ENUM
{
    /// <summary>
    /// EnumProperties : 열거형 전역 Contract
    /// </summary>
    public class EnumProperties
    {
        /// <summary>
        /// GeneralFlag : 일반Flag - 생성, 수정, 삭제
        /// </summary>
        public enum GeneralFlag
        {
              CREATE
            , UPDATE
            , DELETE
        }

        /// <summary>
        /// UserCreateTypeFlag : 사용자 생성 타입 Flag - 홈, 카카오, 네이버, 구글, 메타
        /// </summary>
        public enum UserCreateTypeFlag
        { 
              HOME
            , KAKAO
            , NAVER
            , GOOGLE
            , META
        }

        /// <summary>
        /// UserTypeFlag : 사용자 타입 Flag - 관리자, 일반, 상품판매, 상품관리, 클래스판매, 클래스관리
        /// </summary>
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
