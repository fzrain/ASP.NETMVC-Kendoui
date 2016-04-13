using System.Linq;
using Fzrain.Core.Domain.Logging;

namespace Fzrain.Service.Logging
{
    interface  ILoggingService
    {
        void WriteLog(Log log);
        IQueryable<Log> Logs(LogType logType);
    }
}
