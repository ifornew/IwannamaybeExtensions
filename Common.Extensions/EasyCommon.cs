using Common.Extensions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common.Extensions
{
    /// <summary>
    /// 通用扩展 类
    /// </summary>
    public static class EasyCommon
    {
        /// <summary>
        /// 代码锁
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <returns></returns>
        public static string MakeOrderNO()
        {
            lock (Locker)
            {
                return DateTime.Now.ToString("yyyyMMddHHmmss");
            }
        }

        /// <summary>
        /// 获取Http（1.1）请求方法
        /// </summary>
        /// <returns>Http（1.1）请求方法</returns>
        public static RequestMethod GetRequestMethod()
        {
            switch (HttpContext.Current.Request.HttpMethod)
            {
                case "DELETE":
                    return RequestMethod.DELETE;
                case "GET":
                    return RequestMethod.GET;
                case "HEAD":
                    return RequestMethod.HEAD;
                case "OPTIONS":
                    return RequestMethod.OPTIONS;
                case "POST":
                    return RequestMethod.POST;
                case "PUT":
                    return RequestMethod.PUT;
                case "TRACE":
                    return RequestMethod.TRACE;
                default:
                    return RequestMethod.TRACE;
            }
        }

        /// <summary>
        /// 判断文件类型
        /// </summary>
        /// <param name="mimeType">MIME</param>
        /// <returns>文件类型</returns>
        public static FileType getFileType(string mimeType)
        {
            mimeType = mimeType.ToLower();
            if (mimeType.Contains("audio"))
            {
                return FileType.Audio;
            }
            else if (mimeType.Contains("video"))
            {
                return FileType.Video;
            }
            else if (mimeType.Contains("image"))
            {
                return FileType.Image;
            }
            else if (mimeType.Contains("application/msword") || mimeType.Contains("application/vnd.openxmlformats-officedocument.wordprocessingml.document") || mimeType.Contains("application/vnd.ms-excel") || mimeType.Contains("application/vnd.ms-powerpoint") || mimeType.Contains("application/pdf") || mimeType.Contains("text/plain"))
            {
                return FileType.Doc;
            }
            else
            {
                return FileType.File;
            }
        }
    }
}
