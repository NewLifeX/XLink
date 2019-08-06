using NewLife.Cube;
using NewLife.Web;
using System;
using System.Collections.Generic;
using WiFi.Entity;

namespace WiFi.Device.Web.Controllers
{
    public class ProductController : EntityController<Product>
    {
        static ProductController()
        {
            MenuOrder = 100;

            var list = ListFields;
            list.RemoveField("Secret");
        }

        /// <summary>列表页视图。子控制器可重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override IEnumerable<Product> Search(Pager p)
        {
            var start = p["dtStart"].ToDateTime();
            var end = p["dtEnd"].ToDateTime();
            Boolean? flag = null;
            if (!p["enable"].IsNullOrEmpty()) flag = p["enable"].ToBoolean();

            return Product.Search(flag, start, end, p["Q"], p);
        }
    }
}