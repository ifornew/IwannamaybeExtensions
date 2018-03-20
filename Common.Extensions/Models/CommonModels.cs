using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions.Models
{
    /// <summary>
    /// Http（1.1）请求方法
    /// </summary>
    public enum RequestMethod
    {
        /// <summary>
        /// Delete请求
        /// </summary>
        DELETE,
        /// <summary>
        /// Get请求
        /// </summary>
        GET,
        /// <summary>
        /// Head请求
        /// </summary>
        HEAD,
        /// <summary>
        /// Options请求
        /// </summary>
        OPTIONS,
        /// <summary>
        /// Post请求
        /// </summary>
        POST,
        /// <summary>
        /// Put请求
        /// </summary>
        PUT,
        /// <summary>
        /// Trace请求
        /// </summary>
        TRACE,
        /// <summary>
        /// CONNECT请求
        /// </summary>
        CONNECT,
    }

    /// <summary>
    /// 设备类型 枚举
    /// </summary>
    public enum ClientType
    {
        /// <summary>
        /// 其他设备
        /// </summary>
        Other,

        /// <summary>
        /// Android手机
        /// </summary>
        AndroidPhone,

        /// <summary>
        /// Android平板
        /// </summary>
        AndroidPad,

        /// <summary>
        /// Apple电脑
        /// </summary>
        Mac,

        /// <summary>
        /// Apple手机
        /// </summary>
        iPhone,

        /// <summary>
        /// Apple平板
        /// </summary>
        iPad,

        /// <summary>
        /// Windows电脑
        /// </summary>
        Windows,

        /// <summary>
        /// Windows平板
        /// </summary>
        WindowsPad,

        /// <summary>
        /// Windows手机
        /// </summary>
        WindowsPhone
    }
}
