using NewLife.Log;
using NewLife.Net;
using NewLife.Reflection;
using NewLife.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xLink.Client.Services;

namespace xLink.Client
{
    public partial class FrmMain : Form
    {
        LinkClient _Client;

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

            //// 加载保存的颜色
            //UIConfig.Apply(txtReceive);

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
            //mi日志着色.Checked = cfg.ColorLog;

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
            //cfg.ColorLog = mi日志着色.Checked;

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
            var cfg = Setting.Current;
            var mode = cbMode.Text;

            var url = uri.ToString();
            var ac = mode == "Device" ? new DeviceClient(url) : new LinkClient(url)
            {
                Log = cfg.ShowLog ? XTrace.Log : Logger.Null,
                EncoderLog = cfg.ShowEncoderLog ? XTrace.Log : Logger.Null
            };
            ac.UserName = cfg.UserName;
            ac.Password = cfg.Password;
            ac.ActionPrefix = mode;

            if (!ac.Open()) return;

            "已连接服务器".SpeechTip();

            _Client = ac;

            pnlSetting.Enabled = false;
            pnlAction.Enabled = true;
            btnConnect.Text = "关闭";

            // 添加地址
            cfg.AddAddresss(uri.ToString());

            cfg.Save();

            //BizLog = TextFileLog.Create("DeviceLog");
        }

        void Disconnect()
        {
            if (_Client != null)
            {
                _Client.Dispose();
                _Client = null;

                "关闭连接".SpeechTip();
            }

            pnlSetting.Enabled = true;
            pnlAction.Enabled = false;
            btnConnect.Text = "打开";

            LoadConfig();
        }

        //TimerX _timer;
        //String _lastStat;

        private void btnConnect_Click(Object sender, EventArgs e)
        {
            SaveConfig();

            var btn = sender as Button;
            if (btn.Text == "打开")
                Connect();
            else
                Disconnect();
        }

        private List<LinkClient> cs = new List<LinkClient>();
        private void btnSend_Click(Object sender, EventArgs e)
        {
            if (_Client == null || !_Client.Active) return;

            var count = (Int32)numThreads.Value;
            if (count <= cs.Count) return;

            var uri = new NetUri(cbAddr.Text);

            Task.Run(() =>
            {
                var cc = _Client;
                for (var i = 0; i < count; i++)
                {
                    var ac = new LinkClient(uri.ToString())
                    {
                        UserName = cc.UserName,
                        Password = cc.Password,
                        ActionPrefix = cc.ActionPrefix
                    };

                    cs.Add(ac);

                    Task.Run(() =>
                    {
                        for (var k = 0; k < 10; k++)
                        {
                            if (ac.Open()) break;
                            Thread.Sleep(1000);
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
        }

        private void mi清空2_Click(Object sender, EventArgs e)
        {
            txtSend.Clear();
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

            //// 如果没有编码或者密码，则使用MAC注册
            //if (user.IsNullOrEmpty() || pass.IsNullOrEmpty())
            //{
            //    user = Environment.UserName;
            //    pass = Environment.UserName;
            //}

            var ct = _Client;
            ct.UserName = user;
            ct.Password = pass;
            ct.Logined = false;

            try
            {
                //var rs = await ct.LoginAsync().ConfigureAwait(false);
                var rs = await Task.Run(() => ct.LoginAsync());
                var dic = rs.ToDictionary().ToNullable();

                // 登录成功，需要保存密码
                cfg.UserName = ct.UserName;
                cfg.Password = ct.Password;
                cfg.Save();

                XTrace.WriteLine("登录成功！");
            }
            catch (ApiException ex)
            {
                XTrace.WriteLine(ex.Message);
            }
        }

        private async void btnPing_Click(Object sender, EventArgs e)
        {
            await _Client.PingAsync().ConfigureAwait(false);
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