using EntityLibrary;
using Newtonsoft.Json;
using RepositoryLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiApplication.Controllers
{
    public class LoginAPIController : ApiController
    {

        [HttpPost]
        [Authorize]
        public UserLogin GetLoginUser(dynamic param)
        {
            UserLogin _UserLogin = new UserLogin();
            try
            {
                UserLoginRepo rp = new UserLoginRepo();
                _UserLogin = rp.GetLogin(param.FilterParameter.UserName.ToString(), param.FilterParameter.UserPassword.ToString());
            }
            catch (Exception ex)
            {
                DBLogger loger = new DBLogger(Convert.ToInt32(param.FilterParameter.UserLoginID));
                loger.LogMessage = "Apı  => GetLoginUser => Exception : " + ex.Message;
                LogManager lm = new LogManager(loger);
                lm.LogMe();
            }
            return _UserLogin;
        }



        [HttpPost]
        public void AddUser(dynamic param)
        {
            //  UserLogin _UserLogin = new UserLogin();
            try
            {
                UserLoginRepo rp = new UserLoginRepo();
                UserLogin ul = new UserLogin();// JsonConvert.DeserializeObject<UserLogin>(param.FilterParameter);
                ul.UserName = param.FilterParameter.UserName.ToString();
                ul.UserPassword = param.FilterParameter.UserPassword.ToString();
                if (!string.IsNullOrEmpty(ul.UserName) && !string.IsNullOrEmpty(ul.UserPassword))
                {
                    rp.Insert(ul);
                }

            }
            catch (Exception ex)
            {
                DBLogger loger = new DBLogger(Convert.ToInt32(param.FilterParameter.UserLoginID));
                loger.LogMessage = "Apı  => AddUser => Exception : " + ex.Message;
                LogManager lm = new LogManager(loger);
                lm.LogMe();
            }
            // return _UserLogin;
        }


        [HttpPost]
        [Authorize]
        public List<UserLogin> GetUsers(dynamic param)
        {
            List<UserLogin> _UserLogin = new List<UserLogin>();
            try
            {
                UserLoginRepo rp = new UserLoginRepo();
                _UserLogin = rp.GetAll();
            }
            catch (Exception ex)
            {
                DBLogger loger = new DBLogger(Convert.ToInt32(param.FilterParameter.UserLoginID));
                loger.LogMessage = "Apı  => GetUsers => Exception : " + ex.Message;
                LogManager lm = new LogManager(loger);
                lm.LogMe();
            }
            return _UserLogin;
        }

        [HttpPost]
        [Authorize]
        public void DeleteUserByID(dynamic param)
        {
            List<UserLogin> _UserLogin = new List<UserLogin>();
            try
            {
                UserLoginRepo rp = new UserLoginRepo();
                int ID = Convert.ToInt32(param.FilterParameter.ID);
                rp.DeleteByID(ID);
            }
            catch (Exception ex)
            {
                DBLogger loger = new DBLogger(Convert.ToInt32(param.FilterParameter.UserLoginID));
                loger.LogMessage = "Apı  => DeleteUserByID => Exception : " + ex.Message;
                LogManager lm = new LogManager(loger);
                lm.LogMe();
            }

        }

        [HttpPost]
        [Authorize]
        public void UpdateUser(dynamic param)
        {
            List<UserLogin> _UserLogin = new List<UserLogin>();
            try
            {
                UserLoginRepo rp = new UserLoginRepo();
                UserLogin ul = new UserLogin();
                ul.ID = Convert.ToInt32(param.FilterParameter.ID);
                ul.UserName = param.FilterParameter.UserName.ToString();
                ul.UserPassword = param.FilterParameter.UserPassword.ToString();

                rp.Update(ul);
            }
            catch (Exception ex)
            {
                DBLogger loger = new DBLogger(Convert.ToInt32(param.FilterParameter.UserLoginID));
                loger.LogMessage = "Apı  => UpdateUser => Exception : " + ex.Message;
                LogManager lm = new LogManager(loger);
                lm.LogMe();
            }

        }



        [HttpPost]
        [Authorize]
        public List<TargetApps> GetTargetAppList(dynamic param)
        {
            List<TargetApps> _TargetApps = new List<TargetApps>();
            try
            {
                TargetAppsRepo rp = new TargetAppsRepo();
                _TargetApps = rp.GetAll().Where(p => p.UserLoginID == Convert.ToInt32(param.FilterParameter.UserLoginID)).ToList();
            }
            catch (Exception ex)
            {
                DBLogger loger = new DBLogger(Convert.ToInt32(param.FilterParameter.UserLoginID));
                loger.LogMessage = "Apı  => GetTargetAppList => Exception : " + ex.Message;
                LogManager lm = new LogManager(loger);
                lm.LogMe();
            }
            return _TargetApps;
        }


        [HttpPost]
        [Authorize]
        public void AddTargetApps(dynamic param)
        {
            try
            {
                TargetAppsRepo rp = new TargetAppsRepo();
                TargetApps ta = new TargetApps();
                ta.Name = param.FilterParameter.Name.ToString();
                ta.TargetUrl = param.FilterParameter.TargetUrl.ToString();
                ta.TimeInterval = Convert.ToInt32(param.FilterParameter.TimeInterval.ToString());
                ta.IntervalType = param.FilterParameter.IntervalType.ToString();
                ta.UserLoginID = Convert.ToInt32(param.FilterParameter.UserLoginID.ToString());

                if (!string.IsNullOrEmpty(ta.Name) && !string.IsNullOrEmpty(ta.TargetUrl) && !string.IsNullOrEmpty(ta.TimeInterval.ToString()) && !string.IsNullOrEmpty(ta.IntervalType) && !string.IsNullOrEmpty(ta.UserLoginID.ToString()))
                {
                    rp.Insert(ta);
                }
            }
            catch (Exception ex)
            {
                DBLogger loger = new DBLogger(Convert.ToInt32(param.FilterParameter.UserLoginID));
                loger.LogMessage = "Apı  => AddTargetApps => Exception : " + ex.Message;
                LogManager lm = new LogManager(loger);
                lm.LogMe();
            }
        }


        [HttpPost]
        [Authorize]
        public void UpdateTargetApps(dynamic param)
        {

            try
            {
                TargetAppsRepo rp = new TargetAppsRepo();
                TargetApps ta = new TargetApps();
                ta.ID = Convert.ToInt32(param.FilterParameter.ID.ToString());
                ta.Name = param.FilterParameter.Name.ToString();
                ta.TargetUrl = param.FilterParameter.TargetUrl.ToString();
                ta.TimeInterval = Convert.ToInt32(param.FilterParameter.TimeInterval.ToString());
                ta.IntervalType = param.FilterParameter.IntervalType.ToString();
                ta.UserLoginID = Convert.ToInt32(param.FilterParameter.UserLoginID.ToString());
                rp.Update(ta);
            }
            catch (Exception ex)
            {
                DBLogger loger = new DBLogger(Convert.ToInt32(param.FilterParameter.UserLoginID));
                loger.LogMessage = "Apı  => UpdateTargetApps => Exception : " + ex.Message;
                LogManager lm = new LogManager(loger);
                lm.LogMe();
            }

        }



        [HttpPost]
        [Authorize]
        public void DeleteTargetAppsByID(dynamic param)
        {
            try
            {
                TargetAppsRepo rp = new TargetAppsRepo();
                int ID = Convert.ToInt32(param.FilterParameter.ID);
                rp.DeleteByID(ID);
            }
            catch (Exception ex)
            {
                DBLogger loger = new DBLogger(Convert.ToInt32(param.FilterParameter.UserLoginID));
                loger.LogMessage = "Apı  => DeleteTargetAppsByID => Exception : " + ex.Message;
                LogManager lm = new LogManager(loger);
                lm.LogMe();
            }

        }




        [HttpPost]
        [Authorize]
        public List<EmailSettings> GetEmailSettingsList(dynamic param)
        {
            List<EmailSettings> _EmailSettings = new List<EmailSettings>();
            try
            {
                EmailSettingsRepo rp = new EmailSettingsRepo();
                _EmailSettings = rp.GetAll().Where(p => p.UserLoginID == Convert.ToInt32(param.FilterParameter.UserLoginID)).ToList();
            }
            catch (Exception ex)
            {
                DBLogger loger = new DBLogger(Convert.ToInt32(param.FilterParameter.UserLoginID));
                loger.LogMessage = "Apı  => GetEmailSettingsList => Exception : " + ex.Message;
                LogManager lm = new LogManager(loger);
                lm.LogMe();
            }
            return _EmailSettings;
        }

        [HttpPost]
        [Authorize]
        public void AddEmailSettings(dynamic param)
        {
            try
            {
                EmailSettingsRepo rp = new EmailSettingsRepo();
                EmailSettings ta = new EmailSettings();
                ta.FromMail = param.FilterParameter.FromMail.ToString();
                ta.FromMailPassword = param.FilterParameter.FromMailPassword.ToString();

                ta.ID = Convert.ToInt32(param.FilterParameter.ID.ToString());
                ta.MailSubject = param.FilterParameter.MailSubject.ToString();
                ta.Port = Convert.ToInt32(param.FilterParameter.Port.ToString());

                ta.SmtpServer = param.FilterParameter.SmtpServer.ToString();
                ta.ToMail = param.FilterParameter.ToMail.ToString();
                ta.UserLoginID = Convert.ToInt32(param.FilterParameter.UserLoginID.ToString());

                if (!string.IsNullOrEmpty(ta.FromMail) && !string.IsNullOrEmpty(ta.FromMailPassword) && !string.IsNullOrEmpty(ta.ToMail.ToString())
                    && !string.IsNullOrEmpty(ta.Port.ToString()) && !string.IsNullOrEmpty(ta.SmtpServer) && !string.IsNullOrEmpty(ta.UserLoginID.ToString()))
                {
                    rp.Insert(ta);
                }
            }
            catch (Exception ex)
            {
                DBLogger loger = new DBLogger(Convert.ToInt32(param.FilterParameter.UserLoginID));
                loger.LogMessage = "Apı  => AddEmailSettings => Exception : " + ex.Message;
                LogManager lm = new LogManager(loger);
                lm.LogMe();
            }
        }

        [HttpPost]
        [Authorize]
        public void UpdateEmailSettings(dynamic param)
        {
            try
            {
                EmailSettingsRepo rp = new EmailSettingsRepo();
                EmailSettings ta = new EmailSettings();
                ta.FromMail = param.FilterParameter.FromMail.ToString();
                ta.FromMailPassword = param.FilterParameter.FromMailPassword.ToString();

                ta.ID = Convert.ToInt32(param.FilterParameter.ID.ToString());
                ta.MailSubject = param.FilterParameter.MailSubject.ToString();
                ta.Port = Convert.ToInt32(param.FilterParameter.Port.ToString());

                ta.SmtpServer = param.FilterParameter.SmtpServer.ToString();
                ta.ToMail = param.FilterParameter.ToMail.ToString();
                ta.UserLoginID = Convert.ToInt32(param.FilterParameter.UserLoginID.ToString());
                if (!string.IsNullOrEmpty(ta.FromMail) && !string.IsNullOrEmpty(ta.FromMailPassword) && !string.IsNullOrEmpty(ta.ToMail.ToString())
                    && !string.IsNullOrEmpty(ta.Port.ToString()) && !string.IsNullOrEmpty(ta.SmtpServer) && !string.IsNullOrEmpty(ta.UserLoginID.ToString()))
                {
                    rp.Update(ta);
                }
            }
            catch (Exception ex)
            {
                DBLogger loger = new DBLogger(Convert.ToInt32(param.FilterParameter.UserLoginID));
                loger.LogMessage = "Apı  => UpdateEmailSettings => Exception : " + ex.Message;
                LogManager lm = new LogManager(loger);
                lm.LogMe();
            }

        }

        [HttpPost]
        [Authorize]
        public void DeleteEmailSettingsByID(dynamic param)
        {
            try
            {
                EmailSettingsRepo rp = new EmailSettingsRepo();
                int ID = Convert.ToInt32(param.FilterParameter.ID);
                rp.DeleteByID(ID);
            }
            catch (Exception ex)
            {
                DBLogger loger = new DBLogger(Convert.ToInt32(param.FilterParameter.UserLoginID));
                loger.LogMessage = "Apı  => DeleteEmailSettingsByID => Exception : " + ex.Message;
                LogManager lm = new LogManager(loger);
                lm.LogMe();
            }

        }

        [HttpPost]
        [Authorize]
        public List<ErrorLogs> GetErrorLogsList(dynamic param)
        {
            List<ErrorLogs> _ErrorLogs = new List<ErrorLogs>();
            try
            {
                ErrorLogsRepo rp = new ErrorLogsRepo();
                _ErrorLogs = rp.GetAll().Where(p => p.UserLoginID == Convert.ToInt32(param.FilterParameter.UserLoginID)).ToList();
            }
            catch (Exception ex)
            {
                DBLogger loger = new DBLogger(Convert.ToInt32(param.FilterParameter.UserLoginID));
                loger.LogMessage = "Apı  => GetErrorLogsList => Exception : " + ex.Message;
                LogManager lm = new LogManager(loger);
                lm.LogMe();
            }
            return _ErrorLogs;
        }

    }
}
