using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLife.Remoting;

namespace xLink.Device
{
    /// <summary>用户会话</summary>
    [Api("User")]
    [DisplayName("用户")]
    public class UserSession : LinkSession
    {
    }
}