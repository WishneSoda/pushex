using EntityLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public class DBLogger : ILogger
    {
        public string LogMessage { get; set; }
        private int? LoginUserID { get; set; }
        public DBLogger(int? _LoginUserID = 0)
        {
            LoginUserID = _LoginUserID;
        }
        public void LogMe()
        {
            try
            {
                ErrorLogs err = new ErrorLogs();
                err.CreateDate = DateTime.Now;
                err.UserLoginID = LoginUserID;
                err.LogMsg = LogMessage;
                ErrorLogsRepo errRepo = new ErrorLogsRepo();
                errRepo.Insert(err);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
