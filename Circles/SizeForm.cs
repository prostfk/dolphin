using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Circles
{
    public partial class SizeForm : Form
    {
        public SizeForm()
        {
            InitializeComponent();
        }

        private int sizeOfPen;
        public int sizeOfPenAttr
        {
            get { return sizeOfPen; }
            set { sizeOfPen = value; }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sizeOfPenAttr = int.Parse(textBox1.Text);

            }catch(Exception)
            {
                MessageBox.Show("Число от 0 до 1000");
            }
            finally
            {
                this.Hide();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
