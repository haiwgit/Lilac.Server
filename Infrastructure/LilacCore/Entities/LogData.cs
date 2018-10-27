using System;

namespace Lilac.LilacCore.Entities
{
    public class LogData
    {
        /// <summary>
        /// 日志ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 日志信息
        /// </summary>
        public string LogMessage { get; set; }

        /// <summary>
        /// 异常发生时间
        /// </summary>
        public DateTime HappenedTime { get; set; }

        /// <summary>
        /// 请求的方法
        /// </summary>
        public string RequestMethod { get; set; }

        /// <summary>
        /// 发生异常时客户端请求的url
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// 发生异常时客户端请求包含的json数据
        /// </summary>
        public string RequestData { get; set; }

        /// <summary>
        /// 异常的详细信息
        /// </summary>
        public ExceptionInfo ExceptionDetail { get; set; }
    }

    public class ExceptionInfo
    {
        /// <summary>
        /// 异常类型（Exception.GetType()）
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 调用堆栈
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// 内部异常
        /// </summary>
        public ExceptionInfo InnerException { get; set; }

        public ExceptionInfo() { }

        public ExceptionInfo(Exception ex)
        {
            if (ex != null)
            {
                this.Message = ex.Message;
                this.Type = ex.GetType().ToString();
                this.StackTrace = ex.StackTrace;
                if (ex.InnerException != null)
                {
                    this.InnerException = new ExceptionInfo(ex.InnerException);
                }
            }
        }
    }

}
