using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Extensions.Models.WechatPay
{
    /// <summary>
    /// 企业付款请求 对象
    /// </summary>
    public class TransferRequest
    {
        /// <summary>
        /// 微信分配的公众账号ID（企业号corpid即为此appId）
        /// </summary>
        public string mch_appid { get; set; }
        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        public string mchid { get; set; }
        /// <summary>
        /// 随机字符串，不长于32位
        /// </summary>
        public string nonce_str { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 商户订单号，需保持唯一性
        /// </summary>
        public string partner_trade_no { get; set; }
        /// <summary>
        /// 商户appid下，某用户的openid
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 校验用户姓名选项
        /// </summary>
        public CheckName check_name { get; set; }
        /// <summary>
        /// 收款用户姓名
        /// </summary>
        public string re_user_name { get; set; }
        /// <summary>
        /// 企业付款金额，单位为分,最小值为：100
        /// </summary>
        public int amount { get; set; }
        /// <summary>
        /// 企业付款操作说明信息。必填。
        /// </summary>
        public string desc { get; set; }
        /// <summary>
        /// 调用接口的机器Ip地址
        /// </summary>
        public string spbill_create_ip { get; set; }
    }

    /// <summary>
    /// 转账是否校验真实姓名
    /// </summary>
    public enum CheckName
    {
        /// <summary>
        /// 不校验真实姓名
        /// </summary>
        NO_CHECK,
        /// <summary>
        /// 强校验真实姓名（未实名认证的用户会校验失败，无法转账） 
        /// </summary>
        FORCE_CHECK,
        /// <summary>
        /// 针对已实名认证的用户才校验真实姓名（未实名认证用户不校验，可以转账成功）
        /// </summary>
        OPTION_CHECK,
    }
}