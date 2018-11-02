using Lilac.SystemManager.Entities;
using Nefarian.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Lilac.SystemManager.Contracts
{
    [ServiceContract]
    public interface IAuthService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="endpoint">终端类型。客户端（client）或移动端（mobile）</param>
        /// <param name="properties">附加传递的一些信息</param>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [WebInvoke(Method = HttpMethod.POST, BodyStyle = WebMessageBodyStyle.Wrapped)]
        User Login(string account, string password, string endpoint, Dictionary<string, string> properties, out string ticket);

        /// <summary>
        /// 注销登录
        /// </summary>
        [WebGet]
        void Logout();
    }
}
