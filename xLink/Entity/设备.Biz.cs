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
    public partial class Device : Entity<Device>, IMyModel, IAuthUser
    {
        #region 对象操作
        static Device()
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

            if (Name.IsNullOrEmpty()) throw new ArgumentNullException(__.Name, _.Name.DisplayName + "不能为空！");
            if (Password.IsNullOrEmpty()) throw new ArgumentNullException(__.Password, _.Password.DisplayName + "不能为空！");
            //if (Password.Length != 16 && Password.Length != 32) throw new ArgumentOutOfRangeException(__.Password, _.Password.DisplayName + "非法！");
            //if (Name.Length < 8) throw new ArgumentOutOfRangeException(__.Name, _.Name.DisplayName + "最短8个字符！" + Name);
            //if (Name.Length > 16) throw new ArgumentOutOfRangeException(__.Name, _.Name.DisplayName + "最长16个字符！" + Name);

            // 修正显示名
            if (!NickName.IsNullOrEmpty() && NickName.Length > 16) NickName = NickName.Substring(0, 16);

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);
        }
        #endregion

        #region 扩展属性
        /// <summary>登录地址。IP=>Address</summary>
        [DisplayName("登录地址")]
        public String LastLoginAddress { get { return LastLoginIP.IPToAddress(); } }
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

        /// <summary>根据唯一编码查找</summary>
        /// <param name="code">唯一编码</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Device FindByCode(String code)
        {
            if (Meta.Count < 1000) return Meta.Cache.Entities.FirstOrDefault(e => e.Code == code);

            return Find(__.Code, code);
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
        public static IList<Device> Search(String type, Boolean? enable, DateTime start, DateTime end, String key, PageParameter param)
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
        /// <summary>类别名实体缓存，异步，缓存10分钟</summary>
        static FieldCache<Device> TypeCache = new FieldCache<Device>(_.Type);

        /// <summary>获取所有类别名称</summary>
        /// <returns></returns>
        public static IDictionary<String, String> FindAllTypeName()
        {
            return TypeCache.FindAllName();
        }
        #endregion

        #region 业务
        /// <summary>登录</summary>
        /// <param name="pass"></param>
        /// <param name="salt"></param>
        public void Login(String pass, Byte[] salt)
        {
            if (String.IsNullOrEmpty(pass)) throw new ArgumentNullException(nameof(pass));
            if (salt == null || salt.Length == 0) throw new ArgumentNullException(nameof(salt));

            if (!Enable) throw new EntityException("账号{0}被禁用！", Name);

            // 数据库为空密码，任何密码均可登录
            var buf = salt.RC4(Password.GetBytes());
            if (!pass.EqualIgnoreCase(buf.ToHex()))
            {
                var err = "密码不正确！";
                if (SysConfig.Current.Develop) err = "{0} DB:{1}!=Biz:{2}".F(err, Password, pass);
                throw new EntityException(err);
            }

            Logins++;
            LastLogin = DateTime.Now;

            Save();
        }

        /// <summary>注册网关设备，默认启用网关设备</summary>
        /// <param name="code"></param>
        /// <param name="pass"></param>
        public static void Register(String code, String pass)
        {
            if (String.IsNullOrEmpty(code)) throw new ArgumentNullException(nameof(code));
            if (String.IsNullOrEmpty(pass)) throw new ArgumentNullException(nameof(pass));

            var gw = FindByName(code);
            if (gw != null) throw new EntityException("{0}该设备已经注册", code);

            gw = new Device
            {
                Name = code,
                Password = pass,

                Enable = true,
                RegisterTime = DateTime.Now
            };

            gw.Save();
        }

        /// <summary>注册</summary>
        /// <param name="ip"></param>
        public void Register(String ip)
        {
            //if (Name == null || Password == null) return;

            Registers++;
            RegisterTime = DateTime.Now;
            RegisterIP = ip;
            Save();
        }
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