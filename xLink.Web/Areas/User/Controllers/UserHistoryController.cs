using System;
using System.Collections.Generic;
using System.ComponentModel;
using NewLife.Cube;
using NewLife.Web;
using xLink.Entity;

namespace xLink.User.Web.Controllers
{
    [Description("用户操作历史")]
    public class UserHistoryController : EntityController<UserHistory>
    {
        static UserHistoryController()
        {
            var list = ListFields;
            list.RemoveField("CreateUserID");
            list.AddField("Success", "Remark");
        }

        /// <summary>列表页视图。子控制器可重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override IEnumerable<UserHistory> Search(Pager p)
        {
            return UserHistory.Search(p["TokenID"].ToInt(-1), p["type"], p["action"], p["result"].ToInt(-1), p["dtStart"].ToDateTime(), p["dtEnd"].ToDateTime(), p["Q"], p);
        }
    }
}