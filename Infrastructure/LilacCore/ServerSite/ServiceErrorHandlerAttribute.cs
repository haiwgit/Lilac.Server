using Lilac.LilacCore.Entities;
using log4net;
using Microsoft.Practices.Unity;
using Nefarian.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lilac.LilacCore.ServerSite
{
    public sealed class ServiceErrorHandlerAttribute:ErrorHandlerAttribute
    {
        public override void OnError(Exception error)
        {
            //后期修改：异常拦截器也需要引用UnityContainer
            IUnityContainer container = WebServiceSite.GetAppContainer();
            ILog log = container.Resolve<ILog>();

            LogData data = new LogData();
            data.RequestMethod = "xxx";//request.Method;
            data.RequestUrl = "xxx";//request.RequestUri.AbsoluteUri;
            data.LogMessage = "请求出现错误";
            data.RequestData = "xxx";

            log.Error(data, error);
        }
    }
}
