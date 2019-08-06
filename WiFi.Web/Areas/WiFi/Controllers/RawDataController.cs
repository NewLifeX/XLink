using NewLife.Cube;
using NewLife.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using WiFi.Entity;

namespace WiFi.Device.Web.Controllers
{
    [Description("原始数据")]
    public class RawDataController : EntityController<RawData>
    {
        static RawDataController()
        {
            MenuOrder = 82;
        }

        ///// <summary>列表页视图。子控制器可重载，以传递更多信息给视图，比如修改要显示的列</summary>
        ///// <param name="p"></param>
        ///// <returns></returns>
        //protected override IEnumerable<RawData> Search(Pager p)
        //{
        //    Boolean? flag = null;
        //    if (!p["finished"].IsNullOrEmpty()) flag = p["finished"].ToBoolean();

        //    return RawData.Search(p["deviceid"].ToInt(), p["cmd"], flag, p["dtStart"].ToDateTime(), p["dtEnd"].ToDateTime(), p["Q"], p);
        //}
    }
}