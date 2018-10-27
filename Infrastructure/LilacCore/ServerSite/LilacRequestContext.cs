using Lilac.LilacCore.Entities;

namespace Lilac.LilacCore.ServerSite
{
    public class LilacRequestContext : Nefarian.Core.WebRequestContext
    {
        /// <summary>
        /// 当前登录系统的终端类型Key
        /// </summary>
        public const string EndpointKey = "__Endpoint";

        /// <summary>
        /// 当前登录用户的信息
        /// </summary>
        public UserInfo CurrentUser
        {
            get
            {
                if (Session != null)
                    return Session["__CurrentUser"] as UserInfo;
                return null;
            }
            set
            {
                if (Session != null)
                    Session["__CurrentUser"] = value;
            }
        }

        /// <summary>
        /// 当前是否为客户端浏览器登录系统
        /// </summary>
        public bool IsClient
        {
            get { return object.Equals(Session[EndpointKey], "client"); }
        }

        /// <summary>
        /// 当前是否为移动端登录系统
        /// </summary>
        public bool IsMobile
        {
            get { return object.Equals(Session[EndpointKey], "mobile"); }
        }
    }
}
