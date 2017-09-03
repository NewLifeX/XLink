using System;

namespace xLink.Models
{
    /// <summary>数据模型</summary>
    public class DataModel
    {
        /// <summary>设备</summary>
        public String ID { get; set; }

        /// <summary>起始位置</summary>
        public Int32 Start { get; set; }

        /// <summary>数据</summary>
        public String Data { get; set; }
    }
}