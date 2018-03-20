using Common.Extensions.Models.WechatPay;
using System;
using System.Collections.Generic;
using System.Extensions;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common.Extensions
{
    /// <summary>
    /// 微信支付 助手
    /// </summary>
    public static class EasyWechatPay
    {
        #region 配置项
        /// <summary>
        /// 微信公众号Appid
        /// </summary>
        private static string AppId;

        /// <summary>
        /// 微信公众号AppSecret
        /// </summary>
        private static string AppSecret;

        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        private static string MchId;

        /// <summary>
        /// 微信支付Api密钥
        /// </summary>
        private static string Key;

        /// <summary>
        /// 微信支付证书路径
        /// </summary>
        private static string Cert;

        /// <summary>
        /// 微信支付证书的密码
        /// </summary>
        private static string CertPassword;

        /// <summary>
        /// 微信支付回调地址
        /// </summary>
        private static string PayNotifyUrl;

        /// <summary>
        /// 支付超时时间（单位：分钟）
        /// </summary>
        private static int PayExpire;

        /// <summary>
        /// 微信授权多路径转发回调地址
        /// </summary>
        private static string SnsRedirectUrl;

        /// <summary>
        /// 客户端版本（默认：v1.0）
        /// </summary>
        private static string ClientVersion;

        /// <summary>
        /// 初始化 微信助手
        /// </summary>
        /// <param name="appId">微信公众号Appid</param>
        /// <param name="appSecret">微信公众号AppSecret</param>
        /// <param name="mchId">微信支付分配的商户号</param>
        /// <param name="key">微信支付Api密钥</param>
        /// <param name="cert">微信支付证书路径</param>
        /// <param name="certPassword">微信支付证书的密码</param>
        /// <param name="payNotifyUrl">微信支付回调地址</param>
        /// <param name="snsRedirectUrl">微信授权多路径转发回调地址</param>
        /// <param name="payExpire">支付超时时间（单位：分钟；默认10分钟）</param>
        /// <param name="clientVersion">客户端版本（默认：v1.0）</param>
        public static void InitConfig(string appId, string appSecret, string mchId, string key, string cert, string certPassword, string payNotifyUrl, string snsRedirectUrl = "", int payExpire = 10, string clientVersion = "v1.0")
        {
            AppId = appId;
            AppSecret = appSecret;
            MchId = mchId;
            Key = key;
            Cert = string.Format("{0}App_Data\\{1}", HttpRuntime.AppDomainAppPath.ToString(), cert);
            CertPassword = certPassword;
            PayNotifyUrl = payNotifyUrl;
            SnsRedirectUrl = snsRedirectUrl;
            PayExpire = payExpire;
            ClientVersion = clientVersion;
        }
        #endregion

        #region 微信支付接口列表
        /// <summary>
        /// 统一下单接口
        /// </summary>
        public static Uri UnifiedOrderApi = new Uri("https://api.mch.weixin.qq.com/pay/unifiedorder");

        /// <summary>
        /// 企业付款
        /// </summary>
        public static Uri TransferApi = new Uri("https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers");

        /// <summary>
        /// 微信红包接口
        /// </summary>
        public static Uri RedpackApi = new Uri("https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack");

        /// <summary>
        /// 拉取ScopeCode接口地址
        /// <param name="redirect">是否转发回调地址</param>
        /// </summary>
        public static Uri SnsCodeApi(bool redirect = false)
        {
            return new Uri(string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect", AppId, HttpUtility.UrlEncode(redirect ? SnsRedirectUrl + RequestUrl() : RequestUrl())));
        }

        /// <summary>
        /// 用户信息接口
        /// </summary>
        /// <param name="accessToken">AccessToken令牌</param>
        /// <param name="openId">用户OpenId</param>
        /// <returns></returns>
        public static Uri UserinfoApi(string accessToken, string openId)
        {
            return new Uri(string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", accessToken, openId));
        }

        /// <summary>
        /// 获取AccessToken以及OpenId的接口
        /// </summary>
        /// <param name="scopeCode">ScopeCode</param>
        /// <returns></returns>
        public static Uri AccessTokenApi(string scopeCode)
        {
            return new Uri(string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", AppId, AppSecret, scopeCode));
        }
        #endregion

        /// <summary>
        /// 判断是否是微信客户端
        /// </summary>
        /// <returns></returns>
        public static bool IsWechatClient()
        {
            string userAgent = HttpContext.Current.Request.UserAgent;
            return !string.IsNullOrEmpty(userAgent) && userAgent.ToLower().Contains("micromessenger");
        }

        #region 微信公众号网页授权
        /// <summary>
        /// 拉取并更新用户信息(OAuth2)(errcode==0表示获取成功；errmsg表示获取失败的信息)
        /// </summary>
        /// <returns>用户信息实体</returns>
        public static OAuth2Userinfo UpdateUserInfo()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    string scopeCode = HttpContext.Current.Request.QueryString["code"].ToString();
                    string returnString = httpClient.GetStringAsync(AccessTokenApi(scopeCode)).Result.ToString();

                    OAuth2AccessToken oAuth2AccessToken = returnString.ToJsonResult<OAuth2AccessToken>();

                    if (oAuth2AccessToken.errcode == 0)
                    {
                        returnString = httpClient.GetStringAsync(UserinfoApi(oAuth2AccessToken.access_token, oAuth2AccessToken.openid)).Result.ToString();
                        return returnString.ToJsonResult<OAuth2Userinfo>();
                    }
                    else
                    {
                        return new OAuth2Userinfo { errcode = -1, errmsg = oAuth2AccessToken.errmsg };
                    }
                }
                catch (Exception e)
                {
                    return new OAuth2Userinfo { errcode = -1, errmsg = e.Message };
                }
            }
        }
        #endregion

        #region 微信支付
        /// <summary>
        /// 企业付款(注意判断：return_code和result_code)
        /// </summary>
        /// <param name="openid">微信OpenId</param>
        /// <param name="penny">金额（单位：分）</param>
        /// <param name="description">描述</param>
        /// <param name="orderNo">交易订单号</param>
        /// <returns>XML字符串</returns>
        public static async Task<TransferResponse> SendTransferAsync(string openid, int penny, string description, string orderNo)
        {
            TransferRequest transferRequest = new TransferRequest()
            {
                mch_appid = AppId,
                mchid = MchId,
                amount = penny,
                openid = openid,
                desc = description,
                partner_trade_no = orderNo,
                spbill_create_ip = HttpContext.Current.Request.ServerVariables.Get("Local_Addr"),
                nonce_str = MakeNonceStr(),
                check_name = CheckName.NO_CHECK
            };
            transferRequest.sign = MakeSign(transferRequest.ToDictionary(), Key);
            string xmlReturn = await SendXmlRequest(TransferApi, transferRequest, true, Cert, CertPassword);
            return xmlReturn.ToXmlResult<TransferResponse>();
        }

        /// <summary>
        /// 企业给用户发送普通微信红包（注意判断：return_code和result_code）
        /// </summary>
        /// <param name="openid">微信OpenId</param>
        /// <param name="penny">金额（单位：分）</param>
        /// <param name="orderNo">交易订单号</param>
        /// <param name="description">描述</param>
        /// <param name="activityName">活动名称</param>
        /// <param name="merchantName">发送者名称</param>
        /// <param name="wishing">祝福语</param>
        /// <param name="senceType">红包场景</param>
        /// <returns></returns>
        public static async Task<RedpackResponse> SendRedpackAsync(string openid, int penny, string orderNo, string description, string activityName, string merchantName, string wishing, SenceType senceType = SenceType.PRODUCT_1)
        {
            RedpackRequest redpackRequest = new RedpackRequest
            {
                wxappid = AppId,
                mch_id = MchId,
                mch_billno = orderNo,
                re_openid = openid,
                remark = description,
                total_num = 1,
                total_amount = penny,
                client_ip = HttpContext.Current.Request.ServerVariables.Get("Local_Addr"),
                scene_id = senceType,
                act_name = activityName,
                send_name = merchantName,
                wishing = wishing,
                risk_info = GetRiskInfo(),
                nonce_str = MakeNonceStr(),
            };
            redpackRequest.sign = MakeSign(redpackRequest.ToDictionary(), Key);
            string xmlReturn = await SendXmlRequest(RedpackApi, redpackRequest, true, Cert, CertPassword);
            return xmlReturn.ToXmlResult<RedpackResponse>();
        }

        /// <summary>
        /// 统一下单（注意判断return_code和result_code；成功时取code_url的值）
        /// </summary>
        /// <param name="penny">支付金额，单位：分</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="body">商品标题信息</param>
        /// <param name="tradeType">交易类型</param>
        /// <returns></returns>
        public static async Task<UnifiedOrderResponse> UnifiedOrderAsync(int penny, string orderNo, string body, TradeType tradeType = TradeType.NATIVE)
        {
            UnifiedOrderRequest unifiedOrderRequest = new UnifiedOrderRequest
            {
                appid = AppId,
                mch_id = MchId,
                trade_type = tradeType,
                out_trade_no = orderNo,
                spbill_create_ip = HttpContext.Current.Request.UserHostAddress,
                time_start = DateTime.Now.ToString("yyyyMMddHHmmss"),
                time_expire = DateTime.Now.AddMinutes(PayExpire).ToString("yyyyMMddHHmmss"),
                total_fee = penny,
                fee_type = CashType.CNY,
                notify_url = PayNotifyUrl,
                body = body,
                nonce_str = MakeNonceStr()
            };
            unifiedOrderRequest.sign = MakeSign(unifiedOrderRequest.ToDictionary(), Key);
            string xmlReturn = await SendXmlRequest(UnifiedOrderApi, unifiedOrderRequest, true, Cert, CertPassword);
            return xmlReturn.ToXmlResult<UnifiedOrderResponse>();
        }
        #endregion

        #region 其他辅助方法
        /// <summary>
        /// 参数签名
        /// </summary>
        /// <param name="Params">参数</param>
        /// <param name="key">密钥(留空表示签名之前不追加密钥key)</param>
        /// <returns>签名</returns>
        private static string MakeSign(Dictionary<string, string> Params, string key = null)
        {
            //移除多余
            if (Params.Keys.Any(x => x.Equals("sign", StringComparison.OrdinalIgnoreCase)))
                Params.Remove("sign");
            //排序
            Params = Params.OrderBy(d => d.Key).ToDictionary(o => o.Key, o => o.Value);
            //拼接
            var paramSb = new StringBuilder();
            foreach (var dic in Params)
            {
                paramSb.Append(dic.Key);
                paramSb.Append("=" + dic.Value + "&");
            }
            if (!string.IsNullOrEmpty(key))
            {
                paramSb.Append("key=" + key);
            }
            //加密
            return paramSb.ToString().ToMD5(true, true);
        }

        /// <summary>
        /// 验证通知签名
        /// </summary>
        /// <param name="payNotifyResponse">通知对象</param>
        /// <returns></returns>
        public static bool CheckNotifySign(PayNotifyResponse payNotifyResponse)
        {
            Dictionary<string, string> payNotifyDictionary = new Dictionary<string, string>
                  {
                        { "appid" , payNotifyResponse.appid },
                        {"bank_type",payNotifyResponse.bank_type.ToString() },
                        {"cash_fee",payNotifyResponse.cash_fee },
                        {"fee_type",payNotifyResponse.fee_type.ToString() },
                        {"is_subscribe",payNotifyResponse.is_subscribe },
                        {"mch_id",payNotifyResponse.mch_id },
                        {"nonce_str",payNotifyResponse.nonce_str },
                        {"openid",payNotifyResponse.openid },
                        {"out_trade_no",payNotifyResponse.out_trade_no },
                        {"result_code",payNotifyResponse.result_code.ToString() },
                        {"return_code",payNotifyResponse.return_code.ToString() },
                        {"time_end",payNotifyResponse.time_end },
                        {"total_fee",payNotifyResponse.total_fee.ToString() },
                        {"trade_type",payNotifyResponse.trade_type.ToString() },
                        {"transaction_id",payNotifyResponse.transaction_id }
                  };
            if (MakeSign(payNotifyDictionary, Key) != payNotifyResponse.sign)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 随机字符串
        /// </summary>
        /// <returns>随机字符串</returns>
        private static string MakeNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        /// <summary>
        /// 收集活动相关信息
        /// </summary>
        /// <returns></returns>
        private static string GetRiskInfo()
        {
            return HttpUtility.UrlEncode(string.Format("posttime={0}&clientversion={1}", DateTime.Now.Second, ClientVersion));
        }

        /// <summary>
        /// 获取当前被访问的完整地址
        /// </summary>
        /// <returns></returns>
        public static string RequestUrl()
        {
            return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.Url.PathAndQuery;
        }

        /// <summary>
        /// 发送内容为XML对象的POST请求
        /// </summary>
        /// <param name="uri">请求地址</param>
        /// <param name="xmlObject">xml对象</param>
        /// <param name="useSSL">是否使用证书</param>
        /// <param name="certPath">SSL证书路径</param>
        /// <param name="certPassword">SSL证书密码</param>
        /// <returns>返回XML字符串</returns>
        public static async Task<string> SendXmlRequest<T>(Uri uri, T xmlObject, bool useSSL = false, string certPath = null, string certPassword = null)
        {
            WebRequestHandler handler = new WebRequestHandler();
            if (useSSL)
            {
                X509Certificate2 certificate = new X509Certificate2(certPath, certPassword);
                handler.ClientCertificates.Add(certificate);
            }
            using (HttpClient Requester = new HttpClient(handler))
            {
                Requester.BaseAddress = new Uri(uri.Scheme + "://" + uri.Host);
                Requester.DefaultRequestHeaders.Accept.Clear();
                Requester.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml") { CharSet = "utf-8" });
                HttpResponseMessage response = await Requester.PostAsync(uri.PathAndQuery, new StringContent(xmlObject.ToXml()));
                //HttpResponseMessage response = await Requester.PostAsXmlAsync(uri.PathAndQuery, xmlObject);
                return await response.Content.ReadAsStringAsync();
            }
        }
        #endregion
    }
}
