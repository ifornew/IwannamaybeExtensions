using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Common.Extensions.Models
{
      /// <summary>
      /// Ip所属的位置信息 （EasyIp）模型
      /// </summary>
      public class LocationForIp
      {
            /// <summary>
            /// 国家
            /// </summary>
            [Display(Name = "国家", Description = "所在国家")]
            public string Country { get; set; }

            /// <summary>
            /// 省份
            /// </summary>
            [Display(Name = "省份", Description = "所在省份")]
            public string Province { get; set; }

            /// <summary>
            /// 城市
            /// </summary>
            [Display(Name = "城市", Description = "所在城市")]
            public string City { get; set; }
      }
}
