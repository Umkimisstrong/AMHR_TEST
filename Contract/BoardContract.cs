using Entity;
using System.Collections.Generic;

namespace Contract
{
    
    /// <summary>
    /// BoardContract : 게시판 Contract
    /// </summary>
    public class BoardContract : GlobalContract
    {
        /// <summary>
        /// BRD_SEQ : 게시판 SEQ
        /// </summary>
        public string BRD_SEQ { get; set; }
        /// <summary>
        /// BRD_CATEGORY : 게시판 카테고리
        /// </summary>
        public string BRD_CATEGORY { get; set; }
        /// <summary>
        /// BRD_DIV : 게시판 분류
        /// </summary>
        public string BRD_DIV { get; set; }
        /// <summary>
        /// BRD_TITLE : 게시판 제목
        /// </summary>
        public string BRD_TITLE { get; set; }
        /// <summary>
        /// BRD_CONTENTS : 게시판 내용
        /// </summary>
        public string BRD_CONTENTS {  get; set; }
        /// <summary>
        /// BRD_WRITE_NM : 게시판 작성자 명
        /// </summary>
        public string BRD_WRITE_NM { get; set; }
        /// <summary>
        /// BRD_WRITE_START_DT : 게시판 작성 시작일
        /// </summary>
        public string BRD_WRITE_START_DT { get; set; }
        /// <summary>
        /// BRD_WRITE_END_DT : 게시판 작성 종료일
        /// </summary>
        public string BRD_WRITE_END_DT { get; set; }
        /// <summary>
        /// BRD_PICK_DT : 게시판 특정일
        /// </summary>
        public string BRD_PICK_DT { get; set; }
        /// <summary>
        /// BoardEntity : 게시판 엔티티(1개의 게시판 테이블)
        /// </summary>
        public BoardEntity BoardEntity { get; set; }
        /// <summary>
        /// BoardList : 게시판 엔티티의 집합
        /// </summary>
        public List<BoardEntity> BoardList { get; set; }

        public List<ReplyEntity> ReplyList { get; set; }


    }
}
