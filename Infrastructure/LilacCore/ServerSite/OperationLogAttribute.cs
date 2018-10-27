using Lilac.LilacCore.Entities;
using Lilac.LilacCore.Utility;
using System.ServiceModel.Web;
using Nefarian.Core;
using NHibernate;
using System;

namespace Lilac.LilacCore.ServerSite
{
    public class OperationLogAttribute : MessageInspectorAttribute
    {
        public override object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request)
        {
            OperationLog log = null;
            if (LilacRequestContext.Current<LilacRequestContext>().CurrentUser != null)
            {
                log = new OperationLog()
                {
                    UserID = LilacRequestContext.Current<LilacRequestContext>().CurrentUser.UserID,
                    UserName = LilacRequestContext.Current<LilacRequestContext>().CurrentUser.UserName,
                    OperationTime = DateTime.Now,
                    Url = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.AbsolutePath,
                    RequestIP = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.BaseUri.DnsSafeHost,
                    UserAgent = WebOperationContext.Current.IncomingRequest.UserAgent,
                    Endpoint = LilacRequestContext.Current<LilacRequestContext>().IsClient ? "Client" : "Mobile"
                };
            }
            return log;
        }

        public override void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            OperationLog log = correlationState as OperationLog;
            if (log != null)
            {
                ISession session = DBOperator.Instance.GetCurrentSession();
                try
                {
                    session.Clear();//先清除已被Session管理的其他在服务方法中操作完成的实体，防止这里的Flush对之前的实体数据及状态造成影响
                    session.Save(log);
                    session.Flush();
                }
                catch (Exception ex)
                {
                    //操作日志出错不能影响功能
                    Logger.Log("操作日志insert出错。", ex);
                }
            }
        }
    }
}
