using System;
using System.Collections.Generic;
using System.ComponentModel;
using NewLife;
using NewLife.Cube;
using NewLife.Web;
using xLink.Entity;

namespace xLink.Device.Web.Controllers
{
    [Description("设备命令")]
    public class DeviceCommandController : EntityController<DeviceCommand>
    {
        static DeviceCommandController()
        {
            MenuOrder = 50;
        }

        /// <summary>列表页视图。子控制器可重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override IEnumerable<DeviceCommand> Search(Pager p)
        {
            Boolean? flag = null;
            if (!p["finished"].IsNullOrEmpty()) flag = p["finished"].ToBoolean();

            return DeviceCommand.Search(p["deviceid"].ToInt(), p["cmd"], flag, p["dtStart"].ToDateTime(), p["dtEnd"].ToDateTime(), p["Q"], p);
        }
    }
}