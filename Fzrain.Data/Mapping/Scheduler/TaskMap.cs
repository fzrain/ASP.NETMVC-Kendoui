using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Fzrain.Core.Domain.Scheduler;


namespace Fzrain.Data.Mapping.Scheduler
{
    public class TaskMap : EntityTypeConfiguration<Task>
    {
        public TaskMap()
        {
            HasKey(d => d.Id);
          
        }
    }
}
