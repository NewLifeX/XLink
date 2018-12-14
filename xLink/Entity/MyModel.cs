using System;
using NewLife.Model;
using xLink.Models;

namespace xLink.Entity
{
    /// <summary>接口</summary>
    public partial interface IMyModel : IManageUser
    {
        #region 属性
        ///// <summary>唯一编码</summary>
        //String Code { get; set; }

        /// <summary>类型</summary>
        String Type { get; set; }

        /// <summary>版本</summary>
        String Version { get; set; }

        ///// <summary>数据</summary>
        //String Data { get; set; }

        /// <summary>注册次数</summary>
        Int32 Registers { get; set; }

        ///// <summary>创建者</summary>
        //Int32 CreateUserID { get; set; }

        ///// <summary>创建时间</summary>
        //DateTime CreateTime { get; set; }

        ///// <summary>创建地址</summary>
        //String CreateIP { get; set; }

        ///// <summary>更新者</summary>
        //Int32 UpdateUserID { get; set; }

        ///// <summary>更新时间</summary>
        //DateTime UpdateTime { get; set; }

        ///// <summary>更新地址</summary>
        //String UpdateIP { get; set; }
        #endregion
    }

    /// <summary>历史接口</summary>
    public interface IMyHistory : IHistory
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        ///// <summary>编码</summary>
        //Int32 DeviceID { get; set; }

        /// <summary>版本</summary>
        String Version { get; set; }

        /// <summary>网络类型</summary>
        String NetType { get; set; }

        ///// <summary>创建者</summary>
        //Int32 CreateDeviceID { get; set; }
        #endregion
    }

    /// <summary>在线接口</summary>
    public interface IMyOnline : IOnline
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        ///// <summary>编码</summary>
        //Int32 DeviceID { get; set; }

        /// <summary>版本</summary>
        String Version { get; set; }

        /// <summary>网络类型</summary>
        String NetType { get; set; }

        /// <summary>内网</summary>
        String InternalUri { get; set; }

        /// <summary>外网</summary>
        String ExternalUri { get; set; }

        /// <summary>登录</summary>
        Int32 LoginCount { get; set; }

        /// <summary>心跳</summary>
        Int32 PingCount { get; set; }

        /// <summary>登录时间</summary>
        DateTime LoginTime { get; set; }

        /// <summary>错误</summary>
        Int32 ErrorCount { get; set; }

        /// <summary>最后错误</summary>
        String LastError { get; set; }
        #endregion
    }
}