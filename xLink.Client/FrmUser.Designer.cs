namespace xLink.Client
{
    partial class FrmUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUser));
            this.lbButton2 = new System.Windows.Forms.Label();
            this.lbButton1 = new System.Windows.Forms.Label();
            this.lbLed2 = new System.Windows.Forms.Label();
            this.lbLed1 = new System.Windows.Forms.Label();
            this.txtData = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbButton2
            // 
            this.lbButton2.AutoSize = true;
            this.lbButton2.Location = new System.Drawing.Point(525, 113);
            this.lbButton2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbButton2.Name = "lbButton2";
            this.lbButton2.Size = new System.Drawing.Size(40, 16);
            this.lbButton2.TabIndex = 11;
            this.lbButton2.Text = "按键";
            this.lbButton2.Click += new System.EventHandler(this.lbButton2_Click);
            // 
            // lbButton1
            // 
            this.lbButton1.AutoSize = true;
            this.lbButton1.Location = new System.Drawing.Point(384, 113);
            this.lbButton1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbButton1.Name = "lbButton1";
            this.lbButton1.Size = new System.Drawing.Size(40, 16);
            this.lbButton1.TabIndex = 10;
            this.lbButton1.Text = "按键";
            this.lbButton1.Click += new System.EventHandler(this.lbButton1_Click);
            // 
            // lbLed2
            // 
            this.lbLed2.AutoSize = true;
            this.lbLed2.Location = new System.Drawing.Point(245, 113);
            this.lbLed2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbLed2.Name = "lbLed2";
            this.lbLed2.Size = new System.Drawing.Size(56, 16);
            this.lbLed2.TabIndex = 9;
            this.lbLed2.Text = "指示灯";
            this.lbLed2.Click += new System.EventHandler(this.lbLed2_Click);
            // 
            // lbLed1
            // 
            this.lbLed1.AutoSize = true;
            this.lbLed1.Location = new System.Drawing.Point(109, 113);
            this.lbLed1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbLed1.Name = "lbLed1";
            this.lbLed1.Size = new System.Drawing.Size(56, 16);
            this.lbLed1.TabIndex = 8;
            this.lbLed1.Text = "指示灯";
            this.lbLed1.Click += new System.EventHandler(this.lbLed1_Click);
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(139, 17);
            this.txtData.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(349, 26);
            this.txtData.TabIndex = 7;
            this.txtData.Text = "0501010101";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "数据区：";
            // 
            // FrmUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 215);
            this.Controls.Add(this.lbButton2);
            this.Controls.Add(this.lbButton1);
            this.Controls.Add(this.lbLed2);
            this.Controls.Add(this.lbLed1);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户客户端";
            this.Load += new System.EventHandler(this.FrmUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbButton2;
        private System.Windows.Forms.Label lbButton1;
        private System.Windows.Forms.Label lbLed2;
        private System.Windows.Forms.Label lbLed1;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Label label1;
    }
}