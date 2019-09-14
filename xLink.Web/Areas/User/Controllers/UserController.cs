using System;
using System.Collections.Generic;
using System.ComponentModel;
using NewLife.Cube;
using NewLife.Web;
using UserX = xLink.Entity.User;

namespace xLink.User.Web.Controllers
{
    [Description("用户信息，提供身份验证")]
    public class UserController : EntityController<UserX>
    {
        static UserController()
        {
            MenuOrder = 90;
        }

        /// <summary>列表页视图。子控制器可重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override IEnumerable<UserX> Search(Pager p)
        {
            Boolean? flag = null;
            if (!p["enable"].IsNullOrEmpty()) flag = p["enable"].ToBoolean();

            return UserX.Search(p["type"], flag, p["dtStart"].ToDateTime(), p["dtEnd"].ToDateTime(), p["Q"], p);
        }
    }
}