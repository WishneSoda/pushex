using EntityLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public class ErrorLogsRepo : IBaseRepository<ErrorLogs>
    {
        public void Delete(ErrorLogs obj)
        {
            throw new NotImplementedException();
        }

        public void DeleteByID(int ID)
        {
            throw new NotImplementedException();
        }

        public List<ErrorLogs> GetAll()
        {
            List<ErrorLogs> _ErrorLogs = new List<ErrorLogs>();
            try
            {
                using (NuevoDBConnStr myContext = new NuevoDBConnStr())
                {
                    myContext.Configuration.ProxyCreationEnabled = false;
                    myContext.Configuration.LazyLoadingEnabled = false;
                    myContext.Configuration.ValidateOnSaveEnabled = false;
                    myContext.Database.CommandTimeout = 360;
                    _ErrorLogs = myContext.ErrorLogs.ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return _ErrorLogs;
           
        }

        public ErrorLogs GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public void Insert(ErrorLogs obj)
        {
            using (NuevoDBConnStr myContext = new NuevoDBConnStr())
            {
                myContext.Entry(obj).State = EntityState.Added;
                myContext.SaveChanges();
            }
        }

        public void Update(ErrorLogs obj)
        {
            throw new NotImplementedException();
        }
    }
}
