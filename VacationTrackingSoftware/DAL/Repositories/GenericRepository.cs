﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.IRepositories;
using BLL.Models;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected ProjectContext RepositoryContext;
        public GenericRepository(ProjectContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }
        public void Create(TEntity entity)
        {
            RepositoryContext.Set<TEntity>().Attach(entity);
            RepositoryContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            RepositoryContext.Set<TEntity>().Attach(entity);
            RepositoryContext.Set<TEntity>().Remove(entity);          
        }

        public IEnumerable<TEntity> GetAll()
        {
            return RepositoryContext.Set<TEntity>();
        }

        public TEntity GetById(int id)
        {
            return RepositoryContext.Set<TEntity>().Find(id);
        }

        public void Update(TEntity entity)
        {
            RepositoryContext.Set<TEntity>().Attach(entity);
            RepositoryContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task SaveAsync()
        {
          await RepositoryContext.SaveChangesAsync();
        }

        public void Save()
        {
            RepositoryContext.SaveChanges();
        }



    }

}