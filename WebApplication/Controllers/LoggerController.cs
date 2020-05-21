using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class LoggerController : Controller
    {
        // GET: Logger
        public ActionResult ErrorLogs()
        {
            return View();
        }

        [HttpPost]
        public string GetErrorLogsList()
        {
            StreamReader maReader = new StreamReader(Request.InputStream);
            string strOBJ = maReader.ReadToEnd();
            dynamic dData = (dynamic)JsonConvert.DeserializeObject(strOBJ); 
            string Records = WebApiServiceClass.GetErrorLogsList(); 
            return Records;
        }

        



    }
}