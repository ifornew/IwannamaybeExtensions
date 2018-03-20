using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions.Models
{
      /// <summary>
      /// 百度语音令牌刷新 模型
      /// </summary>
      public class TokenForSpeech
      {
            /// <summary>
            /// 令牌
            /// </summary>
            public string access_token { get; set; }

            /// <summary>
            /// 令牌过期时间
            /// </summary>
            public int expires_in { get; set; }

            /// <summary>
            /// 刷新令牌，有效期10年
            /// </summary>
            public string refresh_token { get; set; }

            /// <summary>
            /// 令牌授权范围
            /// </summary>
            public string scope { get; set; }

            /// <summary>
            /// 基于http调用Open API时所需要的Session Key，其有效期与Access Token一致
            /// </summary>
            public string session_key { get; set; }

            /// <summary>
            /// 基于http调用Open API时计算参数签名用的签名密钥。
            /// </summary>
            public string session_secret { get; set; }

            /// <summary>
            /// 错误码
            /// </summary>
            public string error { get; set; }

            /// <summary>
            /// 错误描述信息
            /// </summary>
            public string error_description { get; set; }
      }
}
