using EntityLibrary;
using Newtonsoft.Json; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        public ActionResult Login(UserLogin ul)
        {

            System.Web.HttpContext.Current.Session["IsLogin"] = false;
            System.Web.HttpContext.Current.Session["User"] = null;
            if (String.IsNullOrEmpty(ul.UserName) && String.IsNullOrEmpty(ul.UserPassword))
            {

            }
            else
            {
                string TokenJson = WebApiServiceClass.GetAPIToken(ul.UserName, ul.UserPassword);

                var UserLogin = WebApiServiceClass.GetLoginUser(ul.UserName, ul.UserPassword);

                UserLogin UserLoginList = JsonConvert.DeserializeObject<UserLogin>(UserLogin);
                if (UserLoginList != null)
                {
                    System.Web.HttpContext.Current.Session["User"] = UserLoginList;
                    return Redirect(Url.Content("/Login/Home"));
                }
            }
            return View();

        }





        public ActionResult NewUser(UserLogin ul)
        {

            if (!String.IsNullOrEmpty(ul.UserName) && !String.IsNullOrEmpty(ul.UserPassword))
            {
                WebApiServiceClass.NewUser(ul);
            }
            return View();

        }


        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session["User"] = null;
            MyScheduler.StopTimerList();
            return Redirect(Url.Content("/Login/Login"));
        }


        public ActionResult Home()
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
            {
                return Redirect(Url.Content("/Login/Login"));
            }

            UserLogin lu = new UserLogin();
            lu = (UserLogin)System.Web.HttpContext.Current.Session["User"];
            MyScheduler.SetTimer(lu.ID);
            return View();
        }



   









    }
}