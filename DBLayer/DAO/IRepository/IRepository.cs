using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.DAO.IRepository
{
    public interface IRepository<T> where T : class
    {
        //CRUD
        public Task<T> Create(T entity);
        public Task<T> Get(int id);
        public Task<T> Update(T entity); 
        public Task<bool> Delete(T entity);


    }
}
