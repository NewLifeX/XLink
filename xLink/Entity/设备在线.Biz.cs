/*
 * XCoder v6.9.6298.42194
 * 作者：nnhy/X3
 * 时间：2017-03-31 22:14:32
 * 版权：版权所有 (C) 新生命开发团队 2002~2017
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NewLife.Data;
using NewLife.Model;
using XCode;
using XCode.Cache;

namespace xLink.Entity
{
    /// <summary>设备在线</summary>
    public partial class DeviceOnline : Entity<DeviceOnline>, IOnline
    {
        #region 对象操作
        ///// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        ///// <param name="isNew"></param>
        //public override void Valid(Boolean isNew)
        //{
        //    // 如果没有脏数据，则不需要进行任何处理
        //    if (!HasDirty) return;

        //    // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
        //    base.Valid(isNew);
        //}
        #endregion

        #region 扩展属性
        /// <summary>地址。IP=>Address</summary>
        [DisplayName("地址")]
        public String ExternalAddress { get { return ExternalUri.IPToAddress(); } }

        Int32 IOnline.UserID { get => DeviceID; set => DeviceID = value; }
        #endregion

        #region 扩展查询
        /// <summary>根据会话查找</summary>
        /// <param name="sessionid">会话</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DeviceOnline FindBySessionID(Int32 sessionid)
        {
            if (Meta.Count >= 1000)
                return Find(__.SessionID, sessionid);
            else // 实体缓存
                return Meta.Cache.Entities.FirstOrDefault(e => e.SessionID == sessionid);
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
        public static IList<DeviceOnline> Search(String type, DateTime start, DateTime end, String key, PageParameter param)
        {
            // 修改DeviceID排序为名称
            //param = new PageParameter(param);
            if (param.Sort.EqualIgnoreCase(__.DeviceID)) param.Sort = __.Name;

            var list = Search(type, start, end, key, param, false);
            // 如果结果为0，并且有key，则使用扩展查询，对内网外网地址进行模糊查询
            if (list.Count == 0 && !key.IsNullOrEmpty()) list = Search(type, start, end, key, param, true);

            // 换回来，避免影响生成升序降序
            if (param.Sort.EqualIgnoreCase(__.Name)) param.Sort = __.DeviceID;

            return list;
        }

        private static IList<DeviceOnline> Search(String type, DateTime start, DateTime end, String key, PageParameter param, Boolean ext)
        {
            var exp = new WhereExpression();

            if (!type.IsNullOrEmpty()) exp &= _.Type == type;

            exp &= _.CreateTime.Between(start, end);

            if (!key.IsNullOrEmpty())
            {
                if (ext)
                    exp &= (_.Name.Contains(key) | _.InternalUri.Contains(key) | _.ExternalUri.Contains(key));
                else
                    exp &= _.Name.StartsWith(key);
            }

            return FindAll(exp, param);
        }
        #endregion

        #region 扩展操作
        /// <summary>类别名实体缓存，异步，缓存10分钟</summary>
        static FieldCache<DeviceOnline> TypeCache = new FieldCache<DeviceOnline>(_.Type);

        /// <summary>获取所有类别名称</summary>
        /// <returns></returns>
        public static IDictionary<String, String> FindAllTypeName()
        {
            return TypeCache.FindAllName();
        }
        #endregion

        #region 业务
        /// <summary>删除过期，指定过期时间</summary>
        /// <param name="secTimeout">超时时间，秒</param>
        /// <returns></returns>
        public static IList<DeviceOnline> ClearExpire(Int32 secTimeout)
        {
            // 10分钟不活跃将会被删除
            var exp = _.LastActive < DateTime.Now.AddSeconds(-secTimeout) | _.LastActive.IsNull();
            var list = FindAll(exp, null, null, 0, 0);
            list.Delete();

            return list;
        }
        #endregion
    }
}