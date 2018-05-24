using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Circles
{
    public partial class LoadForm : Form
    {
        public LoadForm()
        {
            InitializeComponent();
            timer1.Enabled = true;
        }
        public void openForm()
        {
            this.Show();

        }
        private int i = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (++i == 3)
            {
                this.Close();
            }
        }
    }
}
