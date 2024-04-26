using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMHR_WEB.Models
{
    public class SignInViewModel
    {
        public string User_ID { get; set; }

        public string Password { get; set; }

        public string ErrorMessage { get; set; }
    }
}