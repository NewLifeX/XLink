/*
 * XCoder v6.9.6298.42194
 * 作者：nnhy/X3
 * 时间：2017-03-31 23:16:13
 * 版权：版权所有 (C) 新生命开发团队 2002~2017
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NewLife.Common;
using NewLife.Data;
using NewLife.Model;
using XCode;
using XCode.Cache;
using XCode.Membership;

namespace xLink.Entity
{
    /// <summary>设备</summary>
    public partial class Device : Entity<Device>, IAuthUser
    {
        #region 对象操作
        static Device()
        {
            var df = Meta.Factory.AdditionalFields;
            df.Add(__.Logins);
            //df.Add(__.Registers);

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

            if (Name.IsNullOrEmpty()) throw new ArgumentNullException(__.Name, _.Name.DisplayName + "不能为空！");
            //if (Password.IsNullOrEmpty()) throw new ArgumentNullException(__.Password, _.Password.DisplayName + "不能为空！");
            //if (Password.Length != 16 && Password.Length != 32) throw new ArgumentOutOfRangeException(__.Password, _.Password.DisplayName + "非法！");
            //if (Name.Length < 8) throw new ArgumentOutOfRangeException(__.Name, _.Name.DisplayName + "最短8个字符！" + Name);
            //if (Name.Length > 16) throw new ArgumentOutOfRangeException(__.Name, _.Name.DisplayName + "最长16个字符！" + Name);

            //// 修正显示名
            //if (!NickName.IsNullOrEmpty() && NickName.Length > 16) NickName = NickName.Substring(0, 16);

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);
        }
        #endregion

        #region 扩展属性
        /// <summary>登录地址。IP=>Address</summary>
        [DisplayName("登录地址")]
        public String LastLoginAddress { get { return LastLoginIP.IPToAddress(); } }

        String IManageUser.Name { get => Code; set => Code = value; }
        String IManageUser.NickName { get => Name; set => Name = value; }
        String IAuthUser.Password { get => Secret; set => Secret = value; }
        Boolean IAuthUser.Online { get; set; }
        String IAuthUser.RegisterIP { get => CreateIP; set => CreateIP = value; }
        DateTime IAuthUser.RegisterTime { get => CreateTime; set => CreateTime = value; }
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Device FindByID(Int32 id)
        {
            if (Meta.Count < 1000) return Meta.Cache.Entities.FirstOrDefault(e => e.ID == id);

            // 单对象缓存
            return Meta.SingleCache[id];
        }

        /// <summary>根据名称。登录用户名查找</summary>
        /// <param name="name">名称。登录用户名</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Device FindByName(String name)
        {
            if (Meta.Count < 1000) return Meta.Cache.Entities.FirstOrDefault(e => e.Name == name);

            return Find(__.Name, name);
        }

        ///// <summary>根据唯一编码查找</summary>
        ///// <param name="code">唯一编码</param>
        ///// <returns></returns>
        //[DataObjectMethod(DataObjectMethodType.Select, false)]
        //public static Device FindByCode(String code)
        //{
        //    if (Meta.Count < 1000) return Meta.Cache.Entities.FirstOrDefault(e => e.Code == code);

        //    return Find(__.Code, code);
        //}
        #endregion

        #region 高级查询
        /// <summary>高级查询</summary>
        /// <param name="productId"></param>
        /// <param name="enable"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="key"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IList<Device> Search(Int32 productId, Boolean? enable, DateTime start, DateTime end, String key, PageParameter param)
        {
            var exp = SearchWhereByKeys(key, null, null);

            if (productId >= 0) exp &= _.ProductID == productId;
            if (enable != null) exp &= _.Enable == enable.Value;

            exp &= _.UpdateTime.Between(start, end);

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
        public override String ToString() => Name ?? Code;
        #endregion
    }
}