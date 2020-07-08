using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using NewLife.Cube;
using NewLife.Web;
using XCode.Membership;
using xLink.Entity;

namespace xLink.Web.Areas.Devices.Controllers
{
    [DevicesArea]
    [DisplayName("设备在线")]
    public class DeviceOnlineController : EntityController<DeviceOnline>
    {
        static DeviceOnlineController() => MenuOrder = 70;

        protected override IEnumerable<DeviceOnline> Search(Pager p)
        {
            var productId = p["productId"].ToInt(-1);
            var deviceId = p["deviceId"].ToInt(-1);
            var rids = p["areaId"].SplitAsInt("/");
            var cityId = rids.Length > 1 ? rids[1] : -1;

            var start = p["dtStart"].ToDateTime();
            var end = p["dtEnd"].ToDateTime();

            return DeviceOnline.Search(productId, deviceId, cityId, start, end, p["Q"], p);
        }

        /// <summary>批量跟踪</summary>
        /// <returns></returns>
        [EntityAuthorize(PermissionFlags.Update)]
        public ActionResult Trace()
        {
            var ids = GetRequest("keys").SplitAsInt();
            foreach (var item in ids)
            {
                var dv = DeviceOnline.FindByID(item)?.Device;
                if (dv != null)
                {
                    DeviceCommand.Add(dv, "截屏");
                    DeviceCommand.Add(dv, "抓日志");
                }
            }

            return JsonRefresh("操作成功！");
        }
    }
}