using System;
using System.Windows.Forms;

namespace xLink.Client
{
    public partial class FrmDevice : Form
    {
        public LinkClient Client { get; set; }
        public Byte[] Data { get; set; }

        public FrmDevice()
        {
            InitializeComponent();
        }

        private void FrmDevice_Load(Object sender, EventArgs e)
        {
            // 初始化数据区
            var buf = new Byte[1 + 4];
            buf[0] = (Byte)buf.Length;

            Data = buf;
        }

        private async void Led1_Click(Object sender, EventArgs e)
        {
            var idx = 1;
            Data[idx] = (Byte)(Data[idx] == 0 ? 1 : 0);
            txtData.Text = Data.ToHex();

            await Client.Write(idx, Data[idx]);
        }

        private async void lbLed2_Click(Object sender, EventArgs e)
        {
            var idx = 2;
            Data[idx] = (Byte)(Data[idx] == 0 ? 1 : 0);
            txtData.Text = Data.ToHex();

            await Client.Write(idx, Data[idx]);
        }

        private async void lbButton1_Click(Object sender, EventArgs e)
        {
            var idx = 3;
            Data[idx] = (Byte)(Data[idx] == 0 ? 1 : 0);
            txtData.Text = Data.ToHex();

            await Client.Write(idx, Data[idx]);
        }

        private async void lbButton2_Click(Object sender, EventArgs e)
        {
            var idx = 4;
            Data[idx] = (Byte)(Data[idx] == 0 ? 1 : 0);
            txtData.Text = Data.ToHex();

            await Client.Write(idx, Data[idx]);
        }
    }
}
