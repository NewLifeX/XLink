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
using System.Runtime.Serialization;
using System.Xml.Serialization;
using NewLife.Common;
using NewLife.Data;
using NewLife.Model;
using XCode;
using XCode.Cache;
using XCode.Membership;

namespace xLink.Entity
{
    /// <summary>设备</summary>
    public partial class Device : Entity<Device>
    {
        #region 对象操作
        static Device()
        {
            var df = Meta.Factory.AdditionalFields;
            df.Add(__.Logins);
            //df.Add(__.Registers);

            Meta.Modules.Add<UserModule>();
            Meta.Modules.Add<TimeModule>();
            Meta.Modules.Add<IPModule>();

            var sc = Meta.SingleCache;
            sc.FindSlaveKeyMethod = e => Find(__.Code, e);
            sc.GetSlaveKeyMethod = e => e.Code;
            //sc.SlaveKeyIgnoreCase = false;
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            if (Name.IsNullOrEmpty()) throw new ArgumentNullException(__.Name, _.Name.DisplayName + "不能为空！");

            var len = _.MACs.Length;
            if (MACs != null && len > 0 && MACs.Length > len) MACs = MACs.Substring(0, len);
            len = _.COMs.Length;
            if (COMs != null && len > 0 && COMs.Length > len) COMs = COMs.Substring(0, len);

            len = _.Uuid.Length;
            if (Uuid != null && len > 0 && Uuid.Length > len) Uuid = Uuid.Substring(0, len);

            len = _.MachineGuid.Length;
            if (MachineGuid != null && len > 0 && MachineGuid.Length > len) MachineGuid = MachineGuid.Substring(0, len);
        }

        /// <summary>已重载</summary>
        /// <returns></returns>
        public override String ToString() => Name ?? Code;
        #endregion

        #region 扩展属性
        /// <summary>产品</summary>
        [XmlIgnore, IgnoreDataMember]
        public Product Product => Extends.Get(nameof(Product), k => Product.FindByID(ProductId));

        /// <summary>产品名</summary>
        [Map(__.ProductId, typeof(Product), "ID")]
        public String ProductName => Product + "";

        /// <summary>省份</summary>
        [XmlIgnore, IgnoreDataMember]
        public Area Province => Extends.Get(nameof(Province), k => Area.FindByID(ProvinceId));

        /// <summary>省份名</summary>
        [Map(__.ProvinceId)]
        public String ProvinceName => Province + "";

        /// <summary>城市</summary>
        [XmlIgnore, IgnoreDataMember]
        public Area City => Extends.Get(nameof(City), k => Area.FindByID(CityId));

        /// <summary>城市名</summary>
        [Map(__.CityId)]
        public String CityName => City + "";

        /// <summary>最后地址。IP=>Address</summary>
        [DisplayName("最后地址")]
        public String LastLoginAddress => LastLoginIP.IPToAddress();
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public static Device FindByID(Int32 id)
        {
            if (id == 0) return null;

            if (Meta.Count < 1000) return Meta.Cache.Entities.FirstOrDefault(e => e.ID == id);

            // 单对象缓存
            return Meta.SingleCache[id];
        }

        /// <summary>根据名称。登录用户名查找</summary>
        /// <param name="name">名称。登录用户名</param>
        /// <returns></returns>
        public static Device FindByName(String name)
        {
            if (name.IsNullOrEmpty()) return null;

            if (Meta.Count < 1000) return Meta.Cache.Entities.FirstOrDefault(e => e.Name == name);

            return Find(__.Name, name);
        }

        /// <summary>根据名称查找</summary>
        /// <param name="code">名称</param>
        /// <param name="cache">是否走缓存</param>
        /// <returns>实体对象</returns>
        public static Device FindByCode(String code, Boolean cache = true)
        {
            if (code.IsNullOrEmpty()) return null;

            if (!cache) return Find(_.Code == code);

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Code == code);

            // 单对象缓存
            return Meta.SingleCache.GetItemWithSlaveKey(code) as Device;
        }

        /// <summary>根据唯一标识找设备</summary>
        /// <param name="Id"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static Device FindByUuid(String Id, Int32 productId)
        {
            if (Id.IsNullOrEmpty()) return null;

            return Find(_.Uuid == Id & _.ProductId == productId);
        }

        /// <summary>根据唯一标识找设备</summary>
        /// <param name="Id"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static Device FindByMachineGuid(String Id, Int32 productId)
        {
            if (Id.IsNullOrEmpty()) return null;

            return Find(_.MachineGuid == Id & _.ProductId == productId);
        }
        #endregion

        #region 高级查询
        /// <summary>根据唯一标识搜索，任意一个匹配即可</summary>
        /// <param name="uuid"></param>
        /// <param name="guid"></param>
        /// <param name="macs"></param>
        /// <returns></returns>
        public static IList<Device> Search(String uuid, String guid, String macs)
        {
            var exp = new WhereExpression();
            if (!uuid.IsNullOrEmpty()) exp &= _.Uuid == uuid;
            if (!guid.IsNullOrEmpty()) exp &= _.MachineGuid == guid;
            if (!macs.IsNullOrEmpty()) exp &= _.MACs == macs;

            if (exp.IsEmpty) return new List<Device>();

            return FindAll(exp);
        }

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

            if (productId >= 0) exp &= _.ProductId == productId;
            if (enable != null) exp &= _.Enable == enable.Value;

            exp &= _.UpdateTime.Between(start, end);

            return FindAll(exp, param);
        }

        /// <summary>高级查询</summary>
        /// <param name="productId">产品</param>
        /// <param name="provinceId">省份</param>
        /// <param name="cityId">城市</param>
        /// <param name="version">版本</param>
        /// <param name="enable"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="key"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IList<Device> Search(Int32 productId, Int32 provinceId, Int32 cityId, String version, Boolean? enable, DateTime start, DateTime end, String key, PageParameter page)
        {
            var exp = new WhereExpression();

            if (provinceId >= 0) exp &= _.ProvinceId == provinceId;
            if (cityId >= 0) exp &= _.CityId == cityId;
            if (!version.IsNullOrEmpty()) exp &= _.Version == version;
            if (enable != null) exp &= _.Enable == enable.Value;

            //exp &= _.CreateTime.Between(start, end);
            exp &= _.LastLogin.Between(start, end);

            if (!key.IsNullOrEmpty())
            {
                exp &= _.Name == key | _.Code == key | _.Version == key |
                    _.OS.StartsWith(key) | _.OSVersion.StartsWith(key) |
                    _.MachineName == key | _.UserName == key |
                    _.Uuid == key | _.MachineGuid == key | _.MACs.Contains(key) | _.DiskID.Contains(key) |
                    _.Remark.Contains(key);
            }

            return FindAll(exp, page);
        }

        internal static IList<Device> SearchByCreateDate(DateTime date)
        {
            // 先用带有索引的UpdateTime过滤一次
            return FindAll(_.UpdateTime >= date & _.CreateTime.Between(date, date));
        }

        internal static IDictionary<Int32, Int32> SearchGroupByCreateTime(DateTime start, DateTime end)
        {
            var exp = new WhereExpression();
            exp &= _.CreateTime.Between(start, end);
            var list = FindAll(exp.GroupBy(_.ProductId), null, _.ID.Count() & _.ProductId, 0, 0);
            return list.ToDictionary(e => e.ProductId, e => e.ID);
        }

        internal static IDictionary<Int32, Int32> SearchGroupByLastLogin(DateTime start, DateTime end)
        {
            var exp = new WhereExpression();
            exp &= _.LastLogin.Between(start, end);
            var list = FindAll(exp.GroupBy(_.ProductId), null, _.ID.Count() & _.ProductId, 0, 0);
            return list.ToDictionary(e => e.ProductId, e => e.ID);
        }

        internal static IDictionary<Int32, Int32> SearchCountByCreateDate(DateTime date)
        {
            var exp = new WhereExpression();
            exp &= _.CreateTime < date.AddDays(1);
            var list = FindAll(exp.GroupBy(_.ProductId), null, _.ID.Count() & _.ProductId, 0, 0);
            return list.ToDictionary(e => e.ProductId, e => e.ID);
        }
        #endregion

        #region 扩展操作
        /// <summary>类别名实体缓存，异步，缓存10分钟</summary>
        static Lazy<FieldCache<Device>> VersionCache = new Lazy<FieldCache<Device>>(() => new FieldCache<Device>(__.Version)
        {
            Where = _.UpdateTime > DateTime.Today.AddDays(-30) & Expression.Empty,
            MaxRows = 50
        });

        /// <summary>获取所有类别名称</summary>
        /// <returns></returns>
        public static IDictionary<String, String> FindAllVersion() => VersionCache.Value.FindAllName().OrderByDescending(e => e.Key).ToDictionary(e => e.Key, e => e.Value);
        #endregion

        #region 业务
        /// <summary>根据编码查询或添加</summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Device GetOrAdd(String code) => GetOrAdd(code, FindByCode, k => new Device { Code = k, Enable = true });
        #endregion
    }
}