using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common.Extensions.Models.WechatProgram
{
    /// <summary>
    /// 登录凭证获取SessionKey和OpenId接口 响应模型
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class Jscode2SessionResponse
    {
        /// <summary>
        /// 用户 OpenId
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 对用户数据进行加密签名的密钥
        /// </summary>
        public string session_key { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string errmsg { get; set; }
    }
}
