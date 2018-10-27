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
        [WebGet]
        string Login();
    }
}
