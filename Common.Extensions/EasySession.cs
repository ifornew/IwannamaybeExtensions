using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace Common.Extensions
{
    /// <summary>
    /// Session助手
    /// </summary>
    public static class EasySession
    {
        /// <summary>
        /// 获取当前Session的原始实体
        /// </summary>
        public static HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }


        /// <summary>
        /// 更新session值
        /// </summary>
        /// <param name="sessionKey">Session键</param>
        /// <param name="sessionValue">session值</param>
        /// <param name="timeOutMinutes">session超时时间（单位：分钟）</param>
        public static void Update(string sessionKey, object sessionValue, int timeOutMinutes = 0)
        {
            if (timeOutMinutes > 0)
            {
                HttpContext.Current.Session.Timeout = timeOutMinutes;
            }
            HttpContext.Current.Session[sessionKey] = sessionValue;
        }

        /// <summary>
        /// 添加session值
        /// </summary>
        /// <param name="sessionKey">Session键</param>
        /// <param name="sessionValue">session值</param>
        /// <param name="timeOutMinutes">session超时时间（单位：分钟）</param>
        public static void Add(string sessionKey, object sessionValue, int timeOutMinutes = 0)
        {
            if (timeOutMinutes > 0)
            {
                HttpContext.Current.Session.Timeout = timeOutMinutes;
            }
            HttpContext.Current.Session.Add(sessionKey, sessionValue);
        }

        /// <summary>
        /// 移除session值
        /// </summary>
        /// <param name="sessionKey">Session键</param>
        public static void Remove(string sessionKey)
        {
            HttpContext.Current.Session.Remove(sessionKey);
        }

        /// <summary>
        /// 移除所有Session键值
        /// </summary>
        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }

        /// <summary>
        /// 根据键和默认值获取Session值，并进行类型转换
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sessionKey">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T Get<T>(string sessionKey, T defaultValue)
        {
            return (T)(HttpContext.Current.Session[sessionKey] ?? defaultValue);
        }

        /// <summary>
        /// 根据键获取Session值，并进行类型转换
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sessionKey">键</param>
        /// <returns></returns>
        public static T Get<T>(string sessionKey)
        {
            return (T)HttpContext.Current.Session[sessionKey];
        }

        /// <summary>
        /// 根据键获取Session值
        /// </summary>
        /// <param name="sessionKey">键</param>
        /// <returns></returns>
        public static object Get(string sessionKey)
        {
            return HttpContext.Current.Session[sessionKey];
        }
    }
}
