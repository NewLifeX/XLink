using NewLife.Cube;
using NewLife.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using xLink.Entity;

namespace xLink.Device.Web.Controllers
{
    public class ProductController : EntityController<Product>
    {
        static ProductController()
        {
            MenuOrder = 90;

            var list = ListFields;
            list.RemoveField("Secret");
        }

        /// <summary>列表页视图。子控制器可重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override IEnumerable<Product> Search(Pager p)
        {
            var flag = p["enable"]?.ToBoolean();

            return Product.Search(flag, p["dtStart"].ToDateTime(), p["dtEnd"].ToDateTime(), p["Q"], p);
        }
    }
}