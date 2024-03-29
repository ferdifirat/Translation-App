﻿using AFS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AFS.Core
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        DbSet<T> dbSet { get; }
        List<T> GetList(Expression<Func<T, bool>> condition = null);
        T Get(Expression<Func<T, bool>> condition);
        T GetDetached(Expression<Func<T, bool>> condition);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
        bool SaveChanges();
    }
}
