using EntityLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public class TargetAppsRepo : IBaseRepository<TargetApps>
    {
        public void Delete(TargetApps obj)
        {
            throw new NotImplementedException();
        }

        public void DeleteByID(int ID)
        {
            try
            {
                TargetApps ul = new TargetApps { ID = ID };
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

        public List<TargetApps> GetAll()
        {
            List<TargetApps> _TargetApps = new List<TargetApps>();
            try
            {
                using (NuevoDBConnStr myContext = new NuevoDBConnStr())
                {
                    myContext.Configuration.ProxyCreationEnabled = false;
                    myContext.Configuration.LazyLoadingEnabled = false;
                    myContext.Configuration.ValidateOnSaveEnabled = false;
                    myContext.Database.CommandTimeout = 360;
                    _TargetApps = myContext.TargetApps.ToList(); 
                }
            }
            catch (Exception ex)
            {
            }
            return _TargetApps;
        }

        public TargetApps GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public void Insert(TargetApps obj)
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

        public void Update(TargetApps obj)
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
