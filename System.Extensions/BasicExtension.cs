using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace System.Extensions
{
    /// <summary>
    /// 扩展系统方法 类
    /// </summary>
    public static class BasicExtension
    {
        #region int
        /// <summary>
        /// 转为 decimal
        /// </summary>
        /// <param name="input">整型实体</param>
        /// <returns></returns>
        public static decimal ToDecimal(this int input)
        {
            return Convert.ToDecimal(input);
        }
        #endregion

        #region string
        /// <summary>
        /// 判断字符串是否可以转换为整形
        /// </summary>
        /// <param name="input">字符串实体</param>
        /// <returns></returns>
        public static bool IsInt(this string input)
        {
            int i;
            return int.TryParse(input, out i);
        }

        /// <summary>
        /// 字符串转整形（int32），失败时返回：0
        /// </summary>
        /// <param name="input">字符串实体</param>
        /// <returns></returns>
        public static int ToInt(this string input)
        {
            int outPut;
            int.TryParse(input, out outPut);
            return outPut;
        }

        /// <summary>
        /// 字符串转无符号整形（uint32），失败时返回：0
        /// </summary>
        /// <param name="input">字符串实体</param>
        /// <returns></returns>
        public static uint ToUint(this string input)
        {
            uint outPut;
            uint.TryParse(input, out outPut);
            return outPut;
        }

        /// <summary>
        /// 字符串转换为boolean（转换失败时抛出异常）
        /// </summary>
        /// <param name="input">字符串实体</param>
        /// <returns></returns>
        public static bool ToBoolean(this string input)
        {
            if (input.Equals("true", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            else if (input.Equals("false", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }
            else
            {
                throw new FormatException("字符串不满足转换为boolean的格式要求");
            }
        }

        /// <summary>
        /// 判断是否匹配给定的正则表达式
        /// </summary>
        /// <param name="input">字符串实体</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        public static bool IsMatch(this string input, string pattern)
        {
            return input == null ? false : Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// 找出满足正则表达式匹配的部分
        /// </summary>
        /// <param name="input">字符串实体</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        public static string Match(this string input, string pattern)
        {
            return input == null ? string.Empty : Regex.Match(input, pattern).Value;
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="input">字符串实体</param>
        /// <returns></returns>
        public static string ToCamel(this string input)
        {
            return string.IsNullOrEmpty(input) ? input : input[0].ToString().ToLower() + input.Substring(1);
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="input">字符串实体</param>
        /// <returns></returns>
        public static string ToPascal(this string input)
        {
            return string.IsNullOrEmpty(input) ? input : input[0].ToString().ToUpper() + input.Substring(1);
        }

        /// <summary>
        /// Json字符串转换为对象
        /// </summary>
        /// <typeparam name="T">对象泛型</typeparam>
        /// <param name="input">字符串实体</param>
        /// <returns></returns>
        public static T ToJsonResult<T>(this string input)
        {
            return new JavaScriptSerializer() { MaxJsonLength = int.MaxValue }.Deserialize<T>(input);
        }

        /// <summary>
        /// xml字符串转对象
        /// </summary>
        /// <typeparam name="T">对象泛型</typeparam>
        /// <param name="input">xml字符串</param>
        /// <returns></returns>
        public static T ToXmlResult<T>(this string input)
        {
            using (StringReader stringReader = new StringReader(input))
            {
                return (T)new XmlSerializer(typeof(T)).Deserialize(stringReader);
            }
        }

        /// <summary>
        /// MD5加密(UTF8编码)
        /// </summary>
        /// <param name="input">字符串实体</param>
        /// <param name="length32">16/32位加密长度</param>
        /// <param name="upperCase">大写密文</param>
        /// <returns>MD5加密字符串</returns>
        public static string ToMD5(this string input, bool length32 = true, bool upperCase = false)
        {
            //string crypt = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input))).Replace("-", "");
            //return upperCase ? crypt : crypt.ToLower();

            using (MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider())
            {
                string result;
                if (length32)
                {
                    result = BitConverter.ToString(md5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(input))).Replace("-", "");
                }
                else
                {
                    result = BitConverter.ToString(md5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(input)), 4, 8).Replace("-", "");
                }
                return upperCase ? result : result.ToLower();
            }
        }

        /// <summary>
        /// SHA1 加密
        /// </summary>
        /// <param name="input">字符串实体</param>
        /// <param name="upperCase">大写密文</param>
        /// <returns>SHA1加密字符串</returns>
        public static string ToSHA1(this string input, bool upperCase = false)
        {
            try
            {
                using (SHA1 sha1 = new SHA1CryptoServiceProvider())
                {
                    string result = BitConverter.ToString(sha1.ComputeHash(Encoding.Default.GetBytes(input))).Replace("-", "");
                    return upperCase ? result : result.ToLower();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 将字符串写入到文件(UTF8)
        /// </summary>
        /// <param name="input">字符串实体</param>
        /// <param name="filePath">文件完整路径</param>
        /// <param name="append">追加模式</param>
        public static void ToFile(this string input, string filePath, bool append = true)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            if (append)
            {
                File.AppendAllText(filePath, input);
            }
            else
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath, append) { AutoFlush = true })
                {
                    streamWriter.Write(input);
                }
            }
        }
        #endregion

        #region enum
        /// <summary>
        /// 获取枚举的备注信息
        /// </summary>
        /// <param name="input">枚举</param>
        /// <returns></returns>
        public static string ToDescription(this Enum input)
        {
            foreach (MemberInfo memberInfo in input.GetType().GetMembers())
            {
                if (memberInfo.Name == input.GetType().GetEnumName(input))
                {
                    foreach (Attribute attribute in Attribute.GetCustomAttributes(memberInfo))
                    {
                        if (attribute.GetType() == typeof(DescriptionAttribute))
                        {
                            return ((DescriptionAttribute)attribute).Description;
                        }
                    }
                }
            }
            return string.Empty;
        }
        #endregion

        #region TResult
        /// <summary>
        /// 对象转Json字符串
        /// </summary>
        /// <typeparam name="T">对象泛型</typeparam>
        /// <param name="input">对象</param>
        /// <returns></returns>
        public static string ToJson<T>(this T input)
        {
            try
            {
                /*
                MatchEvaluator matchEvaluator = new MatchEvaluator((Match match) =>
                {
                    string result = string.Empty;
                    DateTime dt = new DateTime(1970, 1, 1);
                    dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                    dt = dt.ToLocalTime();
                    result = dt.ToString("yyyy-MM-dd HH:mm:ss");
                    return result;
                });
                Regex regex = new Regex(@"\\/Date\((\d+)\)\\/");
                return regex.Replace(new JavaScriptSerializer() { MaxJsonLength = int.MaxValue }.Serialize(input), matchEvaluator);
                */
                return new JavaScriptSerializer() { MaxJsonLength = int.MaxValue }.Serialize(input);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 转换为xml字符串
        /// </summary>
        /// <typeparam name="T">对象泛型</typeparam>
        /// <param name="input">对象</param>
        /// <returns></returns>
        public static string ToXml<T>(this T input)
        {
            TextWriter textWriter = new StringWriter();
            new XmlSerializer(typeof(T)).Serialize(textWriter, input);
            return textWriter.ToString();
        }

        /// <summary>
        /// 将对象的Public属性转换为key-value字符串键值对
        /// </summary>
        /// <param name="input">对象</param>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="allowNull">是否转换空值（null）</param>
        /// <returns>字典</returns>
        public static Dictionary<string, string> ToDictionary<T>(this T input, bool allowNull = false)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            PropertyInfo[] propertyInfos = input.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.CanRead && (allowNull || propertyInfo.GetValue(input) != null))
                {
                    dictionary.Add(propertyInfo.Name, propertyInfo.GetValue(input) == null ? null : propertyInfo.GetValue(input).ToString());
                }
            }
            return dictionary;
        }

        /// <summary>
        /// 将一个对象的Public属性复制给另一个对象的相同Public属性上
        /// </summary>
        /// <typeparam name="T">输出对象泛型</typeparam>
        /// <param name="input">被复制对象</param>
        /// <param name="TOutput">复制到的对象</param>
        /// <param name="skip">要忽略（跳过）的属性</param>
        /// <param name="allowNull">是否复制空值（null）</param>
        /// <returns></returns>
        public static T ToTResult<T>(this object input, T TOutput, string[] skip = null, bool allowNull = false)
        {
            PropertyInfo[] propertyInfos = input.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] tPropertyInfos = TOutput.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.CanRead && (allowNull || propertyInfo.GetValue(input) != null) && (skip == null || !skip.Contains(propertyInfo.Name)))
                {
                    foreach (PropertyInfo tPropertyInfo in tPropertyInfos)
                    {
                        if (tPropertyInfo.CanWrite && tPropertyInfo.Name == propertyInfo.Name && tPropertyInfo.PropertyType == propertyInfo.PropertyType)
                        {
                            tPropertyInfo.SetValue(TOutput, propertyInfo.GetValue(input));
                        }
                    }
                }
            }
            return TOutput;
        }
        #endregion
    }
}