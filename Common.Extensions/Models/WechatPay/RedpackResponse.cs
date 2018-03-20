using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Common.Extensions.Models.WechatPay
{
      /// <summary>
      /// 微信红包响应 对象
      /// </summary>
      [XmlRoot(ElementName = "xml")]
      public class RedpackResponse
      {
            /// <summary>
            /// SUCCESS/FAIL此字段是通信标识，非交易标识，交易是否成功需要查看result_code来判断
            /// </summary>
            public ReturnCode return_code { get; set; }
            /// <summary>
            /// 返回信息，如非空，为错误原因  签名失败  参数格式校验错误
            /// </summary>
            public string return_msg { get; set; }
            /// <summary>
            /// 签名
            /// </summary>
            public string sign { get; set; }
            /// <summary>
            /// 业务结果SUCCESS/FAIL
            /// </summary>
            public ReturnCode result_code { get; set; }
            /// <summary>
            /// 错误码信息
            /// </summary>
            public RedpackErrorCode err_code { get; set; }
            /// <summary>
            /// 错误代码描述
            /// </summary>
            public string err_code_des { get; set; }

            /// <summary>
            /// 商户订单号，需保持唯一性
            /// </summary>
            public string mch_billno { get; set; }

            /// <summary>
            /// 微信支付分配的商户号
            /// </summary>
            public string mchid { get; set; }

            /// <summary>
            /// 微信分配的公众账号ID（企业号corpid即为此appId）
            /// </summary>
            public string wxappid { get; set; }

            /// <summary>
            /// 接受收红包的用户在wxappid下的openid
            /// </summary>
            public string re_openid { get; set; }

            /// <summary>
            /// 付款金额，单位分
            /// </summary>
            public int total_amount { get; set; }

            /// <summary>
            /// 红包订单的微信单号
            /// </summary>
            public string send_listid { get; set; }
      }

      /// <summary>
      /// 微信红包错误代码
      /// </summary>
      public enum RedpackErrorCode
      {
            /// <summary>
            /// 无错误
            /// </summary>
            SUCCESS,
            /// <summary>
            /// 用户账号异常，被拦截
            /// </summary>
            NOAUTH,
            /// <summary>
            /// 该用户今日领取红包个数超过你在微信支付商户平台配置的上限
            /// </summary>
            SENDNUM_LIMIT,
            /// <summary>
            /// 错误传入了app的appid
            /// </summary>
            ILLEGAL_APPID,
            /// <summary>
            /// 发送红包金额不再限制范围内
            /// </summary>
            MONEY_LIMIT,
            /// <summary>
            /// 红包发放失败,请更换单号再重试
            /// </summary>
            SEND_FAILED,
            /// <summary>
            /// 更换了openid，但商户单号未更新或者更换了金额，但商户单号未更新
            /// </summary>
            FATAL_ERROR,
            /// <summary>
            /// 请求携带的证书出错
            /// </summary>
            CA_ERROR,
            /// <summary>
            /// 签名错误
            /// </summary>
            SIGN_ERROR,
            /// <summary>
            /// 请求已受理，请稍后使用原单号查询发放结果
            /// </summary>
            SYSTEMERROR,
            /// <summary>
            /// 请求的xml格式错误，或者post的数据为空
            /// </summary>
            XML_ERROR,
            /// <summary>
            /// 超过频率限制,请稍后再试
            /// </summary>
            FREQ_LIMIT,
            /// <summary>
            /// 帐号余额不足，请到商户平台充值后再重试
            /// </summary>
            NOTENOUGH,
            /// <summary>
            /// openid和appid不匹配
            /// </summary>
            OPENID_ERROR,
            /// <summary>
            /// msgappid与主、子商户号的绑定关系校验失败
            /// </summary>
            MSGAPPID_ERROR,
            /// <summary>
            /// 服务商模式下主商户号与子商户号关系校验失败
            /// </summary>
            ACCEPTMODE_ERROR,
            /// <summary>
            /// 发红包流程正在处理,二十分钟后查询,按照查询结果成功失败进行处理
            /// </summary>
            PROCESSING,
            /// <summary>
            /// 参数错误
            /// </summary>
            PARAM_ERROR
      }
}