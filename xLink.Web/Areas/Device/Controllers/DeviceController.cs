using System;
using System.ComponentModel;
using System.Web.Mvc;
using NewLife.Cube;
using NewLife.Web;
using DeviceX = xLink.Device.Entity.Device;

namespace xLink.Device.Web.Controllers
{
    [Description("设备信息，所有网关设备激活后都记录在此，并提供身份验证")]
    public class DeviceController : EntityController<DeviceX>
    {

        static DeviceController()
        {
            var list = ListFields;
            list.RemoveField("Code")
                .RemoveField("LastLoginIP")
                .RemoveField("RegisterIP");
        }

        /// <summary>列表页视图。子控制器可重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override ActionResult IndexView(Pager p)
        {
            Boolean? flag = null;
            if (!p["enable"].IsNullOrEmpty()) flag = p["enable"].ToBoolean();

            var list = DeviceX.Search(p["type"], flag, p["dtStart"].ToDateTime(), p["dtEnd"].ToDateTime(), p["Q"], p);

            return View("List", list);
        }
    }
}