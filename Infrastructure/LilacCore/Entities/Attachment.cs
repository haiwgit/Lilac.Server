using System.Runtime.Serialization;

namespace Lilac.LilacCore.Entities
{
    [DataContract]
    public class Attachment
    {
        [DataMember]
        public string ID { get; set; }
    }
}
