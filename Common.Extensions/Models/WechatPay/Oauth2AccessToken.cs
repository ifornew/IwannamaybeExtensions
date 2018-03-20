using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Extensions.Models.WechatPay
{
    /// <summary>
    /// OAuth2.0获取微信AccessToken和OpenId数据模型
    /// </summary>
    [Serializable]
    public class OAuth2AccessToken
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 安全令牌
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 过期时间（单位：秒）
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// 用于刷新的令牌
        /// </summary>
        public string refresh_token { get; set; }

        /// <summary>
        /// OpenId
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// Scope类型
        /// </summary>
        public string scope { get; set; }
    }
}