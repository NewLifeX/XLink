using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using NewLife.Log;
using NewLife.Model;
using NewLife.Reflection;
using NewLife.Threading;
using NewLife.Web;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;
using XCode.Membership;

namespace xLink.Entity
{
    /// <summary>子设备</summary>
    public partial class SubDevice : Entity<SubDevice>
    {
        #region 对象操作
        static SubDevice()
        {
            // 累加字段
            //var df = Meta.Factory.AdditionalFields;
            //df.Add(__.ProductId);

            // 过滤器 UserModule、TimeModule、IPModule
            Meta.Modules.Add<UserModule>();
            Meta.Modules.Add<TimeModule>();
            Meta.Modules.Add<IPModule>();
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew">是否插入</param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 在新插入数据或者修改了指定字段时进行修正
            // 处理当前已登录用户信息，可以由UserModule过滤器代劳
            /*var user = ManageProvider.User;
            if (user != null)
            {
                if (isNew && !Dirtys[nameof(CreateUserID)]) CreateUserID = user.ID;
                if (!Dirtys[nameof(UpdateUserID)]) UpdateUserID = user.ID;
            }*/
            //if (isNew && !Dirtys[nameof(CreateTime)]) CreateTime = DateTime.Now;
            //if (!Dirtys[nameof(UpdateTime)]) UpdateTime = DateTime.Now;
            //if (isNew && !Dirtys[nameof(CreateIP)]) CreateIP = ManageProvider.UserHost;
            //if (!Dirtys[nameof(UpdateIP)]) UpdateIP = ManageProvider.UserHost;

            // 检查唯一索引
            // CheckExist(isNew, __.Code);
        }
        #endregion

        #region 扩展属性
        /// <summary>产品</summary>
        [XmlIgnore, IgnoreDataMember]
        //[ScriptIgnore]
        public Product Product => Extends.Get(nameof(Product), k => Product.FindByID(ProductId));

        /// <summary>产品</summary>
        [Map(__.ProductId, typeof(Product), "ID")]
        public String ProductName => Product?.Name;
        /// <summary>设备</summary>
        [XmlIgnore, IgnoreDataMember]
        //[ScriptIgnore]
        public Device Device => Extends.Get(nameof(Device), k => Device.FindByID(DeviceId));

        /// <summary>设备</summary>
        [Map(__.DeviceId, typeof(Device), "ID")]
        public String DeviceName => Device?.Name;
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static SubDevice FindByID(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ID == id);

            // 单对象缓存
            return Meta.SingleCache[id];

            //return Find(_.ID == id);
        }

        /// <summary>根据编码查找</summary>
        /// <param name="code">编码</param>
        /// <returns>实体对象</returns>
        public static SubDevice FindByCode(String code)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Code == code);

            return Find(_.Code == code);
        }

        /// <summary>根据设备查找</summary>
        /// <param name="deviceid">设备</param>
        /// <returns>实体列表</returns>
        public static IList<SubDevice> FindAllByDeviceId(Int32 deviceid)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.DeviceId == deviceid);

            return FindAll(_.DeviceId == deviceid);
        }

        /// <summary>根据经销商、产品型号查找</summary>
        /// <param name="vendor">经销商</param>
        /// <param name="model">产品型号</param>
        /// <returns>实体列表</returns>
        public static IList<SubDevice> FindAllByVendorAndModel(String vendor, String model)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.Vendor == vendor && e.Model == model);

            return FindAll(_.Vendor == vendor & _.Model == model);
        }
        #endregion

        #region 高级查询
        /// <summary>查询子设备</summary>
        /// <param name="deviceId"></param>
        /// <param name="productId"></param>
        /// <param name="enable"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="key"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IList<SubDevice> Search(Int32 deviceId, Int32 productId, Boolean? enable, DateTime start, DateTime end, String key, PageParameter page)
        {
            var exp = new WhereExpression();

            if (deviceId >= 0) exp &= _.DeviceId == deviceId;
            if (productId >= 0) exp &= _.ProductId == productId;
            if (enable != null) exp &= _.Enable == enable;
            exp &= _.UpdateTime.Between(start, end);

            if (!key.IsNullOrEmpty()) exp &= _.Code == key | _.Name == key;

            return FindAll(exp, page);
        }
        #endregion

        #region 业务操作
        #endregion
    }
}