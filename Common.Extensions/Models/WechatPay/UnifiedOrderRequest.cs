using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Extensions.Models.WechatPay
{
      /// <summary>
      /// 统一下单请求 对象
      /// </summary>
      public class UnifiedOrderRequest
      {
            /// <summary>
            /// 微信分配的公众账号ID（企业号corpid即为此appId）
            /// </summary>
            public string appid { get; set; }
            /// <summary>
            /// 微信支付分配的商户号
            /// </summary>
            public string mch_id { get; set; }
            /// <summary>
            /// 随机字符串，不长于32位
            /// </summary>
            public string nonce_str { get; set; }
            /// <summary>
            /// 签名
            /// </summary>
            public string sign { get; set; }

            /// <summary>
            /// 商品简单描述，该字段须严格按照规范传递
            /// </summary>
            public string body { get; set; }

            /// <summary>
            /// 商户订单号，需保持唯一性
            /// </summary>
            public string out_trade_no { get; set; }

            /// <summary>
            /// 订单总金额，单位为分
            /// </summary>
            public int total_fee { get; set; }

            /// <summary>
            /// 币种
            /// </summary>
            public CashType fee_type { get; set; }

            /// <summary>
            /// APP和网页支付提交用户端ip，Native支付填调用微信支付API的机器IP
            /// </summary>
            public string spbill_create_ip { get; set; }

            /// <summary>
            /// 交易起始时间,格式为yyyyMMddHHmmss
            /// </summary>
            public string time_start { get; set; }

            /// <summary>
            /// 订单失效时间，格式为yyyyMMddHHmmss
            /// </summary>
            public string time_expire { get; set; }

            /// <summary>
            /// 接收微信支付异步通知回调地址，通知url必须为直接可访问的url，不能携带参数。
            /// </summary>
            public string notify_url { get; set; }

            /// <summary>
            /// 交易类型
            /// </summary>
            public TradeType trade_type { get; set; }
      }

      /// <summary>
      /// 交易类型
      /// </summary>
      public enum TradeType
      {
            /// <summary>
            /// 公众号支付
            /// </summary>
            JSAPI,
            /// <summary>
            /// 原生扫码支付
            /// </summary>
            NATIVE,
            /// <summary>
            /// app支付
            /// </summary>
            APP
      }

      /// <summary>
      /// 现金币种
      /// </summary>
      public enum CashType
      {
            /// <summary>
            /// 人民币
            /// </summary>
            CNY,
      }

      /// <summary>
      /// 银行类型
      /// </summary>
      public enum BankType
      {
            /// <summary>
            /// 财付通
            /// </summary>
            CFT,
            /// <summary>
            /// 工商银行(借记卡)
            /// </summary>
            ICBC_DEBIT,
            /// <summary>
            /// 工商银行(信用卡)
            /// </summary>
            ICBC_CREDIT,
            /// <summary>
            /// 农业银行(借记卡)
            /// </summary>
            ABC_DEBIT,
            /// <summary>
            /// 农业银行(信用卡)
            /// </summary>
            ABC_CREDIT,
            /// <summary>
            /// 邮政储蓄银行(借记卡)
            /// </summary>
            PSBC_DEBIT,
            /// <summary>
            /// 邮政储蓄银行(信用卡)
            /// </summary>
            PSBC_CREDIT,
            /// <summary>
            /// 建设银行(借记卡)
            /// </summary>
            CCB_DEBIT,
            /// <summary>
            /// 建设银行(信用卡)
            /// </summary>
            CCB_CREDIT,
            /// <summary>
            /// 招商银行(借记卡)
            /// </summary>
            CMB_DEBIT,
            /// <summary>
            /// 招商银行(信用卡)
            /// </summary>
            CMB_CREDIT,
            /// <summary>
            /// 中国银行(借记卡)
            /// </summary>
            BOC_DEBIT,
            /// <summary>
            /// 中国银行(信用卡)
            /// </summary>
            BOC_CREDIT,
            /// <summary>
            /// 交通银行(借记卡)
            /// </summary>
            COMM_DEBIT,
            /// <summary>
            /// 浦发银行(借记卡)
            /// </summary>
            SPDB_DEBIT,
            /// <summary>
            /// 浦发银行(信用卡)
            /// </summary>
            SPDB_CREDIT,
            /// <summary>
            /// 广发银行(借记卡)
            /// </summary>
            GDB_DEBIT,
            /// <summary>
            /// 广发银行(信用卡)
            /// </summary>
            GDB_CREDIT,
            /// <summary>
            /// 民生银行(借记卡)
            /// </summary>
            CMBC_DEBIT,
            /// <summary>
            /// 民生银行(信用卡)
            /// </summary>
            CMBC_CREDIT,
            /// <summary>
            /// 平安银行(借记卡)
            /// </summary>
            PAB_DEBIT,
            /// <summary>
            /// 平安银行(信用卡)
            /// </summary>
            PAB_CREDIT,
            /// <summary>
            /// 光大银行(借记卡)
            /// </summary>
            CEB_DEBIT,
            /// <summary>
            /// 光大银行(信用卡)
            /// </summary>
            CEB_CREDIT,
            /// <summary>
            /// 兴业银行(借记卡)
            /// </summary>
            CIB_DEBIT,
            /// <summary>
            /// 兴业银行(信用卡)
            /// </summary>
            CIB_CREDIT,
            /// <summary>
            /// 中信银行(借记卡)
            /// </summary>
            CITIC_DEBIT,
            /// <summary>
            /// 中信银行(信用卡)
            /// </summary>
            CITIC_CREDIT,
            /// <summary>
            /// 上海银行(借记卡)
            /// </summary>
            BOSH_DEBIT,
            /// <summary>
            /// 上海银行(信用卡)
            /// </summary>
            BOSH_CREDIT,
            /// <summary>
            /// 华润银行(借记卡)
            /// </summary>
            CRB_DEBIT,
            /// <summary>
            /// 杭州银行(借记卡)
            /// </summary>
            HZB_DEBIT,
            /// <summary>
            /// 杭州银行(信用卡)
            /// </summary>
            HZB_CREDIT,
            /// <summary>
            /// 包商银行(借记卡)
            /// </summary>
            BSB_DEBIT,
            /// <summary>
            /// 包商银行(信用卡)
            /// </summary>
            BSB_CREDIT,
            /// <summary>
            /// 重庆银行(借记卡)
            /// </summary>
            CQB_DEBIT,
            /// <summary>
            /// 顺德农商行(借记卡)
            /// </summary>
            SDEB_DEBIT,
            /// <summary>
            /// 深圳农商银行(借记卡)
            /// </summary>
            SZRCB_DEBIT,
            /// <summary>
            /// 哈尔滨银行(借记卡)
            /// </summary>
            HRBB_DEBIT,
            /// <summary>
            /// 成都银行(借记卡)
            /// </summary>
            BOCD_DEBIT,
            /// <summary>
            /// 南粤银行(借记卡)
            /// </summary>
            GDNYB_DEBIT,
            /// <summary>
            /// 南粤银行(信用卡)
            /// </summary>
            GDNYB_CREDIT,
            /// <summary>
            /// 广州银行(借记卡)
            /// </summary>
            GZCB_DEBIT,
            /// <summary>
            /// 广州银行(信用卡)
            /// </summary>
            GZCB_CREDIT,
            /// <summary>
            /// 江苏银行(借记卡)
            /// </summary>
            JSB_DEBIT,
            /// <summary>
            /// 江苏银行(信用卡)
            /// </summary>
            JSB_CREDIT,
            /// <summary>
            /// 宁波银行(借记卡)
            /// </summary>
            NBCB_DEBIT,
            /// <summary>
            /// 宁波银行(信用卡)
            /// </summary>
            NBCB_CREDIT,
            /// <summary>
            /// 南京银行(借记卡)
            /// </summary>
            NJCB_DEBIT,
            /// <summary>
            /// 晋中银行(借记卡)
            /// </summary>
            JZB_DEBIT,
            /// <summary>
            /// 昆山农商(借记卡)
            /// </summary>
            KRCB_DEBIT,
            /// <summary>
            /// 龙江银行(借记卡)
            /// </summary>
            LJB_DEBIT,
            /// <summary>
            /// 辽宁农信(借记卡)
            /// </summary>
            LNNX_DEBIT,
            /// <summary>
            /// 兰州银行(借记卡)
            /// </summary>
            LZB_DEBIT,
            /// <summary>
            /// 无锡农商(借记卡)
            /// </summary>
            WRCB_DEBIT,
            /// <summary>
            /// 中原银行(借记卡)
            /// </summary>
            ZYB_DEBIT,
            /// <summary>
            /// 浙江农信(借记卡)
            /// </summary>
            ZJRCUB_DEBIT,
            /// <summary>
            /// 温州银行(借记卡)
            /// </summary>
            WZB_DEBIT,
            /// <summary>
            /// 西安银行(借记卡)
            /// </summary>
            XAB_DEBIT,
            /// <summary>
            /// 江西农信(借记卡) 
            /// </summary>
            JXNXB_DEBIT,
            /// <summary>
            /// 宁波通商银行(借记卡)
            /// </summary>
            NCB_DEBIT,
            /// <summary>
            /// 南阳村镇银行(借记卡)
            /// </summary>
            NYCCB_DEBIT,
            /// <summary>
            /// 内蒙古农信(借记卡)
            /// </summary>
            NMGNX_DEBIT,
            /// <summary>
            ///  陕西信合(借记卡)
            /// </summary>
            SXXH_DEBIT,
            /// <summary>
            /// 上海农商银行(信用卡)
            /// </summary>
            SRCB_CREDIT,
            /// <summary>
            ///  盛京银行(借记卡)
            /// </summary>
            SJB_DEBIT,
            /// <summary>
            /// 山东农信(借记卡)
            /// </summary>
            SDRCU_DEBIT,
            /// <summary>
            /// 上海农商银行(借记卡)
            /// </summary>
            SRCB_DEBIT,
            /// <summary>
            /// 四川农信(借记卡)
            /// </summary>
            SCNX_DEBIT,
            /// <summary>
            /// 齐鲁银行(借记卡)
            /// </summary>
            QLB_DEBIT,
            /// <summary>
            ///  青岛银行(借记卡)
            /// </summary>
            QDCCB_DEBIT,
            /// <summary>
            /// 攀枝花银行(借记卡)
            /// </summary>
            PZHCCB_DEBIT,
            /// <summary>
            /// 浙江泰隆银行(借记卡)
            /// </summary>
            ZJTLCB_DEBIT,
            /// <summary>
            /// 天津滨海农商行(借记卡)
            /// </summary>
            TJBHB_DEBIT,
            /// <summary>
            /// 微众银行(借记卡)
            /// </summary>
            WEB_DEBIT,
            /// <summary>
            /// 云南农信(借记卡)
            /// </summary>
            YNRCCB_DEBIT,
            /// <summary>
            /// 潍坊银行(借记卡)
            /// </summary>
            WFB_DEBIT,
            /// <summary>
            ///  武汉农商行(借记卡)
            /// </summary>
            WHRC_DEBIT,
            /// <summary>
            /// 鄂尔多斯银行(借记卡)
            /// </summary>
            ORDOSB_DEBIT,
            /// <summary>
            ///  新疆农信银行(借记卡)
            /// </summary>
            XJRCCB_DEBIT,
            /// <summary>
            /// 鄂尔多斯银行(信用卡)
            /// </summary>
            ORDOSB_CREDIT,
            /// <summary>
            /// 常熟农商银行(借记卡)
            /// </summary>
            CSRCB_DEBIT,
            /// <summary>
            /// 江苏农商行(借记卡)
            /// </summary>
            JSNX_DEBIT,
            /// <summary>
            /// 广州农商银行(信用卡)
            /// </summary>
            GRCB_CREDIT,
            /// <summary>
            /// 桂林银行(借记卡)
            /// </summary>
            GLB_DEBIT,
            /// <summary>
            /// 广东农信银行(借记卡)
            /// </summary>
            GDRCU_DEBIT,
            /// <summary>
            ///  广东华兴银行(借记卡)
            /// </summary>
            GDHX_DEBIT,
            /// <summary>
            /// 福建农信银行(借记卡)
            /// </summary>
            FJNX_DEBIT,
            /// <summary>
            /// 德阳银行(借记卡)
            /// </summary>
            DYCCB_DEBIT,
            /// <summary>
            /// 东莞农商行(借记卡)
            /// </summary>
            DRCB_DEBIT,
            /// <summary>
            /// 稠州银行(借记卡)
            /// </summary>
            CZCB_DEBIT,
            /// <summary>
            ///  浙商银行(借记卡)
            /// </summary>
            CZB_DEBIT,
            /// <summary>
            ///  浙商银行(信用卡)
            /// </summary>
            CZB_CREDIT,
            /// <summary>
            /// 广州农商银行(借记卡)
            /// </summary>
            GRCB_DEBIT,
            /// <summary>
            /// 长沙银行(借记卡)
            /// </summary>
            CSCB_DEBIT,
            /// <summary>
            /// 重庆农商银行(借记卡)
            /// </summary>
            CQRCB_DEBIT,
            /// <summary>
            /// 渤海银行(借记卡)
            /// </summary>
            CBHB_DEBIT,
            /// <summary>
            /// 内蒙古银行(借记卡)
            /// </summary>
            BOIMCB_DEBIT,
            /// <summary>
            ///  东莞银行(借记卡)
            /// </summary>
            BOD_DEBIT,
            /// <summary>
            /// 东莞银行(信用卡)
            /// </summary>
            BOD_CREDIT,
            /// <summary>
            /// 北京银行(借记卡)
            /// </summary>
            BOB_DEBIT,
            /// <summary>
            /// 江西银行(借记卡)
            /// </summary>
            BNC_DEBIT,
            /// <summary>
            /// 北京农商行(借记卡)
            /// </summary>
            BJRCB_DEBIT,
            /// <summary>
            ///  AE(信用卡)
            /// </summary>
            AE_CREDIT,
            /// <summary>
            /// 贵阳银行(信用卡)
            /// </summary>
            GYCB_CREDIT,
            /// <summary>
            /// 晋商银行(借记卡)
            /// </summary>
            JSHB_DEBIT,
            /// <summary>
            /// 江阴农商行(借记卡)
            /// </summary>
            JRCB_DEBIT,
            /// <summary>
            /// 江南农商(借记卡)
            /// </summary>
            JNRCB_DEBIT,
            /// <summary>
            /// 吉林农信(借记卡)
            /// </summary>
            JLNX_DEBIT,
            /// <summary>
            /// 吉林银行(借记卡)
            /// </summary>
            JLB_DEBIT,
            /// <summary>
            /// 九江银行(借记卡)
            /// </summary>
            JJCCB_DEBIT,
            /// <summary>
            /// 华夏银行(借记卡)
            /// </summary>
            HXB_DEBIT,
            /// <summary>
            /// 华夏银行(信用卡)
            /// </summary>
            HXB_CREDIT,
            /// <summary>
            ///  湖南农信(借记卡)
            /// </summary>
            HUNNX_DEBIT,
            /// <summary>
            /// 徽商银行(借记卡)
            /// </summary>
            HSB_DEBIT,
            /// <summary>
            /// 恒生银行(借记卡)
            /// </summary>
            HSBC_DEBIT,
            /// <summary>
            /// 华融湘江银行(借记卡)
            /// </summary>
            HRXJB_DEBIT,
            /// <summary>
            ///  河南农信(借记卡)
            /// </summary>
            HNNX_DEBIT,
            /// <summary>
            /// 东亚银行(借记卡)
            /// </summary>
            HKBEA_DEBIT,
            /// <summary>
            /// 河北农信(借记卡)
            /// </summary>
            HEBNX_DEBIT,
            /// <summary>
            ///  湖北农信(借记卡)
            /// </summary>
            HBNX_DEBIT,
            /// <summary>
            /// 湖北农信(信用卡)
            /// </summary>
            HBNX_CREDIT,
            /// <summary>
            ///  贵阳银行(借记卡)
            /// </summary>
            GYCB_DEBIT,
            /// <summary>
            ///  甘肃农信(借记卡)
            /// </summary>
            GSNX_DEBIT,
            /// <summary>
            /// JCB(信用卡)
            /// </summary>
            JCB_CREDIT,
            /// <summary>
            /// MASTERCARD(信用卡)
            /// </summary>
            MASTERCARD_CREDIT,
            /// <summary>
            /// VISA(信用卡)
            /// </summary>
            VISA_CREDIT
      }
}