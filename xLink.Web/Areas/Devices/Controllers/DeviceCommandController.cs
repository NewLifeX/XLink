using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using NewLife.Cube;
using NewLife.Web;
using XCode.Membership;
using xLink.Entity;

namespace xLink.Web.Areas.Devices.Controllers
{
    [DevicesArea]
    [DisplayName("设备命令")]
    public class DeviceCommandController : EntityController<DeviceCommand>
    {
        static DeviceCommandController() => MenuOrder = 58;

        protected override IEnumerable<DeviceCommand> Search(Pager p)
        {
            var cityId = p["cityId"].ToInt(-1);
            var deviceId = p["deviceId"].ToInt(-1);
            var command = p["command"];

            var start = p["dtStart"].ToDateTime();
            var end = p["dtEnd"].ToDateTime();

            return DeviceCommand.Search(cityId, deviceId, command, start, end, p["Q"], p);
        }

        [EntityAuthorize(PermissionFlags.Update)]
        public ActionResult Download(Int32 Id)
        {
            var device = DeviceCommand.FindByID(Id);
            if (device == null) throw new Exception("找不到ID！");

            var tmpPath = (device.Result).GetFullPath();
            var fi = new FileInfo(tmpPath);

            if (!fi.Exists) return JsonRefresh("下载文件不存在！");

            var bytes = new byte[fi.Length];
            using (var fs = fi.OpenRead())
            {
                fs.Read(bytes, 0, bytes.Length);
            }

            return File(bytes, "application/octet-stream", fi.Name);
            //System.IO.File.Copy(tmpPath, tmpPath, true);
        }
    }
}