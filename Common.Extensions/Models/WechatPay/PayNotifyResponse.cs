using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Common.Extensions.Models.WechatPay
{
      /// <summary>
      /// 微信支付异步通知 对象
      /// </summary>
      [XmlRoot(ElementName = "xml")]
      public class PayNotifyResponse
      {
            /// <summary>
            /// 返回状态码 SUCCESS/FAIL 此字段是通信标识，非交易标识，交易是否成功需要查看result_code来判断
            /// </summary>
            public ReturnCode return_code { get; set; }

            /// <summary>
            /// 返回信息，如非空，为错误原因
            /// </summary>
            public string return_msg { get; set; }

            /// <summary>
            /// 微信分配的公众账号ID（企业号corpid即为此appId）
            /// </summary>
            public string appid { get; set; }
            /// <summary>
            /// 微信支付分配的商户号
            /// </summary>
            public string mch_id { get; set; }

            /// <summary>
            /// 微信支付分配的终端设备号，
            /// </summary>
            public string device_info { get; set; }
            /// <summary>
            /// 随机字符串，不长于32位
            /// </summary>
            public string nonce_str { get; set; }
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
            public UnifiedOrderErrorCode err_code { get; set; }
            /// <summary>
            /// 错误代码描述
            /// </summary>
            public string err_code_des { get; set; }

            /// <summary>
            /// 用户在商户appid下的唯一标识
            /// </summary>
            public string openid { get; set; }

            /// <summary>
            /// 是否关注公众账号
            /// </summary>
            public string is_subscribe { get; set; }

            /// <summary>
            /// 交易类型
            /// </summary>
            public TradeType trade_type { get; set; }

            /// <summary>
            /// 付款银行
            /// </summary>
            public BankType bank_type { get; set; }

            /// <summary>
            /// 订单总金额，单位为分
            /// </summary>
            public int total_fee { get; set; }

            /// <summary>
            /// 应结订单金额=订单金额-非充值代金券金额
            /// </summary>
            public int settlement_total_fee { get; set; }

            /// <summary>
            /// 货币种类
            /// </summary>
            public CashType fee_type { get; set; }

            /// <summary>
            /// 现金支付金额
            /// </summary>
            public string cash_fee { get; set; }

            /// <summary>
            /// 现金支付货币类型
            /// </summary>
            public string cash_fee_type { get; set; }

            /// <summary>
            /// 代金券金额 订单金额-代金券金额=现金支付金额
            /// </summary>
            public int coupon_fee { get; set; }

            /// <summary>
            /// 代金券使用数量
            /// </summary>
            public int coupon_count { get; set; }

            /// <summary>
            /// 微信支付订单号
            /// </summary>
            public string transaction_id { get; set; }

            /// <summary>
            /// 商户系统的订单号，与请求一致
            /// </summary>
            public string out_trade_no { get; set; }

            /// <summary>
            /// 商家数据包，原样返回
            /// </summary>
            public string attach { get; set; }

            /// <summary>
            /// 支付完成时间，格式为yyyyMMddHHmmss
            /// </summary>
            public string time_end { get; set; }
      }
}