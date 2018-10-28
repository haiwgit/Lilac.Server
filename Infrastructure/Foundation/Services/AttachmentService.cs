using Lilac.Foundation.Contracts;
using Lilac.LilacCore.Entities;
using Lilac.LilacCore.Exchange;
using Lilac.LilacCore.ServerSite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lilac.Foundation.Services
{
    public class AttachmentService : LilacServiceBase, IAttachmentService, IAttachmentExchange
    {
        public List<Attachment> GetAttachments(string bizID)
        {
            return null;
        }
        public void DeleteAttachment(Attachment attch)
        {

        }
        public Stream DownloadAttachment(string path, string fileName = null)
        {
            return null;
        }
        public Stream DownloadAttachment(string fileName, Stream stream)
        {
            return null;
        }
        public byte[] TransAttachment(string fileName)
        {
            return null;
        }

        public string getstriing() {

            return null;
        }
    }
}
