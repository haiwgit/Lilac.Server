using Lilac.LilacCore.Entities;
using Lilac.LilacCore.ServerSite;
using Lilac.LilacCore.Utility;
using Nefarian.Exchange;

namespace Lilac.LilacCore.ServerSite
{
    [ServiceErrorHandler]
    [OperationLog(Order = 50)]
    [FreeDBCurrentSession(Order = 99)]
    public  class LilacServiceBase : Nefarian.Core.ServiceBase
    {
        /// <summary>
        /// 模块交互中心
        /// </summary>
        public ExchangeCenter ExchangeCenter
        {
            get { return ExchangeHelper.GetExchange(); }
        }

        /// <summary>
        /// 当前登录用户的信息
        /// </summary>
        public UserInfo CurrentUser
        {
            get
            {
                return LilacRequestContext.Current<LilacRequestContext>().CurrentUser;
            }
            set
            {
                LilacRequestContext.Current<LilacRequestContext>().CurrentUser = value;
            }
        }

        /// <summary>
        /// 当前是否为客户端浏览器登录系统
        /// </summary>
        public bool IsClient
        {
            get { return LilacRequestContext.Current<LilacRequestContext>().IsClient; }
        }

        /// <summary>
        /// 当前是否为移动端登录系统
        /// </summary>
        public bool IsMobile
        {
            get { return LilacRequestContext.Current<LilacRequestContext>().IsMobile; }
        }
    }
}
