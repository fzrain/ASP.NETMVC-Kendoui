using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fzrain.Core.Data;
using Fzrain.Core.Domain.Scheduler;


namespace Fzrain.Service.Scheduler
{
    public class SchedulerTaskService
    {

        private readonly IRepository<Task> taskRepository;

        public SchedulerTaskService(IRepository<Task> taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        public virtual IQueryable<Task> GetAll()
        {
            return taskRepository.Table;
        }

        public virtual void Insert(Task task)
        {
            if (true)
            {
                if (string.IsNullOrEmpty(task.Title))
                {
                    task.Title = "";
                }
                taskRepository.Insert(task);
            }
        }

        public virtual void Update(Task task)
        {

            if (string.IsNullOrEmpty(task.Title))
            {
                task.Title = "";
            }

            taskRepository.Update(task);

        }

        public virtual void Delete(Task task)
        {
            taskRepository.Delete(task);
        }



    }
}
