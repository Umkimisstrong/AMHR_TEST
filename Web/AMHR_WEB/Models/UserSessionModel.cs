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
    }
}