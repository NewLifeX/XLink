using System.ComponentModel;
using NewLife.Cube;
using xLink.Entity;

namespace xLink.Web.Areas.Devices.Controllers
{
    [DevicesArea]
    [DisplayName("产品版本")]
    public class ProductVersionController : EntityController<ProductVersion>
    {
        static ProductVersionController() => MenuOrder = 89;
    }
}