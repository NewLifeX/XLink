using System.ComponentModel;
using NewLife.Cube;
using xLink.Entity;

namespace xLink.Web.Areas.Devices.Controllers
{
    [DevicesArea]
    [DisplayName("产品")]
    public class ProductController : EntityController<Product>
    {
        static ProductController() => MenuOrder = 90;
    }
}