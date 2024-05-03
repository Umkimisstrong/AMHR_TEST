using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMHR_WEB.Models
{
    public class UserSessionModel
    {
        
        public Guid UserId { get; set; }

        public string DisplayName { get; set; }
        public string USER_ID { get; set; }
        public string USER_NM { get; set; }
        public string USER_TYPE { get; set; }
        public string USER_EMAIL { get; set; }
        public string USER_DESCRIPTION { get; set; }
        public string USER_CREATE_TYPE { get; set; }
        public string USE_YN { get; set; }
        public string DEL_YN { get; set; }
    }
}