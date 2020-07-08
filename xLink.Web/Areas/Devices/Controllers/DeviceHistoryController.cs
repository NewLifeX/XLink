using System;
using System.Collections.Generic;
using System.ComponentModel;
using NewLife.Cube;
using NewLife.Web;
using xLink.Entity;

namespace xLink.Web.Areas.Devices.Controllers
{
    [DevicesArea]
    [DisplayName("设备历史")]
    public class DeviceHistoryController : EntityController<DeviceHistory>
    {
        static DeviceHistoryController() => MenuOrder = 60;

        protected override IEnumerable<DeviceHistory> Search(Pager p)
        {
            var rids = p["areaId"].SplitAsInt("/");
            var cityId = rids.Length > 1 ? rids[1] : -1;

            var deviceId = p["deviceId"].ToInt(-1);
            var action = p["action"];
            //var success = p["success"]?.ToBoolean();
            Boolean? success = null;
            if (!p["success"].IsNullOrEmpty()) success = p["success"].ToBoolean();

            var start = p["dtStart"].ToDateTime();
            var end = p["dtEnd"].ToDateTime();

            return DeviceHistory.Search(cityId, deviceId, action, success, start, end, p["Q"], p);
        }
    }
}