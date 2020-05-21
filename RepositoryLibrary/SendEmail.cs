using EntityLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public class SendEmail : IMessage
    {
        private EmailSettings email = new EmailSettings();
        private int? LoginUserID = 0;
        public SendEmail(EmailSettings _email, int? _LoginUserID = 0)
        {
            email = _email;
            LoginUserID = _LoginUserID;
        }
        public void SendMessage(string msg)
        {
            try
            {
                if (email == null)
                {
                    DBLogger loger = new DBLogger(LoginUserID);
                    loger.LogMessage = "class SendEmail => SendMessage => Custom Error : Email Bulunamadı";
                    LogManager lm = new LogManager(loger);
                    lm.LogMe();
                    return;
                }
                SmtpClient SmtpServer = new SmtpClient(email.SmtpServer);
                var mail = new MailMessage();
                mail.From = new MailAddress(email.FromMail);
                mail.To.Add(email.ToMail);
                mail.Subject = email.MailSubject;
                mail.Body = msg;
                SmtpServer.Port = email.Port ?? 0;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(email.FromMail, email.FromMailPassword);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                DBLogger loger = new DBLogger(LoginUserID);
                loger.LogMessage = "class SendEmail => SendMessage => Exception : " + ex.Message;
                LogManager lm = new LogManager(loger);
                lm.LogMe();
            }
        }
    }
}
