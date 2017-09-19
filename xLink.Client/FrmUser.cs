using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewLife.Log;

namespace xLink.Client
{
    public partial class FrmUser : Form
    {
        public LinkClient Client { get; set; }
        public Byte[] Data { get; set; }

        public FrmUser()
        {
            InitializeComponent();
        }

        private void FrmUser_Load(Object sender, EventArgs e)
        {
            // 初始化数据区
            var buf = new Byte[1 + 4];
            buf[0] = (Byte)buf.Length;

            Data = buf;
        }

        private async Task Blink(Int32 idx)
        {
            if (idx >= Data.Length) Data = new Byte[idx + 1];
            var b = (Byte)(Data[idx] == 0 ? 1 : 0);
            //txtData.Text = Data.ToHex();

            var buf = await Client.Write(txtDevice.Text, idx, b);
            if (buf != null && buf.Length > 0)
            {
                Data = buf;
                txtData.Text = buf.ToHex();
            }
        }

        private async void lbLed1_Click(Object sender, EventArgs e)
        {
            var idx = (sender as Control).Tag.ToInt();
            if (idx <= 0) return;

            try
            {
                await Blink(idx);
            }
            catch (Exception ex)
            {
                XTrace.WriteLine(ex.GetTrue().Message);
            }
        }
    }
}