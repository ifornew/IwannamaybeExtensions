using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4Net.Extensions.Models
{
    /// <summary>
    /// Sql参数 模型
    /// </summary>
    public class SqlParameter
    {
        /// <summary>
        /// 参数名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public object Value { get; set; }
    }
}
