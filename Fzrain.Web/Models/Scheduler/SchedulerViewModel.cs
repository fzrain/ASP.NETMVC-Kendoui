using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fzrain.Core.Domain.Scheduler;
using Kendo.Mvc.UI;

namespace Fzrain.Web.Models.Scheduler
{
    public class SchedulerViewModel : ISchedulerEvent
    {
        public int SchedulerID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public string StartTimezone { get; set; }   
        public DateTime End{ get; set; }
        public string EndTimezone { get; set; }
        public string RecurrenceRule { get; set; }
        public int? RecurrenceID { get; set; }
        public string RecurrenceException { get; set; }
        public bool IsAllDay { get; set; }
        public int SchedulerType { get; set; }
      
    }
   
}