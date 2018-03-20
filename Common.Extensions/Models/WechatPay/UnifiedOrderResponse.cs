using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Common.Extensions.Models.WechatPay
{
    /// <summary>
    /// 统一下单 响应对象
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class UnifiedOrderResponse
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
        /// 交易类型
        /// </summary>
        public TradeType trade_type { get; set; }

        /// <summary>
        /// 微信生成的预支付回话标识，用于后续接口调用中使用，该值有效期为2小时
        /// </summary>
        public string prepay_id { get; set; }

        /// <summary>
        /// 二维码链接，trade_type为NATIVE是有返回，可将该参数值生成二维码展示出来进行扫码支付
        /// </summary>
        public string code_url { get; set; }
    }

    /// <summary>
    /// 错误码信息
    /// </summary>
    public enum UnifiedOrderErrorCode
    {
        /// <summary>
        /// 商户未开通此接口权限
        /// </summary>
        NOAUTH,
        /// <summary>
        /// 用户帐号余额不足
        /// </summary>
        NOTENOUGH,
        /// <summary>
        /// 商户订单已支付，无需重复操作
        /// </summary>
        ORDERPAID,
        /// <summary>
        /// 当前订单已关闭，无法支付
        /// </summary>
        ORDERCLOSED,
        /// <summary>
        /// 系统异常，请用相同参数重新调用
        /// </summary>
        SYSTEMERROR,
        /// <summary>
        /// 请求的xml格式错误，或者post的数据为空
        /// </summary>
        APPID_NOT_EXIST,
        /// <summary>
        /// APPID不存在
        /// </summary>
        FREQ_LIMIT,
        /// <summary>
        /// MCHID不存在
        /// </summary>
        MCHID_NOT_EXIST,
        /// <summary>
        /// openid和appid不匹配
        /// </summary>
        APPID_MCHID_NOT_MATCH,
        /// <summary>
        /// 缺少必要的请求参数
        /// </summary>
        LACK_PARAMS,
        /// <summary>
        /// 商户订单号重复
        /// </summary>
        OUT_TRADE_NO_USED,
        /// <summary>
        /// 签名错误
        /// </summary>
        SIGNERROR,
        /// <summary>
        /// XML格式错误
        /// </summary>
        XML_FORMAT_ERROR,
        /// <summary>
        /// 请使用post方法
        /// </summary>
        REQUIRE_POST_METHOD,

        /// <summary>
        /// post数据为空
        /// </summary>
        POST_DATA_EMPTY,
        /// <summary>
        /// 编码格式错误,请使用UTF-8编码格式
        /// </summary>
        NOT_UTF8,
        /// <summary>
        /// 非法请求
        /// </summary>
        INVALID_REQUEST
    }
}