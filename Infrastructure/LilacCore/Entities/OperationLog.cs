using System;

namespace Lilac.LilacCore.Entities
{
    public class OperationLog
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }

        /// <summary>
        /// 服务操作Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 浏览器标识
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 终端设备类型。客户端or移动端
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// 发起请求的ip地址
        /// </summary>
        public string RequestIP { get; set; }
    }
}
