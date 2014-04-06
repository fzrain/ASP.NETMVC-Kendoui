using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fzrain.Core.Domain;

namespace Fzrain.Data.Initializers
{
    class FrameDataSeeder
    {
        private readonly IDbContext db;
        public FrameDataSeeder(IDbContext db)
        {
           this.db = db;
            InitData();
        }

        private void InitData()
        {
            db.Set<User>().Add(new User { UserName = "admin", Password = "123" });
            db.SaveChanges();
        }
    }
}
