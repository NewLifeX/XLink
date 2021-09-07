using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewLife;

namespace xLinkServer.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class TokenService
    {
        /// <summary>
        /// item1=设备code
        /// item2=用户Code
        /// Token无效返回null
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Tuple<String, String> ParseToken(String token)
        {
            var str = token.Trim().Substring(null, ".");
            if (str.IsNullOrEmpty()) return null;
            str = str.ToBase64().ToStr();
            if (str.IsNullOrEmpty()) return null;

            var rlist = str.Split('#', ',');
            var deviceCode = "";
            var userCode = "";
            if (rlist.Length > 0)
            {
                deviceCode = rlist[0];
            }

            if (rlist.Length > 1)
            {
                userCode = rlist[1];
            }

            return Tuple.Create(deviceCode, userCode);
        }
    }
}
