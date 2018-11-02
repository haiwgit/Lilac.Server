using Lilac.Foundation.Services;
using Lilac.LilacCore.Exchange;
using Lilac.LilacCore.Utility;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lilac.Foundation
{
    public class FoundationModule : Nefarian.Core.ModuleBase
    {
        public override void Initialize(IUnityContainer container)
        {
            //注册附件交互服务
            var exchange = ExchangeHelper.GetExchange();
            exchange.Register<IAttachmentExchange, AttachmentService>();
            IUnityContainer appContainer = Nefarian.Core.WebServiceSite.GetAppContainer();
        }
    }
}
