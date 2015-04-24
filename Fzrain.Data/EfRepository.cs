using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using Fzrain.Core;
using Fzrain.Core.Data;

namespace Fzrain.Data
{
    /// <summary>
    /// Entity Framework repository
    /// </summary>
    public partial class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IDbContext _context;
        private IDbSet<T> _entities;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public EfRepository(IDbContext context)
        {
            this._context = context;
        }

        public virtual T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public virtual void Insert(T entity)
        {

            this.Entities.Add(entity);

            this._context.SaveChanges();


        }

        public virtual void Update(T entity)
        {
            this._context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            this.Entities.Remove(entity);
            this._context.SaveChanges();
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        protected virtual IDbSet<T> Entities
        {
            get { return _entities ?? (_entities = _context.Set<T>()); }
        }
    }
}
