using Entity;
using System.Collections.Generic;

namespace Contract
{
    
    /// <summary>
    /// ReplyContract : 댓글 Contract
    /// </summary>
    public class ReplyContract : GlobalContract
    {
        /// <summary>
        /// REPLY_SEQ : 댓글 SEQ
        /// </summary>
        public int REPLY_SEQ { get; set; }
        /// <summary>
        /// REPLY_PARENT_SEQ : 댓글 상위 SEQ
        /// </summary>
        public int REPLY_PARENT_SEQ { get; set; }
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
        /// ReplyEntity : 댓글 엔티티
        /// </summary>
        public ReplyEntity ReplyEntity { get; set; }
        /// <summary>
        /// ReplyList : 댓글 엔티티의 집합
        /// </summary>
        public List<ReplyEntity> ReplyList { get; set; }


    }
}
