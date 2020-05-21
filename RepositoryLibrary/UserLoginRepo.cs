using EntityLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public class UserLoginRepo : IBaseRepository<UserLogin>
    {
        public void Delete(UserLogin obj)
        {
            throw new NotImplementedException();
        }

        public List<UserLogin> GetAll()
        {
            List<UserLogin> _UserLogin = new List<UserLogin>();
            try
            {
                using (NuevoDBConnStr myContext = new NuevoDBConnStr())
                {
                    myContext.Configuration.ProxyCreationEnabled = false;
                    myContext.Configuration.LazyLoadingEnabled = false;
                    myContext.Configuration.ValidateOnSaveEnabled = false;
                    myContext.Database.CommandTimeout = 360;
                    _UserLogin = myContext.UserLogin.ToList();

                }
            }
            catch (Exception ex)
            {
            }
            return _UserLogin;
        }

        public UserLogin GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public void Insert(UserLogin obj)
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

        public void Update(UserLogin obj)
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
        public UserLogin GetLogin(string UserName, string UserPassword)
        {
            UserLogin _UserLogin = new UserLogin();
            try
            {
                using (NuevoDBConnStr myContext = new NuevoDBConnStr())
                {
                    myContext.Configuration.ProxyCreationEnabled = false;
                    myContext.Configuration.LazyLoadingEnabled = false;
                    myContext.Configuration.ValidateOnSaveEnabled = false;
                    myContext.Database.CommandTimeout = 360;
                    _UserLogin = myContext.UserLogin.Where(p => p.UserName == UserName && p.UserPassword == UserPassword).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
            }
            return _UserLogin;
        }

        public void DeleteByID(int ID)
        {
            try
            {
                UserLogin ul = new UserLogin { ID = ID };
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







    }
}
