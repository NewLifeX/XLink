using NewLife.Cube;
using NewLife.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using WiFi.Entity;

namespace WiFi.Device.Web.Controllers
{
    [Description("设备操作历史")]
    public class DeviceHistoryController : ReadOnlyEntityController<DeviceHistory>
    {
        static DeviceHistoryController()
        {
            MenuOrder = 84;

            var list = ListFields;
            list.RemoveField("CreateDeviceID");
            list.AddField("Success", "Remark");
        }

        /// <summary>列表页视图。子控制器可重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override IEnumerable<DeviceHistory> Search(Pager p)
        {
            var deviceid = p["DeviceID"].ToInt(-1);
            var action = p["action"];
            var success = p["success"]?.ToBoolean();

            return DeviceHistory.Search(deviceid, action, success, p["dtStart"].ToDateTime(), p["dtEnd"].ToDateTime(), p["Q"], p);
        }
    }
}