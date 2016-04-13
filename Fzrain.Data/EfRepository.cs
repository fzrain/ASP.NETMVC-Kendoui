using System.Data.Entity;
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
            _context = context;
        }

        public virtual T GetById(object id)
        {
            return Entities.Find(id);
        }

        public virtual void Insert(T entity)
        {
            Entities.Add(entity);
            _context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            Entities.Remove(entity);
            _context.SaveChanges();
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }

        protected virtual IDbSet<T> Entities
        {
            get { return _entities ?? (_entities = _context.Set<T>()); }
        }
    }
}
