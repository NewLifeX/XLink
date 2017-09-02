using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xLink.Client
{
    public partial class FrmDevice : Form
    {
        public LinkClient Client { get; set; }

        public FrmDevice()
        {
            InitializeComponent();
        }
    }
}
