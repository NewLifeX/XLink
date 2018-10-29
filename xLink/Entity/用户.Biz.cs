/*
 * XCoder v6.9.6298.42194
 * 作者：nnhy/X3
 * 时间：2017-03-31 23:16:20
 * 版权：版权所有 (C) 新生命开发团队 2002~2017
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NewLife.Data;
using NewLife.Model;
using XCode;
using XCode.Membership;

namespace xLink.Entity
{
    /// <summary>用户</summary>
    public partial class User : Entity<User>, IMyModel, IAuthUser
    {
        #region 对象操作
        static User()
        {
            var df = Meta.Factory.AdditionalFields;
            df.Add(__.Logins);
            df.Add(__.Registers);

            var sc = Meta.SingleCache;
            sc.FindSlaveKeyMethod = e => Find(__.Name, e);
            sc.GetSlaveKeyMethod = e => e.Name;
            sc.SlaveKeyIgnoreCase = false;

            Meta.Modules.Add<UserModule>();
            Meta.Modules.Add<TimeModule>();
            Meta.Modules.Add<IPModule>();
        }

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

            //if (!Dirtys[__.LastLoginIP]) LastLoginIP = WebHelper.UserHost;
            //if (!Dirtys[__.RegisterIP]) RegisterIP = WebHelper.UserHost;
            //if (isNew && !Dirtys[__.CreateTime]) CreateTime = DateTime.Now;
            //if (!Dirtys[__.CreateIP]) CreateIP = WebHelper.UserHost;
            //if (!Dirtys[__.UpdateTime]) UpdateTime = DateTime.Now;
            //if (!Dirtys[__.UpdateIP]) UpdateIP = WebHelper.UserHost;
        }
        #endregion

        #region 扩展属性


        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static User FindByID(Int32 id)
        {
            if (Meta.Count < 1000) return Meta.Cache.Entities.FirstOrDefault(e => e.ID == id);

            // 单对象缓存
            return Meta.SingleCache[id];
        }

        /// <summary>根据名称。登录用户名查找</summary>
        /// <param name="name">名称。登录用户名</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static User FindByName(String name)
        {
            if (Meta.Count < 1000) return Meta.Cache.Entities.FirstOrDefault(e => e.Name == name);

            return Find(__.Name, name);
        }

        /// <summary>根据类型查找</summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static IList<User> FindAllByType(String type)
        {
            if (Meta.Count >= 1000)
                return FindAll(__.Type, type);
            else // 实体缓存
                return Meta.Cache.Entities.Where(e => e.Type == type).ToList();
        }
        #endregion

        #region 高级查询
        /// <summary>高级查询</summary>
        /// <param name="type"></param>
        /// <param name="enable"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="key"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IList<User> Search(String type, Boolean? enable, DateTime start, DateTime end, String key, PageParameter param)
        {
            // WhereExpression重载&和|运算符，作为And和Or的替代
            // SearchWhereByKeys系列方法用于构建针对字符串字段的模糊搜索，第二个参数可指定要搜索的字段
            var exp = SearchWhereByKeys(key, null, null);

            if (!type.IsNullOrEmpty()) exp &= _.Type == type;
            if (enable != null) exp &= _.Enable == enable.Value;

            //exp &= _.CreateTime.Between(start, end);
            exp &= _.LastLogin.Between(start, end);

            return FindAll(exp, param);
        }
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        #endregion

        #region 辅助
        /// <summary>显示友好名称</summary>
        /// <returns></returns>
        public override String ToString()
        {
            if (!NickName.IsNullOrEmpty()) return NickName;

            return Name;
        }
        #endregion
    }
}