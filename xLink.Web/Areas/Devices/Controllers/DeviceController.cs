using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewLife.Cube;
using NewLife.Web;
using xLink.Entity;

namespace xLink.Web.Areas.Devices.Controllers
{
    [DevicesArea]
    [DisplayName("设备")]
    public class DeviceController : EntityController<Device>
    {
        static DeviceController() => MenuOrder = 80;

        protected override IEnumerable<Device> Search(Pager p)
        {
            var deviceId = p["Id"].ToInt(-1);
            if (deviceId > 0)
            {
                var dv = Device.FindByID(deviceId);
                if (dv != null) return new[] { dv };
            }

            var productId = p["productId"].ToInt(-1);
            var rids = p["areaId"].SplitAsInt("/");
            var provinceId = rids.Length > 0 ? rids[0] : -1;
            var cityId = rids.Length > 1 ? rids[1] : -1;

            var version = p["version"];
            var enable = p["enable"]?.ToBoolean();

            var start = p["dtStart"].ToDateTime();
            var end = p["dtEnd"].ToDateTime();

            return Device.Search(productId, provinceId, cityId, version, enable, start, end, p["Q"], p);
        }

        public ActionResult Trace(Int32 id)
        {
            var dv = Device.FindByID(id);
            if (dv != null)
            {
                DeviceCommand.Add(dv, "截屏");
                DeviceCommand.Add(dv, "抓日志");
            }

            // 跳转到来源页
            var url = Request.Headers["Referer"].FirstOrDefault() + "";
            if (url != null) return Redirect(url);

            return RedirectToAction("Index");
        }
    }
}