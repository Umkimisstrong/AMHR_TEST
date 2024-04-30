using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMHR_WEB.Models
{
    public class CustomErrorModel
    {
        public string status { get; set; }
        public string statusText { get; set; }

        public string responseText { get; set; }
    }
}