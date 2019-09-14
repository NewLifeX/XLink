using System;
using System.Collections.Generic;
using System.ComponentModel;
using NewLife.Cube;
using NewLife.Web;
using xLink.Entity;

namespace xLink.Device.Web.Controllers
{
    [Description("设备操作历史")]
    public class DeviceHistoryController : EntityController<DeviceHistory>
    {
        static DeviceHistoryController()
        {
            MenuOrder = 60;

            var list = ListFields;
            list.RemoveField("CreateDeviceID");
            list.AddField("Success", "Remark");
        }

        /// <summary>列表页视图。子控制器可重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override IEnumerable<DeviceHistory> Search(Pager p)
        {
            var deviceId = p["deviceId"].ToInt(-1);

            return DeviceHistory.Search(deviceId, p["action"], p["result"].ToInt(-1), p["dtStart"].ToDateTime(), p["dtEnd"].ToDateTime(), p["Q"], p);
        }
    }
}