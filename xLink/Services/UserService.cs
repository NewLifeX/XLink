using System;
using xLink.Entity;

namespace xLink.Services
{
    /// <summary>用户服务</summary>
    public class UserService : LinkService<UserSession>
    {
        #region 清理超时
        /// <summary>清理超时会话</summary>
        /// <param name="secTimeout"></param>
        /// <returns></returns>
        public override Int32 ClearExpire(Int32 secTimeout) => UserOnline.ClearExpire(secTimeout).Count;
        #endregion
    }
}