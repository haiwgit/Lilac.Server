using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Lilac.Foundation.Contracts
{
    [ServiceContract]
    public interface IAttachmentService
    {
        [WebGet]
        string getname();
    }
}
