using NewLife;
using NewLife.Model;
using NewLife.Net;
using NewLife.Threading;
using System;
using System.Collections.Generic;
using xLink.Entity;
using xLink.Models;

namespace xLink.Services
{
    /// <summary>用户服务</summary>
    public class UserService : LinkService
    {
        #region 属性
        #endregion

        #region 构造
        static UserService()
        {
            // 异步初始化数据
            ThreadPoolX.QueueUserWorkItem(() =>
            {
                var n = 0;
                n = User.Meta.Count;
                n = UserOnline.Meta.Count;
                n = UserHistory.Meta.Count;
            });
        }

        /// <summary>实例化</summary>
        public UserService()
        {
        }
        #endregion

        #region 登录
        /// <summary>查找用户并登录，找不到用户是返回空，登录失败则抛出异常</summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="ps">附加参数</param>
        /// <returns></returns>
        protected override IManageUser CheckUser(String user, String pass, IDictionary<String, Object> ps)
        {
            var u = User.FindByName(user);
            if (u == null)
            {
                var ns = Session as INetSession;
                u = new User
                {
                    Name = user,
                    Password = pass.MD5(),
                    Enable = true,

                    CreateIP = ns?.Remote.Address + "",
                    CreateTime = DateTime.Now,
                };
                //u.SaveRegister(Session as INetSession);

                u.Insert();

                return u;
            }

            // 验证密码
            //u.CheckMD5(pass);
            if (u.Password != pass.MD5()) throw new XException("密码错误！");

            return u;
        }
        #endregion

        #region 心跳历史
        /// <summary>更新在线信息，登录前、心跳时 调用</summary>
        /// <param name="name"></param>
        /// <param name="ps">附加参数</param>
        protected override void CheckOnline(String name, IDictionary<String, Object> ps)
        {
            var ns = Session as NetSession;
            var sid = ns.Remote.EndPoint + "";

            var olt = Online ?? CreateOnline(sid);
            //if (olt is UserOnline dolt) Fill(dolt, ps);

            Online = olt;

            base.CheckOnline(name, ps);
        }

        /// <summary>创建在线</summary>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        protected override IOnline CreateOnline(String sessionid)
        {
            var ns = Session as NetSession;
            var sid = ns.Remote.EndPoint + "";

            var olt = UserOnline.GetOrAdd(sid);
            olt.ExternalUri = ns.Remote + "";

            if (Current is User user)
            {
                //olt.DeviceID = user.ID;
                //olt.ProductID = user.ProductID;
                olt.Name = user.Name;

                olt.Version = user.Version;
                //olt.CompileTime = user.CompileTime;
                //olt.Memory = user.Memory;
                //olt.MACs = user.MACs;
                //olt.COMs = user.COMs;

                olt.CreateIP = ns.Remote.Address + "";
            }

            olt.SaveAsync();

            return olt;
        }

        /// <summary>保存操作历史</summary>
        /// <param name="action"></param>
        /// <param name="success"></param>
        /// <param name="content"></param>
        protected override void SaveHistory(String action, Boolean success, String content)
        {
            var hi = new UserHistory();

            if (Current is User user)
            {
                if (hi.UserID == 0) hi.UserID = user.ID;
                if (hi.Name.IsNullOrEmpty()) hi.Name = user + "";

                hi.Version = user.Version;
                //hi.CompileTime = user.CompileTime;
            }
            else if (Online is UserOnline olt)
            {
                if (hi.UserID == 0) hi.UserID = olt.UserID;
                if (hi.Name.IsNullOrEmpty()) hi.Name = olt.Name;
            }

            hi.Action = action;
            hi.Success = success;
            hi.Remark = content;
            hi.CreateTime = DateTime.Now;

            if (Session is INetSession ns) hi.CreateIP = ns.Remote + "";

            hi.SaveAsync();
        }
        #endregion

        #region 清理超时
        /// <summary>清理超时会话</summary>
        /// <param name="secTimeout"></param>
        /// <returns></returns>
        public override Int32 ClearExpire(Int32 secTimeout) => UserOnline.ClearExpire(secTimeout).Count;
        #endregion
    }
}