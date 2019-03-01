using ERPManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERPManager.DataAccess.Abstract
{
    public interface IRepository
    {
        System.Linq.IQueryable<T> All<T>() where T : Entity, new();
        IQueryable<T> Where<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : Entity, new();
        T Single<T>(Expression<Func<T, bool>> expression) where T : Entity, new();
        void Delete<T>(Expression<Func<T, bool>> expression) where T : Entity, new();
        void Add<T>(T item) where T : Entity, new();
        void Add<T>(IEnumerable<T> items) where T : Entity, new();
        void Update<T>(T item) where T : Entity, new();
        bool CollectionExists<T>() where T : Entity, new();
    }
}
