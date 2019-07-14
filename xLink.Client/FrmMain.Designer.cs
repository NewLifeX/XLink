namespace xLink.Client
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.numMutilSend = new System.Windows.Forms.NumericUpDown();
            this.numThreads = new System.Windows.Forms.NumericUpDown();
            this.btnSend = new System.Windows.Forms.Button();
            this.numSleep = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSend = new System.Windows.Forms.RichTextBox();
            this.menuSend = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miHexSend = new System.Windows.Forms.ToolStripMenuItem();
            this.mi清空2 = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlSetting = new System.Windows.Forms.Panel();
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbReceive = new System.Windows.Forms.GroupBox();
            this.txtReceive = new System.Windows.Forms.RichTextBox();
            this.menuReceive = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mi显示应用日志 = new System.Windows.Forms.ToolStripMenuItem();
            this.mi显示编码日志 = new System.Windows.Forms.ToolStripMenuItem();
            this.mi显示接收字符串 = new System.Windows.Forms.ToolStripMenuItem();
            this.mi显示发送数据 = new System.Windows.Forms.ToolStripMenuItem();
            this.mi显示接收数据 = new System.Windows.Forms.ToolStripMenuItem();
            this.mi显示统计信息 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnPing = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.pnlAction = new System.Windows.Forms.Panel();
            this.btnAdv = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.gbSend.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMutilSend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThreads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSleep)).BeginInit();
            this.menuSend.SuspendLayout();
            this.pnlSetting.SuspendLayout();
            this.gbReceive.SuspendLayout();
            this.menuReceive.SuspendLayout();
            this.pnlAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 14);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 18);
            this.label7.TabIndex = 15;
            this.label7.Text = "次数：";
            // 
            // cbAddr
            // 
            this.cbAddr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAddr.FormattingEnabled = true;
            this.cbAddr.Location = new System.Drawing.Point(176, 10);
            this.cbAddr.Margin = new System.Windows.Forms.Padding(4);
            this.cbAddr.Name = "cbAddr";
            this.cbAddr.Size = new System.Drawing.Size(373, 26);
            this.cbAddr.TabIndex = 10;
            // 
            // lbAddr
            // 
            this.lbAddr.AutoSize = true;
            this.lbAddr.Location = new System.Drawing.Point(120, 16);
            this.lbAddr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbAddr.Name = "lbAddr";
            this.lbAddr.Size = new System.Drawing.Size(62, 18);
            this.lbAddr.TabIndex = 7;
            this.lbAddr.Text = "地址：";
            // 
            // gbSend
            // 
            this.gbSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSend.Controls.Add(this.panel1);
            this.gbSend.Controls.Add(this.txtSend);
            this.gbSend.Location = new System.Drawing.Point(9, 618);
            this.gbSend.Margin = new System.Windows.Forms.Padding(4);
            this.gbSend.Name = "gbSend";
            this.gbSend.Padding = new System.Windows.Forms.Padding(4);
            this.gbSend.Size = new System.Drawing.Size(1257, 126);
            this.gbSend.TabIndex = 20;
            this.gbSend.TabStop = false;
            this.gbSend.Text = "发送区：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.numMutilSend);
            this.panel1.Controls.Add(this.numThreads);
            this.panel1.Controls.Add(this.btnSend);
            this.panel1.Controls.Add(this.numSleep);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(1018, 20);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(222, 98);
            this.panel1.TabIndex = 2;
            // 
            // numMutilSend
            // 
            this.numMutilSend.Location = new System.Drawing.Point(56, 8);
            this.numMutilSend.Margin = new System.Windows.Forms.Padding(4);
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
            this.numMutilSend.Size = new System.Drawing.Size(78, 28);
            this.numMutilSend.TabIndex = 14;
            this.numMutilSend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numMutilSend.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numThreads
            // 
            this.numThreads.Location = new System.Drawing.Point(138, 8);
            this.numThreads.Margin = new System.Windows.Forms.Padding(4);
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
            this.numThreads.Size = new System.Drawing.Size(78, 28);
            this.numThreads.TabIndex = 18;
            this.numThreads.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.numThreads, "模拟多客户端发送，用于压力测试！");
            this.numThreads.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(142, 48);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 45);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "压测";
            this.toolTip1.SetToolTip(this.btnSend, "上方连接数");
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // numSleep
            // 
            this.numSleep.Location = new System.Drawing.Point(56, 56);
            this.numSleep.Margin = new System.Windows.Forms.Padding(4);
            this.numSleep.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numSleep.Name = "numSleep";
            this.numSleep.Size = new System.Drawing.Size(78, 28);
            this.numSleep.TabIndex = 16;
            this.numSleep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSleep.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 17;
            this.label2.Text = "间隔：";
            // 
            // txtSend
            // 
            this.txtSend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSend.ContextMenuStrip = this.menuSend;
            this.txtSend.HideSelection = false;
            this.txtSend.Location = new System.Drawing.Point(0, 28);
            this.txtSend.Margin = new System.Windows.Forms.Padding(4);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(1008, 86);
            this.txtSend.TabIndex = 2;
            this.txtSend.Text = "";
            // 
            // menuSend
            // 
            this.menuSend.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuSend.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miHexSend,
            this.mi清空2});
            this.menuSend.Name = "menuSend";
            this.menuSend.Size = new System.Drawing.Size(150, 64);
            // 
            // miHexSend
            // 
            this.miHexSend.Name = "miHexSend";
            this.miHexSend.Size = new System.Drawing.Size(149, 30);
            this.miHexSend.Text = "Hex发送";
            this.miHexSend.Click += new System.EventHandler(this.Menu_Click);
            // 
            // mi清空2
            // 
            this.mi清空2.Name = "mi清空2";
            this.mi清空2.Size = new System.Drawing.Size(149, 30);
            this.mi清空2.Text = "清空";
            this.mi清空2.Click += new System.EventHandler(this.mi清空2_Click);
            // 
            // pnlSetting
            // 
            this.pnlSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSetting.Controls.Add(this.cbMode);
            this.pnlSetting.Controls.Add(this.cbAddr);
            this.pnlSetting.Controls.Add(this.lbAddr);
            this.pnlSetting.Location = new System.Drawing.Point(9, 18);
            this.pnlSetting.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSetting.Name = "pnlSetting";
            this.pnlSetting.Size = new System.Drawing.Size(555, 46);
            this.pnlSetting.TabIndex = 18;
            // 
            // cbMode
            // 
            this.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMode.FormattingEnabled = true;
            this.cbMode.Items.AddRange(new object[] {
            "User",
            "Device"});
            this.cbMode.Location = new System.Drawing.Point(9, 10);
            this.cbMode.Margin = new System.Windows.Forms.Padding(4);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(100, 26);
            this.cbMode.TabIndex = 12;
            // 
            // gbReceive
            // 
            this.gbReceive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbReceive.Controls.Add(this.txtReceive);
            this.gbReceive.Location = new System.Drawing.Point(9, 70);
            this.gbReceive.Margin = new System.Windows.Forms.Padding(4);
            this.gbReceive.Name = "gbReceive";
            this.gbReceive.Padding = new System.Windows.Forms.Padding(4);
            this.gbReceive.Size = new System.Drawing.Size(1257, 538);
            this.gbReceive.TabIndex = 17;
            this.gbReceive.TabStop = false;
            this.gbReceive.Text = "接收区：";
            // 
            // txtReceive
            // 
            this.txtReceive.ContextMenuStrip = this.menuReceive;
            this.txtReceive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReceive.HideSelection = false;
            this.txtReceive.Location = new System.Drawing.Point(4, 25);
            this.txtReceive.Margin = new System.Windows.Forms.Padding(4);
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.Size = new System.Drawing.Size(1249, 509);
            this.txtReceive.TabIndex = 1;
            this.txtReceive.Text = "";
            // 
            // menuReceive
            // 
            this.menuReceive.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuReceive.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem3,
            this.mi显示应用日志,
            this.mi显示编码日志,
            this.mi显示接收字符串,
            this.mi显示发送数据,
            this.mi显示接收数据,
            this.mi显示统计信息});
            this.menuReceive.Name = "menuSend";
            this.menuReceive.Size = new System.Drawing.Size(207, 220);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(206, 30);
            this.toolStripMenuItem1.Text = "清空";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.mi清空_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(203, 6);
            // 
            // mi显示应用日志
            // 
            this.mi显示应用日志.Name = "mi显示应用日志";
            this.mi显示应用日志.Size = new System.Drawing.Size(206, 30);
            this.mi显示应用日志.Text = "显示应用日志";
            this.mi显示应用日志.Click += new System.EventHandler(this.Menu_Click);
            // 
            // mi显示编码日志
            // 
            this.mi显示编码日志.Name = "mi显示编码日志";
            this.mi显示编码日志.Size = new System.Drawing.Size(206, 30);
            this.mi显示编码日志.Text = "显示网络日志";
            this.mi显示编码日志.Click += new System.EventHandler(this.Menu_Click);
            // 
            // mi显示接收字符串
            // 
            this.mi显示接收字符串.Name = "mi显示接收字符串";
            this.mi显示接收字符串.Size = new System.Drawing.Size(206, 30);
            this.mi显示接收字符串.Text = "显示接收字符串";
            this.mi显示接收字符串.Click += new System.EventHandler(this.Menu_Click);
            // 
            // mi显示发送数据
            // 
            this.mi显示发送数据.Name = "mi显示发送数据";
            this.mi显示发送数据.Size = new System.Drawing.Size(206, 30);
            this.mi显示发送数据.Text = "显示发送数据";
            this.mi显示发送数据.Click += new System.EventHandler(this.Menu_Click);
            // 
            // mi显示接收数据
            // 
            this.mi显示接收数据.Name = "mi显示接收数据";
            this.mi显示接收数据.Size = new System.Drawing.Size(206, 30);
            this.mi显示接收数据.Text = "显示接收数据";
            this.mi显示接收数据.Click += new System.EventHandler(this.Menu_Click);
            // 
            // mi显示统计信息
            // 
            this.mi显示统计信息.Name = "mi显示统计信息";
            this.mi显示统计信息.Size = new System.Drawing.Size(206, 30);
            this.mi显示统计信息.Text = "显示统计信息";
            this.mi显示统计信息.Click += new System.EventHandler(this.Menu_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(602, 20);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(100, 44);
            this.btnConnect.TabIndex = 16;
            this.btnConnect.Text = "打开";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnPing
            // 
            this.btnPing.Location = new System.Drawing.Point(106, 3);
            this.btnPing.Margin = new System.Windows.Forms.Padding(4);
            this.btnPing.Name = "btnPing";
            this.btnPing.Size = new System.Drawing.Size(93, 44);
            this.btnPing.TabIndex = 5;
            this.btnPing.Text = "心跳";
            this.btnPing.UseVisualStyleBackColor = true;
            this.btnPing.Click += new System.EventHandler(this.btnPing_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(4, 3);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(93, 44);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // pnlAction
            // 
            this.pnlAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAction.Controls.Add(this.btnAdv);
            this.pnlAction.Controls.Add(this.btnTest);
            this.pnlAction.Controls.Add(this.btnPing);
            this.pnlAction.Controls.Add(this.btnLogin);
            this.pnlAction.Enabled = false;
            this.pnlAction.Location = new System.Drawing.Point(729, 18);
            this.pnlAction.Margin = new System.Windows.Forms.Padding(4);
            this.pnlAction.Name = "pnlAction";
            this.pnlAction.Size = new System.Drawing.Size(410, 46);
            this.pnlAction.TabIndex = 22;
            // 
            // btnAdv
            // 
            this.btnAdv.Location = new System.Drawing.Point(310, 3);
            this.btnAdv.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdv.Name = "btnAdv";
            this.btnAdv.Size = new System.Drawing.Size(93, 44);
            this.btnAdv.TabIndex = 7;
            this.btnAdv.Text = "高级";
            this.btnAdv.UseVisualStyleBackColor = true;
            this.btnAdv.Click += new System.EventHandler(this.btnAdv_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(208, 3);
            this.btnTest.Margin = new System.Windows.Forms.Padding(4);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(93, 44);
            this.btnTest.TabIndex = 6;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 762);
            this.Controls.Add(this.pnlAction);
            this.Controls.Add(this.gbSend);
            this.Controls.Add(this.pnlSetting);
            this.Controls.Add(this.gbReceive);
            this.Controls.Add(this.btnConnect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "物联网客户端";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.gbSend.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMutilSend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThreads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSleep)).EndInit();
            this.menuSend.ResumeLayout(false);
            this.pnlSetting.ResumeLayout(false);
            this.pnlSetting.PerformLayout();
            this.gbReceive.ResumeLayout(false);
            this.menuReceive.ResumeLayout(false);
            this.pnlAction.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Panel pnlSetting;
        private System.Windows.Forms.GroupBox gbReceive;
        private System.Windows.Forms.RichTextBox txtReceive;
        private System.Windows.Forms.ContextMenuStrip menuReceive;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mi显示应用日志;
        private System.Windows.Forms.ToolStripMenuItem mi显示编码日志;
        private System.Windows.Forms.ToolStripMenuItem mi显示接收字符串;
        private System.Windows.Forms.ToolStripMenuItem mi显示发送数据;
        private System.Windows.Forms.ToolStripMenuItem mi显示接收数据;
        private System.Windows.Forms.ToolStripMenuItem mi显示统计信息;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnPing;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Panel pnlAction;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbMode;
        private System.Windows.Forms.Button btnAdv;
    }
}

