using Common.Extensions;
using Common.Extensions.Models;
using log4net;
using Log4Net.Extensions.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Extensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Log4Net.Extensions
{
    /// <summary>
    /// 日志 管理器
    /// 配置要点：①Log4Net.config ②在AssemblyInfo.cs中添加 [assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4net.config", Watch = true)]
    /// </summary>
    public static class EasyLogger
    {
        /// <summary>
        /// 访问日志 记录器
        /// </summary>
        private static ILog _requestLogger = LogManager.GetLogger("RequestLogger");
        /// <summary>
        /// Sql日志 记录器
        /// </summary>
        private static ILog _sqlLogger = LogManager.GetLogger("SqlLogger");
        /// <summary>
        /// 普通日志 记录器
        /// </summary>
        private static ILog _logger = LogManager.GetLogger("Logger");

        /// <summary>
        /// 记录 访问日志
        /// </summary>
        public static void LogRequest(TimeSpan spendTime, DateTime endAt)
        {
            string ip = HttpContext.Current.Request.UserHostAddress;
            LogicalThreadContext.Properties["method"] = (int)EasyCommon.GetRequestMethod();

            LogicalThreadContext.Properties["ip"] = ip;
            LocationForIp locationForIp = EasyIp.Find(ip);
            LogicalThreadContext.Properties["country"] = locationForIp.Country;
            LogicalThreadContext.Properties["province"] = locationForIp.Province;
            LogicalThreadContext.Properties["city"] = locationForIp.City;
            LogicalThreadContext.Properties["uri"] = HttpContext.Current.Request.Url;
            LogicalThreadContext.Properties["userAgent"] = HttpContext.Current.Request.UserAgent;
            LogicalThreadContext.Properties["header"] = HttpContext.Current.Request.Headers.ToString();
            LogicalThreadContext.Properties["refer"] = HttpContext.Current.Request.UrlReferrer;
            LogicalThreadContext.Properties["createdAt"] = endAt.AddSeconds(-1 * spendTime.TotalSeconds);
            LogicalThreadContext.Properties["spendTime"] = spendTime.TotalSeconds;
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Current.Request.Form.ToString()))
                {
                    LogicalThreadContext.Properties["form"] = HttpContext.Current.Request.ContentEncoding.GetString(HttpContext.Current.Request.BinaryRead(HttpContext.Current.Request.ContentLength));
                    //LogicalThreadContext.Properties["form"] = HttpContext.Current.Request.ContentEncoding.GetString(HttpContext.Current.Request.BinaryRead(HttpContext.Current.Request.TotalBytes));
                }
                else
                {
                    LogicalThreadContext.Properties["form"] = HttpContext.Current.Request.Form.ToString();
                }
            }
            catch (Exception e)
            {
                LogicalThreadContext.Properties["form"] = "记录失败:" + e.Message;
            }
            _requestLogger.Info("请求日志");
        }

        /// <summary>
        /// 记录 成功的Sql日志
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="dbParameters">参数</param>
        /// <param name="endAt">结束时间</param>
        /// <param name="spendTime">执行耗时</param>
        public static void LogSql(string sql, List<SqlParameter> dbParameters, DateTime endAt, TimeSpan spendTime)
        {
            LogicalThreadContext.Properties["createdAt"] = endAt.AddSeconds(-1 * spendTime.TotalSeconds);
            LogicalThreadContext.Properties["spendTime"] = spendTime.TotalSeconds;
            LogicalThreadContext.Properties["sql"] = sql;
            LogicalThreadContext.Properties["dbParameters"] = dbParameters.ToJson();
            _sqlLogger.Info("Sql执行成功");
        }

        /// <summary>
        /// 记录 失败的Sql日志
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="dbParameters">参数</param>
        /// <param name="endAt">结束时间</param>
        /// <param name="spendTime">执行耗时</param>
        /// <param name="exception">异常信息</param>
        public static void LogSql(string sql, List<SqlParameter> dbParameters, DateTime endAt, TimeSpan spendTime, Exception exception)
        {
            LogicalThreadContext.Properties["createdAt"] = endAt.AddSeconds(-1 * spendTime.TotalSeconds);
            LogicalThreadContext.Properties["spendTime"] = spendTime.TotalSeconds;
            LogicalThreadContext.Properties["sql"] = sql;
            LogicalThreadContext.Properties["dbParameters"] = dbParameters.ToJson();
            _sqlLogger.Info("Sql执行失败：" + exception.Message, exception);
        }

        /// <summary>
        /// 记录 普通日志
        /// </summary>
        /// <param name="message">消息</param>
        public static void Log(string message)
        {
            Log(0,LogLevel.消息, message, "");
        }

        /// <summary>
        /// 记录 普通日志和级别
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        /// <param name="message">消息</param>
        public static void Log(LogLevel logLevel,string message)
        {
            Log(0,logLevel, message, "");
        }

        /// <summary>
        /// 记录 普通日志并附加日志类型
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="logType">日志类型</param>
        public static void Log(int logType, string message)
        {
            Log(logType,LogLevel.消息, message, "");
        }

        /// <summary>
        /// 记录 普通日志并附加日志类型和级别
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="logType">日志类型</param>
        /// <param name="logLevel">日志级别</param>
        public static void Log(int logType,LogLevel logLevel, string message)
        {
            Log(logType, logLevel, message, "");
        }

        /// <summary>
        /// 记录 普通日志并附加自定义值
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="values">自定义值</param>
        public static void Log(string message, object values)
        {
            Log(0,LogLevel.消息, message, values);
        }

        /// <summary>
        /// 记录 普通日志并附加自定义值和日志级别
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        /// <param name="message">消息</param>
        /// <param name="values">自定义值</param>
        public static void Log(LogLevel logLevel,string message, object values)
        {
            Log(0, logLevel, message, values);
        }

        /// <summary>
        /// 记录 普通日志并附加自定义值和日志类型
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="values">自定义值</param>
        /// <param name="logType">日志类型</param>
        public static void Log(int logType, string message, object values)
        {
            Log(logType, LogLevel.消息, message, values);
        }

        /// <summary>
        /// 记录 普通日志并附加自定义值和日志类型和级别
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="logLevel">日志级别</param>
        /// <param name="values">自定义值</param>
        /// <param name="logType">日志类型</param>
        public static void Log(int logType,LogLevel logLevel, string message, object values)
        {
            LogicalThreadContext.Properties["values"] = values.ToJson();
            LogicalThreadContext.Properties["level"] = (int)logLevel;
            LogicalThreadContext.Properties["type"] = logType;
            _logger.Info(message);
        }

        /// <summary>
        /// 记录 手动捕获的异常日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="exception">手动捕获的异常</param>
        public static void Log(string message, Exception exception)
        {
            Log(0, message, exception, "");
        }

        /// <summary>
        /// 记录 手动捕获的异常日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="exception">手动捕获的异常</param>
        /// <param name="logType">日志类型</param>
        public static void Log(int logType, string message, Exception exception)
        {
            Log(logType, message, exception, "");
        }

        /// <summary>
        /// 记录 手动捕获的异常日志并附加自定义值
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="exception">手动捕获的异常</param>
        /// <param name="values">自定义值</param>
        public static void Log(string message, Exception exception, object values)
        {
            Log(0, message, exception, values);
        }

        /// <summary>
        /// 记录 手动捕获的异常日志并附加自定义值和日志类型
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="exception">手动捕获的异常</param>
        /// <param name="values">自定义值</param>
        /// <param name="logType">日志类型</param>
        public static void Log(int logType, string message, Exception exception, object values)
        {
            LogicalThreadContext.Properties["values"] = values.ToJson();
            LogicalThreadContext.Properties["level"] = (int)LogLevel.警告;
            LogicalThreadContext.Properties["type"] = logType;
            _logger.Warn(string.Format("{0}【原因】：{1}", message, exception.Message), exception);
        }

        /// <summary>
        /// 记录 未捕获的异常日志
        /// </summary>
        /// <param name="exception">异常</param>
        public static void Log(Exception exception)
        {
            Log(exception, "");
        }

        /// <summary>
        /// 记录 未捕获的异常日志并附加自定义值
        /// </summary>
        /// <param name="exception">异常</param>
        /// <param name="values">自定义值</param>
        public static void Log(Exception exception, object values)
        {
            LogicalThreadContext.Properties["values"] = values.ToJson();
            LogicalThreadContext.Properties["level"] = (int)LogLevel.错误;
            LogicalThreadContext.Properties["type"] = 0;
            _logger.Fatal("未处理异常：" + exception.Message, exception);
        }

        /// <summary>
        /// 记录 调试日志
        /// </summary>
        /// <param name="message">消息</param>
        public static void Debug(string message)
        {
            Debug(message, "");
        }

        /// <summary>
        /// 记录 调试日志并附加自定义值
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="values">自定义值</param>
        public static void Debug(string message, object values)
        {
            LogicalThreadContext.Properties["values"] = values.ToJson();
            LogicalThreadContext.Properties["level"] = (int)LogLevel.调试;
            LogicalThreadContext.Properties["type"] = 0;
            _logger.Debug(message);
        }
    }
}
