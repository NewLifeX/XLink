using System;
using System.Collections.Generic;
using System.Linq;
using NewLife.Net;
using NewLife.Remoting;
using XCode;

namespace xLink.Services
{
    /// <summary>物联服务接口</summary>
    public interface ILinkService
    {
        /// <summary>清理超时会话</summary>
        /// <param name="secTimeout"></param>
        /// <returns></returns>
        Int32 ClearExpire(Int32 secTimeout);
    }

    /// <summary>物联服务</summary>
    public abstract class LinkService : ILinkService, IApi
    {
        #region 属性
        /// <summary>接口会话</summary>
        public IApiSession Session { get; set; }

        /// <summary>请求参数</summary>
        public IDictionary<String, Object> Parameters { get; set; }
        #endregion

        #region 登录
        #endregion

        #region 心跳
        #endregion

        #region 清理超时
        /// <summary>清理超时会话</summary>
        /// <param name="secTimeout"></param>
        /// <returns></returns>
        public virtual Int32 ClearExpire(Int32 secTimeout) => 0;
        #endregion

        #region 异常处理
        /// <summary>抛出异常</summary>
        /// <param name="errCode"></param>
        /// <param name="msg"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected ApiException Error(Int32 errCode, String msg, Object result = null)
        {
            var ex = new ApiException(errCode, msg);
            if (result != null)
            {
                // 支持自定义类型
                foreach (var item in result.ToDictionary())
                {
                    ex.Data[item.Key] = item.Value;
                }
            }

            return ex;
        }
        #endregion

        #region 辅助
        /// <summary>写日志</summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        protected void WriteLog(String format, params Object[] args)
        {
            var ns = Session as NetSession;
            ns?.WriteLog(format, args);
        }
        #endregion
    }
}