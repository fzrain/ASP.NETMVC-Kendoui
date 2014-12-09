using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Fzrain.Core.Domain.Scheduler;


namespace Fzrain.Data.Mapping.Scheduler
{
    public class TaskMapping : EntityTypeConfiguration<Task>
    {
        public TaskMapping()
        {
            HasKey(d => d.Id);
          
        }
    }
}
