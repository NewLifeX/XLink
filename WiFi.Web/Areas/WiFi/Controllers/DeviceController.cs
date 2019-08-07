using NewLife.Cube;
using NewLife.Web;
using System;
using System.Collections.Generic;
using WiFi.Entity;
using DeviceX = WiFi.Entity.Device;

namespace WiFi.Device.Web.Controllers
{
    public class DeviceController : EntityController<DeviceX>
    {
        static DeviceController()
        {
            MenuOrder = 90;

            var list = ListFields;
            list.RemoveField("Secret");
            list.RemoveField("Data");
            list.RemoveField("CreateUserID", "CreateIP", "UpdateUserID");
        }

        /// <summary>列表页视图。子控制器可重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override IEnumerable<DeviceX> Search(Pager p)
        {
            var kind = (DeviceKinds)p["kind"].ToInt();
            var enable = p["enable"]?.ToBoolean();

            return DeviceX.Search(kind, enable, p["dtStart"].ToDateTime(), p["dtEnd"].ToDateTime(), p["Q"], p);
        }
    }
}