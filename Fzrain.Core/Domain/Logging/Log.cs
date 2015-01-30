using System;
using Fzrain.Core.Domain.Permission;

namespace Fzrain.Core.Domain.Logging
{
    /// <summary>
    /// Represents a log record
    /// </summary>
    public partial class Log : BaseEntity
    {
        /// <summary>
        /// 日志等级
        /// </summary>
        public LogLevel LogLevel { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        public LogType LogType { get; set; }

        /// <summary>
        /// 日志信息
        /// </summary>
        public string Message { get; set; }
     
        /// <summary>
        /// 产生时间
        /// </summary>
        public DateTime SubTime { get; set; }     
        /// <summary>
        /// 相关用户
        /// </summary>
        public virtual User User { get; set; }
    }
}
