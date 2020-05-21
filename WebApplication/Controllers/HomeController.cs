using EntityLibrary;
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
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult TargetApps()
        {
            return View();
        }

        public ActionResult NewTargetApp()
        {
            return View();
        }


        public ActionResult Users()
        {
            return View();
        }


        [HttpPost]
        public string GetUsers()
        {
            StreamReader maReader = new StreamReader(Request.InputStream);
            string strOBJ = maReader.ReadToEnd();
            dynamic dData = (dynamic)JsonConvert.DeserializeObject(strOBJ);

            string Records = WebApiServiceClass.UsersList();

            return Records;
        }


        [HttpPost]
        public void DeleteUsers()
        {
            StreamReader maReader = new StreamReader(Request.InputStream);
            string strOBJ = maReader.ReadToEnd();
            dynamic dData = (dynamic)JsonConvert.DeserializeObject(strOBJ);

            try
            {
                int ID = Convert.ToInt32(dData.ID.Value);
                string Records = WebApiServiceClass.DeleteUserByID(ID);
            }
            catch (Exception ex)
            {

            }


        }


        [HttpPost]
        public void SaveUsers()
        {
            StreamReader maReader = new StreamReader(Request.InputStream);
            string strOBJ = maReader.ReadToEnd();
            dynamic dData = (dynamic)JsonConvert.DeserializeObject(strOBJ);

            try
            {
                int ID = Convert.ToInt32(dData.JsonForm.ID);

                UserLogin u = new UserLogin();
                u.ID = ID;
                u.UserName = dData.JsonForm.UserName.ToString();
                u.UserPassword = dData.JsonForm.UserPassword.ToString();

                if (ID == 0)
                {
                    WebApiServiceClass.NewUser(u);
                }
                else
                {
                    WebApiServiceClass.UpdateUser(u);
                }

            }
            catch (Exception ex)
            {

            }


        }



        [HttpPost]
        public string GetTargetAppList()
        {
            StreamReader maReader = new StreamReader(Request.InputStream);
            string strOBJ = maReader.ReadToEnd();
            dynamic dData = (dynamic)JsonConvert.DeserializeObject(strOBJ);

            string Records = WebApiServiceClass.GetTargetAppList();

            return Records;
        }

        [HttpPost]
        public void SaveTargetApps()
        {
            StreamReader maReader = new StreamReader(Request.InputStream);
            string strOBJ = maReader.ReadToEnd();
            dynamic dData = (dynamic)JsonConvert.DeserializeObject(strOBJ);

            try
            {
                int ID = Convert.ToInt32(dData.JsonForm.ID);

                TargetApps u = new TargetApps();
                u.ID = ID;
                u.Name = dData.JsonForm.Name.ToString();
                u.IntervalType = dData.JsonForm.IntervalType.ToString();
                u.TargetUrl = dData.JsonForm.TargetUrl.ToString();
                u.TimeInterval = Convert.ToInt32(dData.JsonForm.TimeInterval);
                u.UserLoginID = ((UserLogin)System.Web.HttpContext.Current.Session["User"]).ID;

                Uri uriResult;
                bool result = Uri.TryCreate(u.TargetUrl, UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if (!result)
                {
                    return;
                }

                if (ID == 0)
                {
                    WebApiServiceClass.NewTargetApps(u);
                }
                else
                {
                    WebApiServiceClass.UpdateTargetApps(u);
                }
                UserLogin lu = new UserLogin();
                lu = (UserLogin)System.Web.HttpContext.Current.Session["User"];
                
                MyScheduler.SetTimer(lu.ID);
            }
            catch (Exception ex)
            {

            }
        }



        [HttpPost]
        public void DeleteTargetApps()
        {
            StreamReader maReader = new StreamReader(Request.InputStream);
            string strOBJ = maReader.ReadToEnd();
            dynamic dData = (dynamic)JsonConvert.DeserializeObject(strOBJ);

            try
            {
                int ID = Convert.ToInt32(dData.ID.Value);
                string Records = WebApiServiceClass.DeleteTargetAppsByID(ID);
                UserLogin lu = new UserLogin();
                lu = (UserLogin)System.Web.HttpContext.Current.Session["User"];
                MyScheduler.SetTimer(lu.ID);
            }
            catch (Exception ex)
            {

            }


        }



         


        [HttpPost]
        public string GetEmailSettingsList()
        {
            StreamReader maReader = new StreamReader(Request.InputStream);
            string strOBJ = maReader.ReadToEnd();
            dynamic dData = (dynamic)JsonConvert.DeserializeObject(strOBJ);

            string Records = WebApiServiceClass.GetEmailSettingsList();

            return Records;
        }


        [HttpPost]
        public void SaveEmailSettings()
        {
            StreamReader maReader = new StreamReader(Request.InputStream);
            string strOBJ = maReader.ReadToEnd();
            dynamic dData = (dynamic)JsonConvert.DeserializeObject(strOBJ);

            try
            {
                int ID = Convert.ToInt32(dData.JsonForm.ID);

                EmailSettings u = new EmailSettings();
                u.ID = ID;
                u.FromMail = dData.JsonForm.FromMail.ToString();
                u.FromMailPassword = dData.JsonForm.FromMailPassword.ToString();
                u.MailSubject = dData.JsonForm.MailSubject.ToString();
                u.Port = Convert.ToInt32(dData.JsonForm.Port);
                u.SmtpServer = dData.JsonForm.SmtpServer.ToString();
                u.ToMail = dData.JsonForm.ToMail.ToString();
                u.UserLoginID = ((UserLogin)System.Web.HttpContext.Current.Session["User"]).ID;
                 
                if (ID == 0)
                {
                    WebApiServiceClass.NewEmailSettings(u);
                }
                else
                {
                    WebApiServiceClass.UpdateEmailSettings(u);
                }
                
            }
            catch (Exception ex)
            {

            }
        }


        [HttpPost]
        public void DeleteEmailSettings()
        {
            StreamReader maReader = new StreamReader(Request.InputStream);
            string strOBJ = maReader.ReadToEnd();
            dynamic dData = (dynamic)JsonConvert.DeserializeObject(strOBJ);
            try
            {
                int ID = Convert.ToInt32(dData.ID.Value);
                string Records = WebApiServiceClass.DeleteEmailSettingsByID(ID);                
            }
            catch (Exception ex)
            {

            }


        }

    }

}