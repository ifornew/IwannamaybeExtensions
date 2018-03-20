using Common.Extensions.Models.WechatPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions.Models.WechatProgram
{
    /// <summary>
    /// 加密数据解密 模型
    /// </summary>
    public class EncryptedDataMolde
    {
        /// <summary>
        /// 小程序OpenId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 唯一关联Id
        /// </summary>
        public string UnionId { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Sex Gender { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 水印
        /// </summary>
        public WaterMark WaterMark { get; set; }
    }

    /// <summary>
    /// 水印
    /// </summary>
    public class WaterMark
    {
        /// <summary>
        /// 小程序AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 水印时间戳
        /// </summary>
        public int TimeStamp { get; set; }
    }
}
