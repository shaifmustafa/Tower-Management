using LKTManagement.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKTManagement.BLL.Base
{
    public class Manager<T> where T:class
    {
        protected Repository<T> Repository;


        public Manager(Repository<T> repository)
        {
            Repository = repository;
        }

        public virtual IQueryable<T> GetAll()
        {
            return Repository.GetAll();
        }

        public virtual T GetById(Int64 id)
        {
            return Repository.GetById(id);
        }

        public virtual bool Save(T entity)
        {
            return Repository.Save(entity);
        }

        public virtual bool Update(T entity)
        {
            return Repository.Update(entity);
        }
        public bool Remove(T entity)
        {
            return Repository.Remove(entity);
        }

        public virtual bool SaveOrUpdate(T entity)
        {
            return Repository.SaveOrUpdate(entity);
        }
    }
}
