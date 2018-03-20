using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Common.Extensions.Models.WechatPay
{
      /// <summary>
      /// 微信支付异步通知回复请求 对象
      /// </summary>
      [XmlRoot(ElementName = "xml")]
      public class PayNotifyRequest
      {
            /// <summary>
            /// 商户是否接收通知成功并校验成功
            /// </summary>
            public ReturnCode return_code { get; set; }

            /// <summary>
            /// 返回信息，如非空，为错误原因
            /// </summary>
            public string return_msg { get; set; }
      }

      /// <summary>
      /// 返回码类型
      /// </summary>
      public enum ReturnCode
      {
            /// <summary>
            /// 通信失败
            /// </summary>
            FAIL,
            /// <summary>
            /// 通信成功
            /// </summary>
            SUCCESS
      }
}