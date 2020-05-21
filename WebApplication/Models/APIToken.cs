using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class APIToken
    {
        public APIToken()
        {
            IsSuccessStatusCode = false;
            Token = "";
            Error = "";
        }
        public bool IsSuccessStatusCode { get; set; }
        public string Token { get; set; }
        public string Error { get; set; }
    }
}