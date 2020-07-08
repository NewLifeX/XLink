using System;
using System.Collections.Generic;
using System.ComponentModel;
using NewLife.Cube;
using NewLife.Web;
using xLink.Entity;

namespace xLink.Web.Areas.Devices.Controllers
{
    [DevicesArea]
    [DisplayName("子设备")]
    public class SubDeviceController : EntityController<SubDevice>
    {
        static SubDeviceController() => MenuOrder = 58;

        protected override IEnumerable<SubDevice> Search(Pager p)
        {
            var deviceId = p["deviceId"].ToInt(-1);
            var productId = p["productId"].ToInt(-1);

            var enable = p["enable"]?.ToBoolean();

            var start = p["dtStart"].ToDateTime();
            var end = p["dtEnd"].ToDateTime();

            return SubDevice.Search(deviceId, productId, enable, start, end, p["Q"], p);
        }
    }
}