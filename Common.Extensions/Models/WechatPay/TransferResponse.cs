using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Common.Extensions.Models.WechatPay
{
    /// <summary>
    /// 企业付款的响应 对象
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class TransferResponse
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
        /// 错误码信息
        /// </summary>
        public TransferErrorCode err_code { get; set; }
        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }


        /// <summary>
        /// 微信分配的公众账号ID（企业号corpid即为此appId）
        /// </summary>
        public string mch_appid { get; set; }
        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 微信支付分配的终端设备号，
        /// </summary>
        public string device_info { get; set; }
        /// <summary>
        /// 随机字符串，不长于32位
        /// </summary>
        public string nonce_str { get; set; }
        /// <summary>
        /// 业务结果SUCCESS/FAIL
        /// </summary>
        public ReturnCode result_code { get; set; }




        /// <summary>
        /// 商户订单号，需保持唯一性
        /// </summary>
        public string partner_trade_no { get; set; }
        /// <summary>
        /// 企业付款成功，返回的微信订单号
        /// </summary>
        public string payment_no { get; set; }
        /// <summary>
        /// 企业付款成功时间
        /// </summary>
        public string payment_time { get; set; }
    }

    /// <summary>
    /// 企业付款错误代码
    /// </summary>
    public enum TransferErrorCode
    {
        /// <summary>
        /// 没有权限
        /// </summary>
        NOAUTH,
        /// <summary>
        /// 付款金额不能小于最低限额,每次付款金额必须大于1元
        /// </summary>
        AMOUNT_LIMIT,
        /// <summary>
        /// 参数错误,请查看err_code_des，修改设置错误的参数
        /// </summary>
        PARAM_ERROR,
        /// <summary>
        /// Openid格式错误或者不属于商家公众账号
        /// </summary>
        OPENID_ERROR,
        /// <summary>
        /// 帐号余额不足
        /// </summary>
        NOTENOUGH,
        /// <summary>
        /// 系统繁忙，请稍后使用原单号以及原请求参数重试。
        /// </summary>
        SYSTEMERROR,
        /// <summary>
        /// 姓名校验出错
        /// </summary>
        NAME_MISMATCH,
        /// <summary>
        /// 签名错误
        /// </summary>
        SIGN_ERROR,
        /// <summary>
        /// Post请求数据不是合法的xml格式内容
        /// </summary>
        XML_ERROR,
        /// <summary>
        /// 两次请求商户单号一样，但是参数不一致
        /// </summary>
        FATAL_ERROR,
        /// <summary>
        /// 请求没带证书或者带上了错误的证书
        /// </summary>
        CA_ERROR
    }
}