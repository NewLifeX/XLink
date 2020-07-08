using NewLife.Cube;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace xLink.Web.Areas.Devices
{
    [DisplayName("设备管理")]
    public class DevicesArea : AreaBase
    {
        public DevicesArea() : base(nameof(DevicesArea).TrimEnd("Area")) { }

        static DevicesArea() => RegisterArea<DevicesArea>();
    }
}
