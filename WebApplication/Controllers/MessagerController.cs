using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class MessagerController : Controller
    {
        // GET: Messager
        public ActionResult EmailSettings()
        {
            return View();
        }
    }
}