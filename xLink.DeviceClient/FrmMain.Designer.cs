﻿namespace xLink.DeviceClient
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.label7 = new System.Windows.Forms.Label();
            this.cbAddr = new System.Windows.Forms.ComboBox();
            this.lbAddr = new System.Windows.Forms.Label();
            this.gbSend = new System.Windows.Forms.GroupBox();
            this.numThreads = new System.Windows.Forms.NumericUpDown();
            this.numSleep = new System.Windows.Forms.NumericUpDown();
            this.txtSend = new System.Windows.Forms.RichTextBox();
            this.menuSend = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miHexSend = new System.Windows.Forms.ToolStripMenuItem();
            this.mi清空2 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSend = new System.Windows.Forms.Button();
            this.numMutilSend = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cbColor = new System.Windows.Forms.CheckBox();
            this.pnlSetting = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtDeviceID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.gbReceive = new System.Windows.Forms.GroupBox();
            this.txtReceive = new System.Windows.Forms.RichTextBox();
            this.menuReceive = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mi显示应用日志 = new System.Windows.Forms.ToolStripMenuItem();
            this.mi显示网络日志 = new System.Windows.Forms.ToolStripMenuItem();
            this.mi显示接收字符串 = new System.Windows.Forms.ToolStripMenuItem();
            this.mi显示发送数据 = new System.Windows.Forms.ToolStripMenuItem();
            this.mi显示接收数据 = new System.Windows.Forms.ToolStripMenuItem();
            this.mi显示统计信息 = new System.Windows.Forms.ToolStripMenuItem();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.btnConnect = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPing = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnHello = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gbSend.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThreads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSleep)).BeginInit();
            this.menuSend.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMutilSend)).BeginInit();
            this.pnlSetting.SuspendLayout();
            this.gbReceive.SuspendLayout();
            this.menuReceive.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(503, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "次数：";
            // 
            // cbAddr
            // 
            this.cbAddr.FormattingEnabled = true;
            this.cbAddr.Location = new System.Drawing.Point(45, 5);
            this.cbAddr.Name = "cbAddr";
            this.cbAddr.Size = new System.Drawing.Size(149, 20);
            this.cbAddr.TabIndex = 10;
            // 
            // lbAddr
            // 
            this.lbAddr.AutoSize = true;
            this.lbAddr.Location = new System.Drawing.Point(8, 9);
            this.lbAddr.Name = "lbAddr";
            this.lbAddr.Size = new System.Drawing.Size(41, 12);
            this.lbAddr.TabIndex = 7;
            this.lbAddr.Text = "地址：";
            // 
            // gbSend
            // 
            this.gbSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSend.Controls.Add(this.numThreads);
            this.gbSend.Controls.Add(this.numSleep);
            this.gbSend.Controls.Add(this.txtSend);
            this.gbSend.Controls.Add(this.btnSend);
            this.gbSend.Controls.Add(this.numMutilSend);
            this.gbSend.Controls.Add(this.label2);
            this.gbSend.Controls.Add(this.label7);
            this.gbSend.Location = new System.Drawing.Point(6, 346);
            this.gbSend.Name = "gbSend";
            this.gbSend.Size = new System.Drawing.Size(652, 84);
            this.gbSend.TabIndex = 20;
            this.gbSend.TabStop = false;
            this.gbSend.Text = "发送区：已发送0字节";
            // 
            // numThreads
            // 
            this.numThreads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numThreads.Location = new System.Drawing.Point(593, 22);
            this.numThreads.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numThreads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numThreads.Name = "numThreads";
            this.numThreads.Size = new System.Drawing.Size(52, 21);
            this.numThreads.TabIndex = 18;
            this.numThreads.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.numThreads, "模拟多客户端发送，用于压力测试！");
            this.numThreads.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numSleep
            // 
            this.numSleep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numSleep.Location = new System.Drawing.Point(538, 54);
            this.numSleep.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numSleep.Name = "numSleep";
            this.numSleep.Size = new System.Drawing.Size(52, 21);
            this.numSleep.TabIndex = 16;
            this.numSleep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSleep.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // txtSend
            // 
            this.txtSend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSend.ContextMenuStrip = this.menuSend;
            this.txtSend.HideSelection = false;
            this.txtSend.Location = new System.Drawing.Point(0, 19);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(497, 59);
            this.txtSend.TabIndex = 2;
            this.txtSend.Text = "";
            // 
            // menuSend
            // 
            this.menuSend.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miHexSend,
            this.mi清空2});
            this.menuSend.Name = "menuSend";
            this.menuSend.Size = new System.Drawing.Size(153, 70);
            // 
            // miHexSend
            // 
            this.miHexSend.Name = "miHexSend";
            this.miHexSend.Size = new System.Drawing.Size(152, 22);
            this.miHexSend.Text = "Hex发送";
            this.miHexSend.Click += new System.EventHandler(this.miHex发送_Click);
            // 
            // mi清空2
            // 
            this.mi清空2.Name = "mi清空2";
            this.mi清空2.Size = new System.Drawing.Size(152, 22);
            this.mi清空2.Text = "清空";
            this.mi清空2.Click += new System.EventHandler(this.mi清空2_Click);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(596, 49);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(50, 30);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // numMutilSend
            // 
            this.numMutilSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numMutilSend.Location = new System.Drawing.Point(538, 22);
            this.numMutilSend.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMutilSend.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMutilSend.Name = "numMutilSend";
            this.numMutilSend.Size = new System.Drawing.Size(52, 21);
            this.numMutilSend.TabIndex = 14;
            this.numMutilSend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numMutilSend.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(503, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "间隔：";
            // 
            // cbColor
            // 
            this.cbColor.AutoSize = true;
            this.cbColor.Checked = true;
            this.cbColor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbColor.Location = new System.Drawing.Point(497, 19);
            this.cbColor.Name = "cbColor";
            this.cbColor.Size = new System.Drawing.Size(72, 16);
            this.cbColor.TabIndex = 19;
            this.cbColor.Text = "日志着色";
            this.cbColor.UseVisualStyleBackColor = true;
            // 
            // pnlSetting
            // 
            this.pnlSetting.Controls.Add(this.checkBox1);
            this.pnlSetting.Controls.Add(this.txtDeviceID);
            this.pnlSetting.Controls.Add(this.label1);
            this.pnlSetting.Controls.Add(this.cbAddr);
            this.pnlSetting.Controls.Add(this.lbAddr);
            this.pnlSetting.Location = new System.Drawing.Point(6, 12);
            this.pnlSetting.Name = "pnlSetting";
            this.pnlSetting.Size = new System.Drawing.Size(471, 31);
            this.pnlSetting.TabIndex = 18;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(349, 8);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 20;
            this.checkBox1.Text = "自动发送";
            this.toolTip1.SetToolTip(this.checkBox1, "默认只生成指令到发送区而不发送");
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // txtDeviceID
            // 
            this.txtDeviceID.Location = new System.Drawing.Point(245, 5);
            this.txtDeviceID.Name = "txtDeviceID";
            this.txtDeviceID.Size = new System.Drawing.Size(84, 21);
            this.txtDeviceID.TabIndex = 12;
            this.txtDeviceID.Text = "ABCD12345678";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(209, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "编码：";
            // 
            // gbReceive
            // 
            this.gbReceive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbReceive.Controls.Add(this.txtReceive);
            this.gbReceive.Location = new System.Drawing.Point(146, 47);
            this.gbReceive.Name = "gbReceive";
            this.gbReceive.Size = new System.Drawing.Size(512, 293);
            this.gbReceive.TabIndex = 17;
            this.gbReceive.TabStop = false;
            this.gbReceive.Text = "接收区：已接收0字节";
            // 
            // txtReceive
            // 
            this.txtReceive.ContextMenuStrip = this.menuReceive;
            this.txtReceive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReceive.HideSelection = false;
            this.txtReceive.Location = new System.Drawing.Point(3, 17);
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.Size = new System.Drawing.Size(506, 273);
            this.txtReceive.TabIndex = 1;
            this.txtReceive.Text = "";
            // 
            // menuReceive
            // 
            this.menuReceive.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem3,
            this.mi显示应用日志,
            this.mi显示网络日志,
            this.mi显示接收字符串,
            this.mi显示发送数据,
            this.mi显示接收数据,
            this.mi显示统计信息});
            this.menuReceive.Name = "menuSend";
            this.menuReceive.Size = new System.Drawing.Size(161, 164);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem1.Text = "清空";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.mi清空_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(157, 6);
            // 
            // mi显示应用日志
            // 
            this.mi显示应用日志.Name = "mi显示应用日志";
            this.mi显示应用日志.Size = new System.Drawing.Size(160, 22);
            this.mi显示应用日志.Text = "显示应用日志";
            this.mi显示应用日志.Click += new System.EventHandler(this.mi显示应用日志_Click);
            // 
            // mi显示网络日志
            // 
            this.mi显示网络日志.Name = "mi显示网络日志";
            this.mi显示网络日志.Size = new System.Drawing.Size(160, 22);
            this.mi显示网络日志.Text = "显示网络日志";
            this.mi显示网络日志.Click += new System.EventHandler(this.mi显示网络日志_Click);
            // 
            // mi显示接收字符串
            // 
            this.mi显示接收字符串.Name = "mi显示接收字符串";
            this.mi显示接收字符串.Size = new System.Drawing.Size(160, 22);
            this.mi显示接收字符串.Text = "显示接收字符串";
            this.mi显示接收字符串.Click += new System.EventHandler(this.mi显示接收字符串_Click);
            // 
            // mi显示发送数据
            // 
            this.mi显示发送数据.Name = "mi显示发送数据";
            this.mi显示发送数据.Size = new System.Drawing.Size(160, 22);
            this.mi显示发送数据.Text = "显示发送数据";
            this.mi显示发送数据.Click += new System.EventHandler(this.mi显示发送数据_Click);
            // 
            // mi显示接收数据
            // 
            this.mi显示接收数据.Name = "mi显示接收数据";
            this.mi显示接收数据.Size = new System.Drawing.Size(160, 22);
            this.mi显示接收数据.Text = "显示接收数据";
            this.mi显示接收数据.Click += new System.EventHandler(this.mi显示接收数据_Click);
            // 
            // mi显示统计信息
            // 
            this.mi显示统计信息.Name = "mi显示统计信息";
            this.mi显示统计信息.Size = new System.Drawing.Size(160, 22);
            this.mi显示统计信息.Text = "显示统计信息";
            this.mi显示统计信息.Click += new System.EventHandler(this.mi显示统计信息_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(584, 13);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(67, 29);
            this.btnConnect.TabIndex = 16;
            this.btnConnect.Text = "打开";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPing);
            this.groupBox1.Controls.Add(this.btnRegister);
            this.groupBox1.Controls.Add(this.btnLogin);
            this.groupBox1.Controls.Add(this.btnHello);
            this.groupBox1.Controls.Add(this.lbStatus);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(6, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(134, 290);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "动作区";
            // 
            // btnPing
            // 
            this.btnPing.Location = new System.Drawing.Point(16, 149);
            this.btnPing.Name = "btnPing";
            this.btnPing.Size = new System.Drawing.Size(99, 29);
            this.btnPing.TabIndex = 5;
            this.btnPing.Text = "心跳";
            this.btnPing.UseVisualStyleBackColor = true;
            this.btnPing.Click += new System.EventHandler(this.btnPing_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(16, 114);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(99, 29);
            this.btnRegister.TabIndex = 4;
            this.btnRegister.Text = "注册";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(16, 79);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(99, 29);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnHello
            // 
            this.btnHello.Location = new System.Drawing.Point(15, 44);
            this.btnHello.Name = "btnHello";
            this.btnHello.Size = new System.Drawing.Size(99, 29);
            this.btnHello.TabIndex = 2;
            this.btnHello.Text = "握手";
            this.btnHello.UseVisualStyleBackColor = true;
            this.btnHello.Click += new System.EventHandler(this.btnHello_Click);
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStatus.Location = new System.Drawing.Point(56, 19);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(44, 12);
            this.lbStatus.TabIndex = 1;
            this.lbStatus.Text = "未连接";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "状态：";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 442);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbSend);
            this.Controls.Add(this.cbColor);
            this.Controls.Add(this.pnlSetting);
            this.Controls.Add(this.gbReceive);
            this.Controls.Add(this.btnConnect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "物联网设备模拟客户端";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.gbSend.ResumeLayout(false);
            this.gbSend.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThreads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSleep)).EndInit();
            this.menuSend.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numMutilSend)).EndInit();
            this.pnlSetting.ResumeLayout(false);
            this.pnlSetting.PerformLayout();
            this.gbReceive.ResumeLayout(false);
            this.menuReceive.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbAddr;
        private System.Windows.Forms.Label lbAddr;
        private System.Windows.Forms.GroupBox gbSend;
        private System.Windows.Forms.NumericUpDown numThreads;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NumericUpDown numSleep;
        private System.Windows.Forms.RichTextBox txtSend;
        private System.Windows.Forms.ContextMenuStrip menuSend;
        private System.Windows.Forms.ToolStripMenuItem miHexSend;
        private System.Windows.Forms.ToolStripMenuItem mi清空2;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.NumericUpDown numMutilSend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbColor;
        private System.Windows.Forms.Panel pnlSetting;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.GroupBox gbReceive;
        private System.Windows.Forms.RichTextBox txtReceive;
        private System.Windows.Forms.ContextMenuStrip menuReceive;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mi显示应用日志;
        private System.Windows.Forms.ToolStripMenuItem mi显示网络日志;
        private System.Windows.Forms.ToolStripMenuItem mi显示接收字符串;
        private System.Windows.Forms.ToolStripMenuItem mi显示发送数据;
        private System.Windows.Forms.ToolStripMenuItem mi显示接收数据;
        private System.Windows.Forms.ToolStripMenuItem mi显示统计信息;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDeviceID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnPing;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnHello;
    }
}

