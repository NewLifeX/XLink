using System;
using System.Collections.Generic;
using System.ComponentModel;
using NewLife.Cube;
using NewLife.Web;
using xLink.Entity;

namespace xLink.Web.Areas.Devices.Controllers
{
    [DevicesArea]
    [DisplayName("设备配置")]
    public class DeviceConfigController : EntityController<DeviceConfig>
    {
        static DeviceConfigController() => MenuOrder = 40;

        protected override IEnumerable<DeviceConfig> Search(Pager p)
        {
            var cityId = p["cityId"].ToInt(-1);
            var deviceId = p["deviceId"].ToInt(-1);
            var name = p["name"];

            var start = p["dtStart"].ToDateTime();
            var end = p["dtEnd"].ToDateTime();

            return DeviceConfig.Search(cityId, deviceId, name, start, end, p["Q"], p);
        }
    }
}