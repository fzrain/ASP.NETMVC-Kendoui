using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fzrain.Core.Infrastructure;

namespace Fzrain.Data.Initializers
{
    public class MigrationsContextFactory : IDbContextFactory<FzrainContext>
    {
        public FzrainContext Create()
        {
            return (FzrainContext)EngineContext.Current.Resolve<IDbContext>();
        }
    }
}
