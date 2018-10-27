using log4net;
using Microsoft.Practices.Unity;
using Nefarian.Core;
using System;

namespace Lilac.LilacCore.Entities
{
    public static class Logger
    {
        /// <summary>
        /// 记录错误日志
        /// </summary>
        public static void Log(string msg, Exception ex)    //Error方法
        {
            IUnityContainer container = WebServiceSite.GetAppContainer();
            ILog log = container.Resolve<ILog>();
            log.Error(msg, ex);
        }
    }
}
