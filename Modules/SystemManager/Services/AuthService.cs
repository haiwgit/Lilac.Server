using Lilac.LilacCore.ServerSite;
using Lilac.SystemManager.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lilac.SystemManager.Services
{
    public class AuthService: LilacServiceBase,IAuthService
    {
        public string Login() {
            return "hello";
        }
    }
}
