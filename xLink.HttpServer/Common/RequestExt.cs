using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xLinkServer.Common
{
    public static class RequestExt
    {

        public static Uri GetRawUrl(this HttpRequest request)
        {
            string encodedUrl = request.GetEncodedUrl();
            return new Uri(encodedUrl);
        }
    }
}
