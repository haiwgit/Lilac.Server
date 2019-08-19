using Nefarian.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Lilac.LilacCore.Utility
{
    public class StreamHelper
    {
        public static void AppendFileName(Stream stream, string fileName)
        {
            fileName.ThrowIfEmptyString();
            stream.ThrowIfNull();
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
            WebOperationContext.Current.OutgoingResponse.ContentLength = stream.Length;
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "attachment;filename=" + Uri.EscapeUriString(fileName));
        }
    }
}
