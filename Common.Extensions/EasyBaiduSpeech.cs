using Common.Extensions.Models;
using System;
using System.Collections.Generic;
using System.Extensions;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
      /// <summary>
      /// 百度语音识别、语音合成 助手
      /// </summary>
      public static class EasyBaiduSpeech
      {
            /// <summary>
            /// 应用的API Key
            /// </summary>
            private static string ApiKey;

            /// <summary>
            /// 应用的Secret Key
            /// </summary>
            private static string SecretKey;

            /// <summary>
            /// AccessToken令牌
            /// </summary>
            private static string AccessToken;

            /// <summary>
            /// AccessToken过期时间
            /// </summary>
            private static DateTime ExpireTime;

            /// <summary>
            /// 错误信息
            /// </summary>
            public static string Error { get; private set; }

            /// <summary>
            /// 全局统一初始化语音助手
            /// 不要重复调用，除非更改接口调用参数
            /// </summary>
            /// <param name="apiKey">应用的API Key</param>
            /// <param name="secretKey">应用的Secret Key</param>
            public static void Init(string apiKey, string secretKey)
            {
                  ApiKey = apiKey;
                  SecretKey = secretKey;
            }

            /// <summary>
            /// 自动刷新令牌
            /// </summary>
            /// <returns></returns>
            private static string GetAccessToken()
            {
                  if (ExpireTime <= DateTime.Now)
                  {
                        HttpClient httpClient = new HttpClient();
                        string response = httpClient.GetStringAsync(string.Format("https://openapi.baidu.com/oauth/2.0/token?grant_type=client_credentials&client_id={0}&client_secret={1}", ApiKey, SecretKey)).Result;
                        httpClient.Dispose();
                        TokenForSpeech tokenForSpeech = response.ToJsonResult<TokenForSpeech>();
                        Error = tokenForSpeech.error_description;
                        if (tokenForSpeech.error == null)
                        {
                              AccessToken = tokenForSpeech.access_token;
                              ExpireTime = DateTime.Now.AddSeconds(tokenForSpeech.expires_in);
                        }
                  }
                  return AccessToken;
            }

            /// <summary>
            /// 语音合成 长度为0表示合成失败
            /// </summary>
            /// <param name="text">待合成内容</param>
            /// <param name="language">语言</param>
            public static async Task<Stream> Text2Audio(string text, string language = "zh")
            {
                  HttpClient httpClient = new HttpClient();
                  try
                  {
                        HttpResponseMessage response = await httpClient.GetAsync(string.Format("http://tsn.baidu.com/text2audio?tex={0}&lan={1}&cuid={2}&ctp=1&tok={3}", text, language, Guid.Empty, GetAccessToken()));
                        if (response.IsSuccessStatusCode)
                        {
                              if (response.Content.Headers.ContentType.MediaType == "audio/mp3")
                              {
                                    return response.Content.ReadAsStreamAsync().Result;
                              }
                              else
                              {
                                    Error = await response.Content.ReadAsStringAsync();
                              }
                        }
                        else
                        {
                              Error = "语音合成接口调用失败";
                        }
                        return new MemoryStream();
                  }
                  catch (Exception e)
                  {
                        Error = e.Message;
                        return new MemoryStream();
                  }
                  finally
                  {
                        httpClient.Dispose();
                  }
            }
      }
}
