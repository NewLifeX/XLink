/*
 * XCoder v6.9.6298.42194
 * 作者：nnhy/X3
 * 时间：2017-03-31 21:55:42
 * 版权：版权所有 (C) 新生命开发团队 2002~2017
*/
using NewLife;
using NewLife.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Cache;
using XCode.Membership;
using xLink.Models;

namespace xLink.Entity
{
    /// <summary>用户在线</summary>
    public partial class UserOnline : Entity<UserOnline>, IOnline
    {
        #region 对象操作
        static UserOnline()
        {
            var df = Meta.Factory.AdditionalFields;
            df.Add(__.LoginCount);
            df.Add(__.PingCount);

            Meta.Modules.Add<TimeModule>();
            Meta.Modules.Add<IPModule>();

            var sc = Meta.SingleCache;
            sc.FindSlaveKeyMethod = k => Find(_.SessionID == k);
            sc.GetSlaveKeyMethod = e => e.SessionID;
        }
        #endregion

        #region 扩展属性
        /// <summary>用户</summary>
        public User User => Extends.Get(nameof(User), k => User.FindByID(UserID));

        /// <summary>用户名</summary>
        [Map(__.UserID)]
        public String UserName => User + "";

        /// <summary>地址。IP=>Address</summary>
        [DisplayName("地址")]
        public String ExternalAddress => ExternalUri.IPToAddress();
        #endregion

        #region 扩展查询
        /// <summary>根据会话查找</summary>
        /// <param name="sessionid">会话</param>
        /// <param name="cache">是否走缓存</param>
        /// <returns></returns>
        public static UserOnline FindBySessionID(String sessionid, Boolean cache = true)
        {
            if (!cache) return Find(_.SessionID == sessionid);

            return Meta.SingleCache.GetItemWithSlaveKey(sessionid) as UserOnline;
        }
        #endregion

        #region 高级查询
        /// <summary>查询满足条件的记录集，分页、排序</summary>
        /// <param name="type">类型</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="key">关键字</param>
        /// <param name="param">分页排序参数，同时返回满足条件的总记录数</param>
        /// <returns>实体集</returns>
        public static IList<UserOnline> Search(String type, DateTime start, DateTime end, String key, PageParameter param)
        {
            var exp = new WhereExpression();

            //if (!type.IsNullOrEmpty()) exp &= _.Type == type;

            exp &= _.CreateTime.Between(start, end);

            if (!key.IsNullOrEmpty())
                exp &= (_.Name.Contains(key) | _.InternalUri.Contains(key) | _.ExternalUri.Contains(key));

            return FindAll(exp, param);
        }
        #endregion

        #region 扩展操作
        /// <summary>获取 或 添加</summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public static UserOnline GetOrAdd(String sessionId) => GetOrAdd(sessionId, FindBySessionID, k => new UserOnline { SessionID = k });
        #endregion

        #region 业务
        /// <summary>删除过期，指定过期时间</summary>
        /// <param name="secTimeout">超时时间，秒</param>
        /// <returns></returns>
        public static IList<UserOnline> ClearExpire(Int32 secTimeout)
        {
            // 10分钟不活跃将会被删除
            var exp = _.UpdateTime < DateTime.Now.AddSeconds(-secTimeout);
            var list = FindAll(exp, null, null, 0, 0);
            list.Delete();

            return list;
        }
        #endregion
    }
}