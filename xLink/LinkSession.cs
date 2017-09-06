using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewLife.Model;
using NewLife.Remoting;
using XCode.Remoting;
using xLink.Entity;
using xLink.Models;

namespace xLink
{
    /// <summary>物联会话</summary>
    [Api(null)]
    public abstract class LinkSession : ApiUserSession
    {
        #region 登录注册
        /// <summary>注册，登录找不到用户时调用注册，返回空表示禁止注册</summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        protected override IManageUser Register(String user, String pass)
        {
            var u = CreateUser(user, pass);

            u.Enable = true;
            u.Registers++;

            Name = u.Name;
            WriteLog("注册 {0} => {1}/{2}", user, u.Name, u.Password);

            return u;
        }

        /// <summary>创建用户</summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        protected abstract IMyModel CreateUser(String user, String pass);

        /// <summary>登录或注册完成后，保存登录信息</summary>
        /// <param name="user"></param>
        protected override void SaveLogin(IManageUser user)
        {
            var u = user as IMyModel;
            u.Type = Type;
            u.Version = Version;
            if (u.NickName.IsNullOrEmpty()) u.NickName = "{0}{1}".F(Agent, user.Name);

            var dic = ControllerContext.Current?.Parameters?.ToNullable();
            if (dic != null)
            {
                var olt = Online as IMyOnline;
                olt.LoginTime = DateTime.Now;
                olt.LoginCount++;
                // 本地地址
                olt.InternalUri = dic["ip"] + "";
            }

            base.SaveLogin(user);
        }

        /// <summary>心跳</summary>
        /// <returns></returns>
        protected override Object OnPing()
        {
            var olt = Online as IMyOnline;
            olt.PingCount++;

            return base.OnPing();
        }
        #endregion

        #region 读写
        /// <summary>写入数据，返回整个数据区</summary>
        /// <param name="id">设备</param>
        /// <param name="start"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual async Task<Byte[]> Write(String id, Int32 start, params Byte[] data)
        {
            var rs = await InvokeAsync<DataModel>("Write", new { id, start, data = data.ToHex() });
            return rs.Data.ToHex();
        }

        /// <summary>读取对方数据</summary>
        /// <param name="id">设备</param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual async Task<Byte[]> Read(String id, Int32 start, Int32 count)
        {
            var rs = await InvokeAsync<DataModel>("Read", new { id, start, count });
            return rs.Data.ToHex();
        }

        public Func<String, Byte[]> GetData;
        public Action<String, Byte[]> SetData;

        /// <summary>收到写入请求</summary>
        /// <param name="id">设备</param>
        /// <param name="start"></param>
        /// <param name="data"></param>
        [Api("Write")]
        protected virtual DataModel OnWrite(String id, Int32 start, String data)
        {
            var buf = GetData?.Invoke(id);
            if (buf == null) throw new ApiException(405, "找不到设备！");

            var ds = data.ToHex();

            // 检查扩容
            if (start + ds.Length > buf.Length)
            {
                var buf2 = new Byte[start + ds.Length];
                buf2.Write(0, buf);
                buf = buf2;
            }
            buf.Write(start, ds);
            buf[0] = (Byte)buf.Length;

            // 保存回去
            SetData?.Invoke(id, buf);

            return new DataModel { ID = id, Start = 0, Data = buf.ToHex() };
        }

        /// <summary>收到读取请求</summary>
        /// <param name="id">设备</param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        [Api("Read")]
        protected virtual DataModel OnRead(String id, Int32 start, Int32 count)
        {
            var buf = GetData?.Invoke(id);
            if (buf == null) throw new ApiException(405, "找不到设备！");

            return new DataModel { ID = id, Start = start, Data = buf.ReadBytes(start, count).ToHex() };
        }
        #endregion
    }
}