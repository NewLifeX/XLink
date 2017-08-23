/*
 * XCoder v6.9.6298.42194
 * 作者：nnhy/X3
 * 时间：2017-03-31 22:29:16
 * 版权：版权所有 (C) 新生命开发团队 2002~2017
*/
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using NewLife.Log;
using NewLife.Web;
﻿using NewLife.Data;
using XCode;
using XCode.Configuration;
using XCode.Membership;
using XCode.Cache;

namespace xLink.Master.Entity
{
    /// <summary>服务器历史</summary>
    public partial class ServerHistory : Entity<ServerHistory>
    {
        #region 对象操作
        static ServerHistory()
        {
            Meta.Table.DataTable.InsertOnly = true;
        }

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
        [DisplayName("创建地址")]
        public String CreateAddress { get { return CreateIP.IPToAddress(); } }
        #endregion

        #region 扩展查询
        #endregion

        #region 高级查询
        /// <summary>高级搜索</summary>
        /// <param name="userid"></param>
        /// <param name="type"></param>
        /// <param name="action"></param>
        /// <param name="result"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="key"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IList<ServerHistory> Search(Int32 userid, String type, String action, Int32 result, DateTime start, DateTime end, String key, PageParameter param)
        {
            var list = Search(userid, type, action, result, start, end, key, param, false);
            // 如果结果为0，并且有key，则使用扩展查询，对内网外网地址进行模糊查询
            if (list.Count == 0 && !key.IsNullOrEmpty()) list = Search(userid, type, action, result, start, end, key, param, true);

            return list;
        }

        private static IList<ServerHistory> Search(Int32 tokenid, String type, String action, Int32 result, DateTime start, DateTime end, String key, PageParameter param, Boolean ext)
        {
            var exp = new WhereExpression();

            if (tokenid >= 0) exp &= _.ServerID == tokenid;
            if (!type.IsNullOrEmpty()) exp &= _.Type == type;
            if (!action.IsNullOrEmpty()) exp &= _.Action == action;
            if (result == 0)
                exp &= _.Success == false;
            else if (result == 1)
                exp &= _.Success == true;

            exp &= _.CreateTime.Between(start, end);

            if (!key.IsNullOrEmpty())
            {
                if (ext)
                    exp &= (_.Name.Contains(key) | _.Remark.Contains(key) | _.CreateIP.Contains(key));
                else
                    exp &= _.Name.StartsWith(key);
            }

            return FindAll(exp, param);
        }
        #endregion

        #region 扩展操作
        /// <summary>类别名实体缓存，异步，缓存10分钟</summary>
        static FieldCache<ServerHistory> TypeCache = new FieldCache<ServerHistory>(_.Type);

        /// <summary>获取所有类别名称</summary>
        /// <returns></returns>
        public static IDictionary<String, String> FindAllTypeName()
        {
            return TypeCache.FindAllName();
        }

        /// <summary>类别名实体缓存，异步，缓存10分钟</summary>
        static FieldCache<ServerHistory> ActionCache = new FieldCache<ServerHistory>(_.Action);

        /// <summary>获取所有类别名称</summary>
        /// <returns></returns>
        public static IDictionary<String, String> FindAllActionName()
        {
            return ActionCache.FindAllName();
        }
        #endregion

        #region 业务
        #endregion
    }
}