using NewLife.Cube;
using NewLife.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using Vsd.Entity;
using XCode.Membership;

namespace Vsd.Device.Web.Controllers
{
    [Description("设备命令")]
    public class DeviceCommandController : EntityController<DeviceCommand>
    {
        static DeviceCommandController()
        {
            MenuOrder = 82;
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

        /// <summary>启用禁用任务</summary>
        /// <param name="id"></param>
        /// <param name="enable"></param>
        /// <returns></returns>
        [EntityAuthorize(PermissionFlags.Update)]
        public ActionResult Set(Int32 id = 0, Boolean enable = true)
        {
            if (id > 0)
            {
                var dt = DeviceCommand.FindByID(id);
                if (dt == null) throw new ArgumentNullException(nameof(id), "找不到命令 " + id);

                dt.Status = enable ? CommandStatus.就绪 : CommandStatus.取消;
                dt.Save();
            }
            else
            {
                var ids = Request["keys"].SplitAsInt(",");

                foreach (var item in ids)
                {
                    var dt = DeviceCommand.FindByID(item);
                    if (dt != null)
                    {
                        dt.Status = enable ? CommandStatus.就绪 : CommandStatus.取消;
                        dt.Save();
                    }
                }
            }
            return JsonRefresh("操作成功！");
        }
    }
}