using Lilac.LilacCore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Lilac.LilacCore.Exchange
{
    [ServiceContract]
    public interface IAttachmentExchange
    {
        /// <summary>
        /// 获取附件列表
        /// </summary>
        /// <param name="bizid">附件关联的业务数据ID</param>
        /// <returns></returns>
        [WebGet]
        List<Attachment> GetAttachments(string bizID);

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="attch"></param>
        
        void DeleteAttachment(Attachment attch);

        /// <summary>
        /// 下载附件
        /// </summary>
        /// <param name="path">文件的绝对路径</param>
        /// <param name="fileName">可选的文件名</param>
        /// <returns></returns>
        Stream DownloadAttachment(string path, string fileName = null);

        /// <summary>
        /// 下载附件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="stream">文件流</param>
        /// <returns></returns>
        Stream DownloadAttachment(string fileName, Stream stream);

        /// <summary>
        /// 传输附件流
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        byte[] TransAttachment(string fileName);

      
    }
}
