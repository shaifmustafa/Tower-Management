﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKTManagement.Repository.Base
{
    public abstract class Repository<T> where T:class
    {
        protected DbContext Db;

        protected DbSet<T> Table
        {
            get { return Db.Set<T>(); }
        }

        public Repository(DbContext db)
        {
            Db = db;
        }

        public virtual T GetById(Int64 id)
        {
            return Table.Find(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return Table;
        }

        public virtual bool Save(T entity)
        {
            Table.Add(entity);
            return Db.SaveChanges() > 0;
        }

        public virtual bool Update(T entity)
        {
            Table.Attach(entity);
            Db.Entry(entity).State = EntityState.Modified;
            return Db.SaveChanges() > 0;

        }

        public bool Remove(T entity)
        {
            Table.Remove(entity);
            return Db.SaveChanges() > 0;
        }

        public virtual bool SaveOrUpdate(T entity)
        {
            Table.AddOrUpdate(entity);
            return Db.SaveChanges() > 0;
        }
    }
}
