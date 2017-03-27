using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewLife.Log;
using NewLife.Messaging;
using NewLife.Net;
using NewLife.Reflection;
using NewLife.Remoting;
using NewLife.Threading;
using NewLife.Windows;

namespace xLink.DeviceClient
{
    public partial class FrmMain : Form
    {
        LinkClient _Client;
        IPacket _Packet;

        #region 窗体
        public FrmMain()
        {
            InitializeComponent();

            //Icon = IcoHelper.GetIcon("消息");
        }

        private void FrmMain_Load(Object sender, EventArgs e)
        {
            txtReceive.UseWinFormControl();

            txtReceive.SetDefaultStyle(12);
            txtSend.SetDefaultStyle(12);
            numMutilSend.SetDefaultStyle(12);

            gbReceive.Tag = gbReceive.Text;
            gbSend.Tag = gbSend.Text;

            var cfg = DeviceConfig.Current;
            if (!cfg.Address.IsNullOrEmpty())
            {
                //cbAddr.DropDownStyle = ComboBoxStyle.DropDownList;
                cbAddr.DataSource = cfg.Address.Split(";");
            }

            // 加载保存的颜色
            UIConfig.Apply(txtReceive);

            LoadConfig();

            // 语音识别
            Task.Factory.StartNew(() =>
            {
                var sp = SpeechRecognition.Current;
                if (!sp.Enable) return;

                sp.Register("打开", () => this.Invoke(Connect))
                .Register("关闭", () => this.Invoke(Disconnect))
                .Register("退出", () => Application.Exit())
                .Register("发送", () => this.Invoke(() => btnSend_Click(null, null)));

                XTrace.WriteLine("语音识别前缀：{0} 可用命令：{1}", sp.Name, sp.GetAllKeys().Join());
            });
        }
        #endregion

        #region 加载/保存 配置
        void LoadConfig()
        {
            var cfg = DeviceConfig.Current;
            mi显示应用日志.Checked = cfg.ShowLog;
            mi显示网络日志.Checked = cfg.ShowSocketLog;
            mi显示接收字符串.Checked = cfg.ShowReceiveString;
            mi显示发送数据.Checked = cfg.ShowSend;
            mi显示接收数据.Checked = cfg.ShowReceive;
            mi显示统计信息.Checked = cfg.ShowStat;
            miHexSend.Checked = cfg.HexSend;

            txtSend.Text = cfg.SendContent;
            numMutilSend.Value = cfg.SendTimes;
            numSleep.Value = cfg.SendSleep;
            numThreads.Value = cfg.SendUsers;
            cbColor.Checked = cfg.ColorLog;

            txtDeviceID.Text = cfg.DeviceID;
            //cbAddr.Text = cfg.Address;
        }

        void SaveConfig()
        {
            var cfg = DeviceConfig.Current;
            cfg.ShowLog = mi显示应用日志.Checked;
            cfg.ShowSocketLog = mi显示网络日志.Checked;
            cfg.ShowReceiveString = mi显示接收字符串.Checked;
            cfg.ShowSend = mi显示发送数据.Checked;
            cfg.ShowReceive = mi显示接收数据.Checked;
            cfg.ShowStat = mi显示统计信息.Checked;
            cfg.HexSend = miHexSend.Checked;

            cfg.SendContent = txtSend.Text;
            cfg.SendTimes = (Int32)numMutilSend.Value;
            cfg.SendSleep = (Int32)numSleep.Value;
            cfg.SendUsers = (Int32)numThreads.Value;
            cfg.ColorLog = cbColor.Checked;

            cfg.DeviceID = txtDeviceID.Text;

            var addrs = (cfg.Address + "").Split(";").ToList();
            if (!addrs.Contains(cbAddr.Text)) addrs.Add(cbAddr.Text);
            cfg.Address = addrs.Join(";");

            cfg.Save();
        }
        #endregion

        #region 收发数据
        void Connect()
        {
            _Client = null;

            var uri = new NetUri(cbAddr.Text);
            _Packet = new DefaultPacket();
            var cfg = DeviceConfig.Current;

            var ac = new LinkClient(uri.ToString());
            ac.Log = cfg.ShowLog ? XTrace.Log : Logger.Null;
            ac.Received += OnReceived;

            var sc = ac.Client.GetService<ISocketClient>();
            sc.Log = cfg.ShowLog ? XTrace.Log : Logger.Null;
            sc.LogSend = cfg.ShowSend;
            sc.LogReceive = cfg.ShowReceive;

            "已连接服务器".SpeechTip();

            _Client = ac;
            ac.Open();

            pnlSetting.Enabled = false;
            btnConnect.Text = "关闭";

            // 添加地址
            var addr = uri.ToString();
            var list = cfg.Address.Split(";").ToList();
            if (!list.Contains(addr))
            {
                list.Insert(0, addr);
                cfg.Address = list.Join(";");
            }

            cfg.Save();

            _timer = new TimerX(ShowStat, null, 5000, 5000);

            BizLog = TextFileLog.Create("MessageLog");
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
            btnConnect.Text = "打开";
        }

        TimerX _timer;
        String _lastStat;
        void ShowStat(Object state)
        {
            if (!DeviceConfig.Current.ShowStat) return;

            var msg = "";
            if (_Client != null)
            {
                var sc = _Client.Client.GetService<ISocketClient>();
                msg = sc.GetStat();
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

            if (DeviceConfig.Current.ShowReceiveString)
            {
                var line = e.Message.Payload.ToStr();
                XTrace.WriteLine(line);

                if (BizLog != null) BizLog.Info(line);
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

                if (cbColor.Checked) txtReceive.ColourDefault(_pColor);
                _pColor = txtReceive.TextLength;
            }
        }

        private void btnSend_Click(Object sender, EventArgs e)
        {
            var str = txtSend.Text;
            if (String.IsNullOrEmpty(str))
            {
                MessageBox.Show("发送内容不能为空！", Text);
                txtSend.Focus();
                return;
            }

            // 多次发送
            var count = (Int32)numMutilSend.Value;
            var sleep = (Int32)numSleep.Value;
            var ths = (Int32)numThreads.Value;
            if (count <= 0) count = 1;
            if (sleep <= 0) sleep = 1;

            SaveConfig();

            var cfg = DeviceConfig.Current;

            // 处理换行
            str = str.Replace("\n", "\r\n");
            var buf = cfg.HexSend ? str.ToHex() : str.GetBytes();

            // 构造消息
            var msg = _Packet.CreateMessage(buf);
            buf = msg.ToArray();

            if (_Client != null)
            {
                var sc = _Client.Client.GetService<ISocketClient>();
                if (ths <= 1)
                {
                    sc.SendMulti(buf, count, sleep);
                }
                else
                {
                    Parallel.For(0, ths, n =>
                    {
                        var client = sc.Remote.CreateRemote();
                        client.StatSend = sc.StatSend;
                        client.StatReceive = sc.StatReceive;
                        client.SendMulti(buf, count, sleep);
                    });
                }
            }
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

        private void mi显示应用日志_Click(Object sender, EventArgs e)
        {
            var mi = sender as ToolStripMenuItem;
            mi.Checked = !mi.Checked;
        }

        private void mi显示网络日志_Click(Object sender, EventArgs e)
        {
            var mi = sender as ToolStripMenuItem;
            mi.Checked = !mi.Checked;
        }

        private void mi显示发送数据_Click(Object sender, EventArgs e)
        {
            var mi = sender as ToolStripMenuItem;
            mi.Checked = !mi.Checked;
        }

        private void mi显示接收数据_Click(Object sender, EventArgs e)
        {
            var mi = sender as ToolStripMenuItem;
            mi.Checked = !mi.Checked;
        }

        private void mi显示统计信息_Click(Object sender, EventArgs e)
        {
            var mi = sender as ToolStripMenuItem;
            DeviceConfig.Current.ShowStat = mi.Checked = !mi.Checked;
        }

        private void mi显示接收字符串_Click(Object sender, EventArgs e)
        {
            var mi = sender as ToolStripMenuItem;
            DeviceConfig.Current.ShowReceiveString = mi.Checked = !mi.Checked;
        }

        private void miHex发送_Click(Object sender, EventArgs e)
        {
            var mi = sender as ToolStripMenuItem;
            DeviceConfig.Current.HexSend = mi.Checked = !mi.Checked;
        }
        #endregion

        #region 业务动作
        private async void btnLogin_Click(Object sender, EventArgs e)
        {
            var cfg = DeviceConfig.Current;
            var user = cfg.DeviceID;
            var pass = cfg.Password;

            // 如果没有编码或者密码，则使用MAC注册
            if (user.IsNullOrEmpty() || pass.IsNullOrEmpty())
            {
                user = NetHelper.GetMacs().FirstOrDefault()?.ToHex();
                pass = null;
            }

            var rs = await _Client.LoginAsync(user, pass);

            // 注册成功，需要保存密码
            if (rs.ContainsKey("User"))
            {
                cfg.DeviceID = rs["User"] + "";
                cfg.Password = rs["Pass"] + "";
                cfg.Save();

                XTrace.WriteLine("注册成功！DeviceID={0} Password={1}", cfg.DeviceID, cfg.Password);
            }
            else
            {
                XTrace.WriteLine("登录成功！");
            }
        }

        private async void btnPing_Click(Object sender, EventArgs e)
        {
            await _Client.PingAsync();
        }
        #endregion
    }
}