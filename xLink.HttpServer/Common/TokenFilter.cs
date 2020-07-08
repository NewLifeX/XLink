using Microsoft.AspNetCore.Mvc.Filters;
using NewLife.Log;
using NewLife.Remoting;
using System;
using xLinkServer.Controllers;

namespace xLinkServer.Common
{
    /// <summary>令牌校验</summary>
    public class TokenFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is BaseController bc)
            {
                var path = getPath(context);
                var session = bc.Session;
                if (bc.Token.IsNullOrEmpty()) throw new ApiException(403, "未授权");
                if (session == null)
                {
                    XTrace.WriteLine($"令牌无效：{bc.Token} {path}");
                    throw new ApiException(402, "令牌无效");
                }

                if (context.Controller is DeviceController)
                {
                    if (session["Device"] == null)
                    {
                        XTrace.WriteLine($"设备未登录：{bc.Token} {path}");
                        throw new ApiException(500, "设备未登录");
                    }
                }
                else
                {
                    if (session["User"] == null)
                    {

                        XTrace.WriteLine($"用户未登录：{bc.Token} {path}");
                        throw new ApiException(500, "用户未登录");
                    }
                }
            }

            base.OnActionExecuting(context);
        }

        private static String getPath(ActionExecutingContext context)
        {
            try
            {
                var path = context.HttpContext.Request.Path;
                if (path.HasValue) return path.Value;
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex);
            }
            return "";
        }
    }
}