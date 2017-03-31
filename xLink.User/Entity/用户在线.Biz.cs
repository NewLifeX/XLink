/*
 * XCoder v6.9.6298.42194
 * 作者：nnhy/X3
 * 时间：2017-03-31 21:55:42
 * 版权：版权所有 (C) 新生命开发团队 2002~2017
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using NewLife.Data;
using XCode;
using XCode.Cache;
using XCode.Membership;

namespace xLink.User.Entity
{
    /// <summary>用户在线</summary>
    public partial class UserOnline : Entity<UserOnline>
    {
        #region 对象操作


        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
            //if (String.IsNullOrEmpty(Name)) throw new ArgumentNullException(_.Name, _.Name.DisplayName + "无效！");
            //if (!isNew && ID < 1) throw new ArgumentOutOfRangeException(_.ID, _.ID.DisplayName + "必须大于0！");

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);

            // 在新插入数据或者修改了指定字段时进行唯一性验证，CheckExist内部抛出参数异常
            //if (isNew || Dirtys[__.Name]) CheckExist(__.Name);

            // 处理当前已登录用户信息
            if (!Dirtys[__.UserID] && ManageProvider.Provider.Current != null) UserID = (Int32)ManageProvider.Provider.Current.ID;
            if (isNew && !Dirtys[__.CreateTime]) CreateTime = DateTime.Now;
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    base.InitData();

        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    // Meta.Count是快速取得表记录数
        //    if (Meta.Count > 0) return;

        //    // 需要注意的是，如果该方法调用了其它实体类的首次数据库操作，目标实体类的数据初始化将会在同一个线程完成
        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}[{1}]数据……", typeof(UserOnline).Name, Meta.Table.DataTable.DisplayName);

        //    var entity = new UserOnline();
        //    entity.UserID = 0;
        //    entity.Name = "abc";
        //    entity.Ver = "abc";
        //    entity.Type = "abc";
        //    entity.SessionID = 0;
        //    entity.InternalUri = "abc";
        //    entity.ExternalUri = "abc";
        //    entity.LoginCount = 0;
        //    entity.PingCount = 0;
        //    entity.LoginTime = DateTime.Now;
        //    entity.LastActive = DateTime.Now;
        //    entity.CreateTime = DateTime.Now;
        //    entity.ErrorCount = 0;
        //    entity.LastError = "abc";
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", typeof(UserOnline).Name, Meta.Table.DataTable.DisplayName);
        //}


        ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
        ///// <returns></returns>
        //public override Int32 Insert()
        //{
        //    return base.Insert();
        //}

        ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        ///// <returns></returns>
        //protected override Int32 OnInsert()
        //{
        //    return base.OnInsert();
        //}

        #endregion

        #region 扩展属性
        /// <summary>地址。IP=>Address</summary>
        [DisplayName("地址")]
        public String ExternalAddress { get { return ExternalUri.IPToAddress(); } }
        #endregion

        #region 扩展查询

        /// <summary>根据会话查找</summary>
        /// <param name="sessionid">会话</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EntityList<UserOnline> FindAllBySessionID(Int32 sessionid)
        {
            if (Meta.Count >= 1000)
                return FindAll(__.SessionID, sessionid);
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(__.SessionID, sessionid);
        }

        /// <summary>根据编码查找</summary>
        /// <param name="userid">编码</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EntityList<UserOnline> FindAllByUserID(Int32 userid)
        {
            if (Meta.Count >= 1000)
                return FindAll(__.UserID, userid);
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(__.UserID, userid);
        }

        /// <summary>根据名称查找</summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EntityList<UserOnline> FindAllByName(String name)
        {
            if (Meta.Count >= 1000)
                return FindAll(__.Name, name);
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(__.Name, name);
        }

        /// <summary>根据类型查找</summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EntityList<UserOnline> FindAllByType(String type)
        {
            if (Meta.Count >= 1000)
                return FindAll(__.Type, type);
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(__.Type, type);
        }

        #endregion

        #region 高级查询
        // 以下为自定义高级查询的例子

        /// <summary>查询满足条件的记录集，分页、排序</summary>
        /// <param name="type">类型</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="key">关键字</param>
        /// <param name="param">分页排序参数，同时返回满足条件的总记录数</param>
        /// <returns>实体集</returns>
        public static EntityList<UserOnline> Search(String type, DateTime start, DateTime end, String key, PageParameter param)
        {
            // 修改UserID排序为名称
            //param = new PageParameter(param);
            if (param.Sort.EqualIgnoreCase(__.UserID)) param.Sort = __.Name;

            var list = Search(type, start, end, key, param, false);
            // 如果结果为0，并且有key，则使用扩展查询，对内网外网地址进行模糊查询
            if (list.Count == 0 && !key.IsNullOrEmpty()) list = Search(type, start, end, key, param, true);

            // 换回来，避免影响生成升序降序
            if (param.Sort.EqualIgnoreCase(__.Name)) param.Sort = __.UserID;

            return list;
        }

        private static EntityList<UserOnline> Search(String type, DateTime start, DateTime end, String key, PageParameter param, Boolean ext)
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
        static FieldCache<UserOnline> TypeCache = new FieldCache<UserOnline>(_.Type);

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
        public static EntityList<UserOnline> ClearExpire(Int32 secTimeout)
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