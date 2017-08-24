using System;
using System.Collections.Generic;
using System.ComponentModel;
using NewLife.Cube;
using NewLife.Web;
using xLink.Entity;

namespace xLink.User.Web.Controllers
{
    [Description("用户登录后产生在线记录，一般长期在线")]
    public class UserOnlineController : EntityController<UserOnline>
    {
        static UserOnlineController()
        {
            ListFields.RemoveField("LastError");
        }

        /// <summary>列表页视图。子控制器可重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override IEnumerable<UserOnline> Search(Pager p)
        {
            return UserOnline.Search(p["Type"], p["dtStart"].ToDateTime(), p["dtEnd"].ToDateTime(), p["Q"], p);
        }
    }
}