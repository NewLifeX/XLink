using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewLife.Log;
using NewLife.Net;
using NewLife.Reflection;
using NewLife.Remoting;
using NewLife.Threading;

namespace xLink.Client
{
    public partial class FrmMain : Form
    {
        LinkClient _Client;
        IPacket _Packet;

        #region 窗体
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(Object sender, EventArgs e)
        {
            txtReceive.UseWinFormControl();

            var asmx = AssemblyX.Entry;
            Text += " " + asmx.Compile.ToFullString();

            txtReceive.SetDefaultStyle(12);
            txtSend.SetDefaultStyle(12);
            numMutilSend.SetDefaultStyle(12);

            gbReceive.Tag = gbReceive.Text;
            gbSend.Tag = gbSend.Text;

            var cfg = Setting.Current;
            cbMode.SelectedItem = cfg.Mode;

            if (cfg.IsNew) "欢迎使用物联网客户端！".SpeechTip();

            // 加载保存的颜色
            UIConfig.Apply(txtReceive);

            LoadConfig();
        }
        #endregion

        #region 加载/保存 配置
        void LoadConfig()
        {
            var cfg = Setting.Current;
            mi显示应用日志.Checked = cfg.ShowLog;
            mi显示编码日志.Checked = cfg.ShowEncoderLog;
            mi显示接收字符串.Checked = cfg.ShowReceiveString;
            mi显示发送数据.Checked = cfg.ShowSend;
            mi显示接收数据.Checked = cfg.ShowReceive;
            mi显示统计信息.Checked = cfg.ShowStat;
            miHexSend.Checked = cfg.HexSend;
            mi日志着色.Checked = cfg.ColorLog;

            txtSend.Text = cfg.SendContent;
            numMutilSend.Value = cfg.SendTimes;
            numSleep.Value = cfg.SendSleep;
            numThreads.Value = cfg.SendUsers;

            cbAddr.DataSource = cfg.GetAddresss();
        }

        void SaveConfig()
        {
            var cfg = Setting.Current;
            cfg.ShowLog = mi显示应用日志.Checked;
            cfg.ShowEncoderLog = mi显示编码日志.Checked;
            cfg.ShowReceiveString = mi显示接收字符串.Checked;
            cfg.ShowSend = mi显示发送数据.Checked;
            cfg.ShowReceive = mi显示接收数据.Checked;
            cfg.ShowStat = mi显示统计信息.Checked;
            cfg.HexSend = miHexSend.Checked;

            cfg.SendContent = txtSend.Text;
            cfg.SendTimes = (Int32)numMutilSend.Value;
            cfg.SendSleep = (Int32)numSleep.Value;
            cfg.SendUsers = (Int32)numThreads.Value;
            cfg.ColorLog = mi日志着色.Checked;

            cfg.Mode = cbMode.Text;
            cfg.AddAddresss(cbAddr.Text);

            cfg.Save();
        }
        #endregion

        #region 收发数据
        void Connect()
        {
            _Client = null;

            var uri = new NetUri(cbAddr.Text);
            _Packet = new DefaultPacket();
            var cfg = Setting.Current;
            var mode = cbMode.Text;

            var ac = new LinkClient(uri.ToString())
            {
                Log = cfg.ShowLog ? XTrace.Log : Logger.Null,
                EncoderLog = cfg.ShowEncoderLog ? XTrace.Log : Logger.Null
            };
            ac.Received += OnReceived;
            ac.UserName = cfg.UserName;
            ac.Password = cfg.Password;
            ac.LoginAction = mode + "/Login";
            ac.PingAction = mode + "/Ping";

            ac.Encrypted = cfg.Encrypted;
            ac.Compressed = cfg.Compressed;

            if (!ac.Open()) return;

            var sc = ac.Client.GetService<ISocketClient>();
            sc.Log = cfg.ShowLog ? XTrace.Log : Logger.Null;
            sc.LogSend = cfg.ShowSend;
            sc.LogReceive = cfg.ShowReceive;

            "已连接服务器".SpeechTip();

            _Client = ac;

            pnlSetting.Enabled = false;
            pnlAction.Enabled = true;
            btnConnect.Text = "关闭";

            // 添加地址
            cfg.AddAddresss(uri.ToString());

            cfg.Save();

            _timer = new TimerX(ShowStat, null, 5000, 5000);

            BizLog = TextFileLog.Create("DeviceLog");
        }

        void Disconnect()
        {
            if (_Client != null)
            {
                _Client.Dispose();
                _Client = null;

                "关闭连接".SpeechTip();
            }
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }

            pnlSetting.Enabled = true;
            pnlAction.Enabled = false;
            btnConnect.Text = "打开";

            LoadConfig();
        }

        TimerX _timer;
        String _lastStat;
        void ShowStat(Object state)
        {
            if (!Setting.Current.ShowStat) return;

            var msg = "";
            if (_Client != null)
            {
                var sc = _Client.Client.GetService<ISocketClient>();
                msg = sc.GetStat();

                msg += " 并发 " + cs.Count;
            }

            if (!msg.IsNullOrEmpty() && msg != _lastStat)
            {
                _lastStat = msg;
                XTrace.WriteLine(msg);
            }
        }

        private void btnConnect_Click(Object sender, EventArgs e)
        {
            SaveConfig();

            var btn = sender as Button;
            if (btn.Text == "打开")
                Connect();
            else
                Disconnect();
        }

        /// <summary>业务日志输出</summary>
        ILog BizLog;

        void OnReceived(Object sender, ApiMessageEventArgs e)
        {
            var session = sender as ISocketSession;
            if (session == null)
            {
                var ns = sender as INetSession;
                if (ns == null) return;
                session = ns.Session;
            }

            if (Setting.Current.ShowReceiveString)
            {
                var line = e.Message.Payload.ToStr();
                XTrace.WriteLine(line);

                BizLog?.Info(line);
            }
        }

        Int32 _pColor = 0;
        Int32 BytesOfReceived = 0;
        Int32 BytesOfSent = 0;
        Int32 lastReceive = 0;
        Int32 lastSend = 0;
        private void timer1_Tick(Object sender, EventArgs e)
        {
            //if (!pnlSetting.Enabled)
            {
                var rcount = BytesOfReceived;
                var tcount = BytesOfSent;
                if (rcount != lastReceive)
                {
                    gbReceive.Text = (gbReceive.Tag + "").Replace("0", rcount + "");
                    lastReceive = rcount;
                }
                if (tcount != lastSend)
                {
                    gbSend.Text = (gbSend.Tag + "").Replace("0", tcount + "");
                    lastSend = tcount;
                }

                var cfg = Setting.Current;
                if (cfg.ColorLog) txtReceive.ColourDefault(_pColor);
                _pColor = txtReceive.TextLength;
            }
        }

        private List<LinkClient> cs = new List<LinkClient>();
        private void btnSend_Click(Object sender, EventArgs e)
        {
            if (_Client == null || !_Client.Active) return;

            var count = (Int32)numThreads.Value;
            if (count <= cs.Count) return;

            var sc = _Client.Client.GetService<ISocketClient>();
            if (sc == null) return;

            var uri = new NetUri(cbAddr.Text);

            Task.Run(() =>
            {
                var cc = _Client;
                for (var i = 0; i < count; i++)
                {
                    var ac = new LinkClient(uri.ToString());
                    ac.Received += OnReceived;

                    ac.UserName = cc.UserName;
                    ac.Password = cc.Password;
                    ac.LoginAction = cc.LoginAction;
                    ac.PingAction = cc.PingAction;

                    ac.Encrypted = cc.Encrypted;
                    ac.Compressed = cc.Compressed;

                    cs.Add(ac);

                    Task.Run(() =>
                    {
                        for (var k = 0; k < 10; k++)
                        {
                            if (ac.Open()) break;
                            Thread.Sleep(1000);
                        }

                        // 共用统计对象
                        if (ac.Active)
                        {
                            var sc2 = ac.Client.GetService<ISocketClient>();
                            sc2.StatSend = sc.StatSend;
                            sc2.StatReceive = sc.StatReceive;
                        }
                    });
                }
            });
        }
        #endregion

        #region 右键菜单
        private void mi清空_Click(Object sender, EventArgs e)
        {
            txtReceive.Clear();
            BytesOfReceived = 0;
        }

        private void mi清空2_Click(Object sender, EventArgs e)
        {
            txtSend.Clear();
            BytesOfSent = 0;
        }

        private void Menu_Click(Object sender, EventArgs e)
        {
            var mi = sender as ToolStripMenuItem;
            mi.Checked = !mi.Checked;

            SaveConfig();
        }
        #endregion

        #region 业务动作
        private async void btnLogin_Click(Object sender, EventArgs e)
        {
            var cfg = Setting.Current;
            var user = cfg.UserName;
            var pass = cfg.Password;

            // 如果没有编码或者密码，则使用MAC注册
            if (user.IsNullOrEmpty() || pass.IsNullOrEmpty())
            {
                user = NetHelper.GetMacs().FirstOrDefault()?.ToHex();
                pass = null;
            }

            var ct = _Client;
            ct.UserName = user;
            ct.Password = pass;

            try
            {
                var rs = await ct.LoginAsync();
                var dic = rs.ToDictionary().ToNullable();

                // 注册成功，需要保存密码
                if (dic["User"] != null)
                {
                    cfg.UserName = ct.UserName;
                    cfg.Password = ct.Password;
                    cfg.Save();

                    XTrace.WriteLine("注册成功！DeviceID={0} Password={1}", cfg.UserName, cfg.Password);
                }
                else
                {
                    XTrace.WriteLine("登录成功！");
                }
            }
            catch (ApiException ex)
            {
                XTrace.WriteLine(ex.Message);
            }
        }

        private async void btnPing_Click(Object sender, EventArgs e)
        {
            await _Client.PingAsync();
        }
        #endregion

        private async void btnTest_Click(Object sender, EventArgs e)
        {
            var rs = await _Client.InvokeAsync<String[]>("Api/All");
            XTrace.WriteLine(rs.Join(Environment.NewLine));
        }

        private void btnAdv_Click(Object sender, EventArgs e)
        {
            var cfg = Setting.Current;
            switch (cfg.Mode?.ToLower())
            {
                case "user":
                    new FrmUser { Client = _Client }.Show();
                    break;
                case "device":
                    new FrmDevice { Client = _Client }.Show();
                    break;
                default:
                    break;
            }
        }
    }
}