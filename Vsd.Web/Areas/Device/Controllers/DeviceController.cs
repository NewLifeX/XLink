using NewLife.Cube;
using NewLife.Web;
using System;
using System.Collections.Generic;
using DeviceX = Vsd.Entity.Device;

namespace Vsd.Device.Web.Controllers
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
            Boolean? flag = null;
            if (!p["enable"].IsNullOrEmpty()) flag = p["enable"].ToBoolean();

            return DeviceX.Search(p["type"], flag, p["dtStart"].ToDateTime(), p["dtEnd"].ToDateTime(), p["Q"], p);
        }
    }
}