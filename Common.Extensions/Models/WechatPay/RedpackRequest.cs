using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Extensions.Models.WechatPay
{
      /// <summary>
      /// 微信红包请求 对象
      /// </summary>
      public class RedpackRequest
      {
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
            public string mch_billno { get; set; }

            /// <summary>
            /// 微信分配的公众账号ID（企业号corpid即为此appId）
            /// </summary>
            public string wxappid { get; set; }
            /// <summary>
            /// 微信支付分配的商户号
            /// </summary>
            public string mch_id { get; set; }

            /// <summary>
            /// 红包发送者名称
            /// </summary>
            public string send_name { get; set; }

            /// <summary>
            /// 接受红包的用户在wxappid下的openid
            /// </summary>
            public string re_openid { get; set; }

            /// <summary>
            /// 付款金额，单位分,最小值：100
            /// </summary>
            public int total_amount { get; set; }

            /// <summary>
            /// 红包发放总人数 :1
            /// </summary>
            public int total_num { get; set; }

            /// <summary>
            /// 红包祝福语
            /// </summary>
            public string wishing { get; set; }

            /// <summary>
            /// 调用接口的机器Ip地址
            /// </summary>
            public string client_ip { get; set; }

            /// <summary>
            /// 活动名称
            /// </summary>
            public string act_name { get; set; }

            /// <summary>
            /// 备注
            /// </summary>
            public string remark { get; set; }

            /// <summary>
            /// 场景类型
            /// </summary>
            public SenceType scene_id { get; set; }

            /// <summary>
            /// 活动信息，如：posttime%3d123123412%26clientversion%3d234134%26mobile%3d122344545%26deviceid%3dIOS，其中：posttime:用户操作的时间戳;mobile:业务系统账号的手机号，国家代码-手机号。不需要+号；deviceid:mac 地址或者设备唯一标识;clientversion:用户操作的客户端版本。把值为非空的信息用key = value进行拼接，再进行urlencode。
            /// </summary>
            public string risk_info { get; set; }

            /// <summary>
            /// 资金授权商户号，服务商替特约商户发放时使用
            /// </summary>
            public string consume_mch_id { get; set; }
      }

      /// <summary>
      /// 微信红包场景类型
      /// </summary>
      public enum SenceType
      {
            /// <summary>
            /// 商品促销
            /// </summary>
            PRODUCT_1,
            /// <summary>
            /// 抽奖
            /// </summary>
            PRODUCT_2,
            /// <summary>
            /// 虚拟物品兑奖
            /// </summary>
            PRODUCT_3,
            /// <summary>
            /// 企业内部福利
            /// </summary>
            PRODUCT_4,
            /// <summary>
            /// 渠道分润
            /// </summary>
            PRODUCT_5,
            /// <summary>
            /// 保险回馈
            /// </summary>
            PRODUCT_6,
            /// <summary>
            /// 彩票派奖
            /// </summary>
            PRODUCT_7,
            /// <summary>
            /// 税务刮奖
            /// </summary>
            PRODUCT_8
      }
}