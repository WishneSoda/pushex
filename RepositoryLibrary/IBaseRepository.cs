using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public interface IBaseRepository<T> where T : class
    {
        List<T> GetAll(); 
        T GetByID(int ID); 
        void Insert(T obj); 
        void Update(T obj);
        void Delete(T obj);
        void DeleteByID(int ID);
    }
}
