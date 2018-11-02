using Lilac.LilacCore.Entities;
using Lilac.LilacCore.ServerSite;
using Lilac.LilacCore.Utility;
using Lilac.SystemManager.Contracts;
using Lilac.SystemManager.Entities;
using Nefarian.Core;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lilac.SystemManager.Services
{
    public class AuthService: LilacServiceBase,IAuthService
    {
        public const string AuthPagesKey = "__AuthPages";
        public User Login(string account, string password, string endpoint, Dictionary<string, string> properties, out string ticket)
        {
            ticket = null;
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
                return null;
            //账号不区分大小写
            account = account.ToUpper();
            //获取服务端加密后的密码
            string encryptedPassword = SHA1Helper.SHA1(password);
            ISession session = DBOperator.Instance.GetCurrentSession();
            User user = session.Query<User>()
                .Where(p => p.Account.ToUpper() == account && p.Password == encryptedPassword)
                .SingleOrDefault();
            if (user == null)
                //用户名或密码错误，登录失败
                return null;

            //客户端为每个用户创建一个票据，而移动端为每个设备创建一个票据
            //创建身份票据
            ticket = WebRequestContext.Current().CreateTicket();

          

            
          

            //url权限、菜单权限（node端使用）
            //操作权限（调用服务方法时使用）

            //设置当前登录系统的终端类型
            Session[LilacRequestContext.EndpointKey] = endpoint;

            //设置当前登录用户信息
            this.CurrentUser = new UserInfo()
            {
                UserID = user.ID,
                UserName = user.Name,
            };
            return user.UnWrapNHibernateClass();
        }
        public void Logout()
        {
            //注销身份票据
            LilacRequestContext.Current().SignOut();
        }

    }
}
