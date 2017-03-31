using System;
using System.ComponentModel;
using System.Web.Mvc;
using NewLife.Cube;
using NewLife.Web;
using xLink.Master.Entity;

namespace xLink.Web.Controllers
{
    [Description("服务器操作历史")]
    public class ServerHistoryController : EntityController<ServerHistory>
    {
        static ServerHistoryController()
        {
            var list = ListFields;
            list.RemoveField("CreateServerID");
            list.AddField("Success", "Remark");
            list.RemoveField("Name");
        }

        /// <summary>列表页视图。子控制器可重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override ActionResult IndexView(Pager p)
        {
            var list = ServerHistory.Search(p["TokenID"].ToInt(-1), p["type"], p["action"], p["result"].ToInt(-1), p["dtStart"].ToDateTime(), p["dtEnd"].ToDateTime(), p["Q"], p);

            return View("List", list);
        }
    }
}