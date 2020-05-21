using EntityLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public class EmailSettingsRepo : IBaseRepository<EmailSettings>
    {
        public void Delete(EmailSettings obj)
        {
            throw new NotImplementedException();
        }

        public void DeleteByID(int ID)
        {
            try
            {
                EmailSettings ul = new EmailSettings { ID = ID };
                using (NuevoDBConnStr myContext = new NuevoDBConnStr())
                {
                    myContext.Entry(ul).State = EntityState.Deleted;
                    myContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public List<EmailSettings> GetAll()
        {
            List<EmailSettings> _EmailSettings = new List<EmailSettings>();
            try
            {
                using (NuevoDBConnStr myContext = new NuevoDBConnStr())
                {
                    myContext.Configuration.ProxyCreationEnabled = false;
                    myContext.Configuration.LazyLoadingEnabled = false;
                    myContext.Configuration.ValidateOnSaveEnabled = false;
                    myContext.Database.CommandTimeout = 360;
                    _EmailSettings = myContext.EmailSettings.ToList(); 
                }
            }
            catch (Exception ex)
            {
            }
            return _EmailSettings;
        }

        public EmailSettings GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public void Insert(EmailSettings obj)
        {
            try
            {
                using (NuevoDBConnStr myContext = new NuevoDBConnStr())
                {
                    myContext.Entry(obj).State = EntityState.Added;
                    myContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void Update(EmailSettings obj)
        {
            try
            {
                using (NuevoDBConnStr myContext = new NuevoDBConnStr())
                {
                    myContext.Entry(obj).State = EntityState.Modified;
                    myContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
