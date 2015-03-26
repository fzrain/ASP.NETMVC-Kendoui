using System.Data.Entity.ModelConfiguration;
using Fzrain.Core.Domain.Scheduler;

namespace Fzrain.Data.Mapping.Schedulers
{
    public class SchedulerMap : EntityTypeConfiguration<Scheduler>
    {
        public SchedulerMap()
        {
            HasKey(d => d.Id);
          
        }
    }
}
