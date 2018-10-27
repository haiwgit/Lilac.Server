using Lilac.LilacCore.Utility;

namespace Lilac.LilacCore.ServerSite
{
    public sealed class FreeDBCurrentSessionAttribute : Nefarian.Core.MessageInspectorAttribute
    {
        public override void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            DBOperator.Instance.ReleaseCurrentSession();
        }
    }
}
