using Common.Extensions.Models.WechatProgram;
using System;
using System.Collections.Generic;
using System.Extensions;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    /// <summary>
    /// 微信小程序 助手
    /// </summary>
    public static class EasyWechatProgram
    {
        /// <summary>
        /// 微信小程序 Appid
        /// </summary>
        private static string AppId;

        /// <summary>
        /// 微信小程序 AppSecret
        /// </summary>
        private static string AppSecret;

        /// <summary>
        /// 使用登录凭证获取 session_key 和 openid的接口地址
        /// </summary>
        /// <param name="code">登录凭证</param>
        /// <returns></returns>
        public static Uri Jscode2SessionApi(string code)
        {
            return new Uri(string.Format("https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code", AppId, AppSecret, code));
        }

        /// <summary>
        /// 初始化 小程序助手
        /// </summary>
        /// <param name="appId">微信小程序 Appid</param>
        /// <param name="appSecret">微信小程序 AppSecret</param>
        public static void InitConfig(string appId, string appSecret)
        {
            AppId = appId;
            AppSecret = appSecret;
        }

        /// <summary>
        /// 使用登录凭证获取 session_key 和 openid
        /// </summary>
        /// <param name="code">登录凭证</param>
        public static Jscode2SessionResponse Jscode2Session(string code)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string returnString = httpClient.GetStringAsync(Jscode2SessionApi(code)).Result.ToString();
                Jscode2SessionResponse jscode2SessionResponse = returnString.ToJsonResult<Jscode2SessionResponse>();
                return jscode2SessionResponse;
            }
        }

        /// <summary>
        /// AES-128-CBC对称解密
        /// </summary>
        /// <param name="encryptedDataStr">加密数据</param>
        /// <param name="sessionKey">对称解密秘钥session_key</param>
        /// <param name="iv">对称解密算法初始向量</param>
        /// <returns></returns>
        public static string AesDecrypt(string encryptedDataStr, string sessionKey, string iv)
        {
            RijndaelManaged rijalg = new RijndaelManaged();
            //设置 cipher 格式 AES-128-CBC      
            rijalg.KeySize = 128;
            rijalg.Padding = PaddingMode.PKCS7;
            rijalg.Mode = CipherMode.CBC;

            rijalg.Key = Convert.FromBase64String(sessionKey);
            rijalg.IV = Convert.FromBase64String(iv);

            byte[] encryptedData = Convert.FromBase64String(encryptedDataStr);
            //解密
            ICryptoTransform decryptor = rijalg.CreateDecryptor(rijalg.Key, rijalg.IV);
            string result;
            using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        result = srDecrypt.ReadToEnd();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 检查签名
        /// </summary>
        /// <param name="rawData">原始数据</param>
        /// <param name="sessionKey">对称解密秘钥session_key</param>
        /// <param name="signature">签名</param>
        /// <returns></returns>
        public static bool CheckSign(string rawData, string sessionKey, string signature)
        {
            return (rawData + sessionKey).ToSHA1() == signature;
        }
    }
}
