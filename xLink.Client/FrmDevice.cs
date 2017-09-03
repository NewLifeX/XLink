using System;
using System.Threading.Tasks;
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

        private async Task Blink(Int32 idx)
        {
            var b = (Byte)(Data[idx] == 0 ? 1 : 0);
            //txtData.Text = Data.ToHex();

            var buf = await Client.Write(null, idx, b);
            if (buf != null && buf.Length > 0)
            {
                Data = buf;
                txtData.Text = buf.ToHex();
            }
        }

        private async void lbLed1_Click(Object sender, EventArgs e)
        {
            await Blink(1);
        }

        private async void lbLed2_Click(Object sender, EventArgs e)
        {
            await Blink(2);
        }

        private async void lbButton1_Click(Object sender, EventArgs e)
        {
            await Blink(3);
        }

        private async void lbButton2_Click(Object sender, EventArgs e)
        {
            await Blink(4);
        }
    }
}
