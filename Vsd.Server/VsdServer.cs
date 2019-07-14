using NewLife.Net;
using NewLife.Serialization;
using System;
using System.Collections.Generic;

namespace Vsd.Server
{
    class VsdServer : NetServer<VsdSession>
    {
    }

    class VsdSession : NetSession
    {
        #region 主循环
        protected override void OnReceive(ReceivedEventArgs e)
        {
            //base.OnReceive(e);

            var str = e.Packet.ToStr();
            if (str.IsNullOrEmpty()) return;

            var dic = new JsonParser(str).Decode() as IDictionary<String, Object>;
            if (dic == null || dic.Count == 0) return;

            object result = null;
            var cmd = dic["cmd"] + "";
            switch (cmd)
            {
                case "dHeartbeat":
                    result = Heartbeat(cmd, dic);
                    break;
                case "dRecord":
                    result = UploadRecord(cmd, dic);
                    break;
            }

            // 处理结果，做出响应
            if (result != null) Send(result.ToJson().GetBytes());
        }
        #endregion

        #region 心跳
        public virtual Object Heartbeat(String cmd, IDictionary<String, Object> parameters)
        {
            var ps = parameters;
            var code = ps["snr"] + "";
            var ip = ps["ip"] + "";
            var name = ps["name"] + "";

            return new
            {
                cmd,
                snr = code,
                ip,
                name,
                time = DateTime.Now.ToFullString(),
                heartInterval = 60,
                keepAliveTime = 10,
                loraID = 0,
                resetTime = TimeSpan.FromDays(1).Subtract(TimeSpan.FromSeconds(1)).ToString(),
                terminalMode = 1,
                terminalBaud = 115200,
                terminalParity = "none",
                terminalStopbit = 1,
            };
        }
        #endregion

        #region 下发数据
        public virtual void DownloadData()
        {

        }
        #endregion

        #region 下发脚本
        public virtual void DownloadScript()
        {

        }
        #endregion

        #region 上传记录
        public virtual Object UploadRecord(String cmd, IDictionary<String, Object> parameters)
        {
            var ps = parameters;
            var code = ps["snr"] + "";
            var ip = ps["ip"] + "";
            var seq = ps["seq"].ToInt();

            // 记录集合
            var records = ps["records"] as IList<Object>;
            if (records != null && records.Count > 0)
            {
                foreach (var item in records)
                {
                    // 解析记录，数据是base64编码
                    var time = ps["date"].ToDateTime();
                    var data = (ps["record"] + "").ToBase64();
                    var s = ps["seq"].ToInt();
                    var name = ps["name"] + "";
                }
            }

            return new
            {
                cmd,
                snr = code,
                ip,
                seq,
                rsq = "pk",
            };
        }
        #endregion
    }
}