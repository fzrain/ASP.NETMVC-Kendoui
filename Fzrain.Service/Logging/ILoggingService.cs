using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fzrain.Core.Domain.Logging;

namespace Fzrain.Service.Logging
{
    interface  ILoggingService
    {
        void WriteLog(Log log);
        IQueryable<Log> Logs(LogType logType);
    }
}
