using System.Linq;
using Fzrain.Core.Data;
using Fzrain.Core.Domain.Logging;

namespace Fzrain.Service.Logging
{
    public class LoggingService:ILoggingService
    {
        private IRepository<Log> logRepository;
        public LoggingService(IRepository<Log> logRepository)
        {
            this.logRepository = logRepository;
        }

        public void WriteLog(Log log)
        {
            logRepository.Insert(log);
        }

        public IQueryable<Log> Logs(LogType logType)
        {
            return logRepository.Table.Where(l => l.LogType == logType);
        }
    }
}
